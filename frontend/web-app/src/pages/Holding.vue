<template>
  <page>
    <progress
      v-if="!isDataReady"
      class="progress is-small is-primary"
      max="100"
    ></progress>
    <template v-else>
      <stock-info
        :stockInfo="stockInfo"
        :holdingId="holdingId"
        :totalCost="totalCost"
      ></stock-info>
      <br />
      <p class="subtitle">Trade History</p>
      <table class="table is-hoverable is-fullwidth">
        <thead>
          <tr>
            <th>Date</th>
            <th>Quantity</th>
            <th>Unit Price</th>
            <th>Fees</th>
            <th>Type</th>
            <th>Action</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="trade in trades" :key="trade.id">
            <td>{{ trade.date }}</td>
            <td>{{ trade.quantity }}</td>
            <td>{{ trade.unitPrice }}</td>
            <td>{{ trade.fees }}</td>
            <td>
              <span class="icon-text">
                <span v-if="trade.tradeType == 'Buy'" class="icon">
                  <i class="fas fa-caret-left"></i>
                </span>
                <span v-else-if="trade.tradeType == 'Sell'" class="icon">
                  <i class="fas fa-caret-right"></i>
                </span>
                <span>{{ trade.tradeType }}</span>
              </span>
            </td>
            <td>
              <button
                class="button is-danger is-small"
                @click="deleteTrade(trade)"
              >
                Delete
              </button>
            </td>
          </tr>
        </tbody>
      </table>
    </template>
    <div>
      <canvas id="historicalPrice"></canvas>
    </div>
  </page>
</template>

<script>
import Page from "../components/Page.vue";
import StockInfo from "../components/StockInfo.vue";
import pageMixin from "../mixins/page";
import holdingService from "../services/holdingService";
import stockService from "../services/stockService";
import tradeService from "../services/tradeService";
import dateConverter from "../utils/dateConverter";
import charting from "../utils/charting";

export default {
  components: { Page, StockInfo },
  data() {
    return {
      holdingId: "",
      ticker: "",
      trades: [],
      stockInfo: {},
      historicalPrices: [],
      chartItem: {}
    };
  },
  mixins: [pageMixin],
  computed: {
    totalCost() {
      return this.trades
        .map(t => t.unitPrice * t.quantity + t.fees)
        .reduce((a, b) => a + b, 0);
    }
  },
  async created() {
    this.holdingId = this.$route.params.id;

    const accessToken = this.getAccessToken();

    const holdingResp = await holdingService.getHolding(
      this.holdingId,
      accessToken
    );

    this.ticker = holdingResp.data.ticker;

    for (const trade of holdingResp.data.trades) {
      this.trades.push({
        date: dateConverter.toString(new Date(trade.date)),
        quantity: trade.quantity,
        unitPrice: trade.unitPrice,
        fees: trade.fees,
        tradeType: trade.tradeType
      });
    }

    const costHistory = [];
    for (const ch of holdingResp.data.costHistory) {
      costHistory.push({
        date: dateConverter.toString(new Date(ch.date)),
        cost: ch.cost
      });
    }

    const stockInfoResp = await stockService.getInfo(this.ticker, accessToken);
    this.stockInfo = stockInfoResp.data;

    const stockPerformanceResp = await stockService.getPerformance(
      this.ticker,
      dateConverter.toString(new Date(this.trades[0].date)),
      dateConverter.toString(new Date()),
      accessToken
    );

    let cost = 0;
    for (const stockClosePrice of stockPerformanceResp.data) {
      const date = dateConverter.toString(new Date(stockClosePrice.date));
      const index = costHistory.findIndex(ch => ch.date === date);
      if (index != -1) {
        cost = costHistory[index].cost;
      }
      this.historicalPrices.push({
        date,
        closePrice: stockClosePrice.closePrice,
        cost
      });
    }

    this.isDataReady = true;

    charting.plotPriceTrend("historicalPrice", this.historicalPrices);
  },
  methods: {
    async deleteTrade(trade) {
      const accessToken = this.getAccessToken();
      try {
        await tradeService.deleteTrade(trade.id, accessToken);

        await this.notifySuccess(
          "Trade deleted",
          `Trade ${trade.id} has been deleted.`
        );

        this.$router.go();
      } catch (error) {
        this.notifyError("Unable to delete trade", error);
      }
    }
  }
};
</script>
