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
            <td>{{ trade.tradeType }}</td>
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

    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
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
// import charting from "../utils/charting";

export default {
  components: { Page, StockInfo },
  data() {
    return {
      holdingId: "",
      ticker: "",
      trades: [],
      stockInfo: {},
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
  created() {
    this.holdingId = this.$route.params.id;
    const accessToken = this.getAccessToken();
    holdingService
      .getHolding(this.holdingId, accessToken)
      .then(resp => {
        this.ticker = resp.data.ticker;
        this.trades = resp.data.trades;
      })
      .then(() => stockService.getInfo(this.ticker, accessToken))
      .then(resp => {
        console.log("resp", resp);
        this.stockInfo = resp.data;
        this.isDataReady = true;
      });

    // stockService.getDetails("AAPL").then(resp => {
    //   this.details.logo = resp.logo;
    //   this.details.name = resp.name;
    //   this.details.sector = resp.sector;
    //   this.details.industry = resp.industry;
    //   this.details.description = resp.description;
    // });

    // stockService.getPreviousPrice("AAPL").then(resp => {
    //   const result = resp.results[0];
    //   this.stock.open = result.o;
    //   this.stock.close = result.c;
    //   this.stock.high = result.h;
    //   this.stock.low = result.l;
    //   this.stock.volume = result.v;
    // });
  },
  mounted() {
    // stockService.getTrend("AAPL").then(resp => {
    //   const trend = resp.results.map(r => {
    //     return {
    //       value: r.c,
    //       cost: 100,
    //       date: new Date(r.t).toDateString()
    //     };
    //   });
    //   charting.plotPriceTrend("historicalPrice", trend);
    // });
  },
  methods: {
    deleteTrade(trade) {
      const accessToken = this.getAccessToken();
      tradeService
        .deleteTrade(trade.id, accessToken)
        .then(() => {
          this.notifySuccess(
            "Trade deleted",
            `Trade ${trade.id} has been deleted.`
          ).then(() => {
            this.$router.go();
          });
        })
        .catch(err => {
          this.notifyError("Unable to delete trade", err);
        });
    }
  }
};
</script>
