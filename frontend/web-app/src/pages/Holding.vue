<template>
  <page>
    <progress
      v-if="!isDataReady"
      class="progress is-small"
      max="100"
    ></progress>
    <template v-else>
      <stock-info
        :stockInfo="stockInfo"
        :holdingId="holdingId"
        :investedAmount="investedAmount"
      ></stock-info>
      <br />
      <div class="box">
        <p class="subtitle">Trade History</p>
        <add-trade-modal-form
          :isActive="isAddTradeModalFormActive"
          @close="isAddTradeModalFormActive = false"
          @submit="addTrade"
        ></add-trade-modal-form>
        <div class="buttons">
          <button class="button" @click="isAddTradeModalFormActive = true">
            Add New Trade
          </button>
        </div>
        <div v-if="trades.length > 0" class="table-container">
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
                  <div class="buttons">
                    <button
                      class="button is-small"
                      @click="updateEditTradeModalForm(trade)"
                    >
                      Edit
                    </button>
                    <button
                      class="button is-danger is-light is-small"
                      @click="deleteTrade(trade)"
                    >
                      Delete
                    </button>
                  </div>
                </td>
              </tr>
            </tbody>
          </table>
        </div>
        <article v-else class="message is-warning">
          <div class="message-body">
            There is no trade record found for this holding
          </div>
        </article>
      </div>
      <edit-trade-modal-form
        :selectedTrade="selectedTrade"
        :isActive="isEditTradeModalFormActive"
        @close="isEditTradeModalFormActive = false"
        @submit="updateTrade"
      ></edit-trade-modal-form>
    </template>
    <div v-if="trades.length > 0" class="box">
      <canvas id="historicalPrice"></canvas>
    </div>
  </page>
</template>

<script>
import Page from "../components/Page.vue";
import StockInfo from "../components/StockInfo.vue";
import AddTradeModalForm from "../modalForms/AddTrade.vue";
import EditTradeModalForm from "../modalForms/EditTrade.vue";
import pageMixin from "../mixins/page";
import holdingService from "../services/holdingService";
import stockService from "../services/stockService";
import tradeService from "../services/tradeService";
import dateConverter from "../utils/dateConverter";
import charting from "../utils/charting";

export default {
  components: { Page, StockInfo, AddTradeModalForm, EditTradeModalForm },
  data() {
    return {
      holdingId: "",
      ticker: "",
      trades: [],
      stockInfo: {},
      historicalPrices: [],
      chartItem: {},
      isEditTradeModalFormActive: false,
      selectedTrade: {},
      isAddTradeModalFormActive: false,
      investedAmount: 0
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

    const holdingResp = await holdingService.getHolding(
      this.holdingId,
      this.accessToken
    );

    this.ticker = holdingResp.data.ticker;
    this.trades = holdingResp.data.trades;
    this.investedAmount = holdingResp.data.investedAmount;

    if (this.trades.length > 0) {
      const costHistory = holdingResp.data.costHistory;
      const stockPerformanceResp = await stockService.getPerformance(
        this.ticker,
        this.trades[0].date,
        dateConverter.toString(new Date()),
        this.accessToken
      );

      const stockClosePrices = stockPerformanceResp.data;

      let cost = costHistory
        .filter(ch => ch.date <= stockClosePrices[0].date)
        .pop().cost;
      for (const stockClosePrice of stockClosePrices) {
        const date = stockClosePrice.date;
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

      charting.plotPriceTrend(
        "historicalPrice",
        this.historicalPrices,
        this.ticker
      );
    }

    const stockInfoResp = await stockService.getInfo(
      this.ticker,
      this.accessToken
    );
    this.stockInfo = stockInfoResp.data;

    this.isDataReady = true;
  },
  methods: {
    updateEditTradeModalForm(trade) {
      this.selectedTrade = trade;
      this.isEditTradeModalFormActive = true;
    },
    async addTrade(trade) {
      try {
        await tradeService.createTrade(
          {
            ...trade,
            holdingId: this.holdingId
          },
          this.accessToken
        );
        await this.notifySuccess("Trade added", `Trade has been added.`);
      } catch (error) {
        this.notifyError("Unable to add trade", error);
      }

      this.isAddTradeModalFormActive = false;
      this.$router.go();
    },
    async updateTrade(trade) {
      try {
        await tradeService.updateTrade(
          {
            id: this.selectedTrade.id,
            ...trade,
            holdingId: this.holdingId
          },
          this.accessToken
        );

        await this.notifySuccess(
          "Trade updated",
          `Trade ${trade.id} has been updated.`
        );
      } catch (error) {
        this.notifyError("Unable to update trade", error);
      }

      this.isEditTradeModalFormActive = false;
      this.$router.go();
    },
    async deleteTrade(trade) {
      try {
        await tradeService.deleteTrade(trade.id, this.accessToken);

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
