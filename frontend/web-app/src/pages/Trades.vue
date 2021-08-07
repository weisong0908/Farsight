<template>
  <page>
    <progress
      v-if="!isDataReady"
      class="progress is-small"
      max="100"
    ></progress>
    <template v-else>
      <div class="select">
        <select v-model="selectedHolding">
          <option value="all">All holdings</option>
          <option
            v-for="holding in holdings"
            :key="holding.id"
            :value="holding.id"
            >{{ holding.ticker }}</option
          >
        </select>
      </div>
      <div class="table-container">
        <table class="table is-hoverable is-fullwidth">
          <thead>
            <tr>
              <th>Ticker</th>
              <th>Quantity</th>
              <th>Unit Price</th>
              <th>Fees</th>
              <th>Trade Type</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="trade in filteredTrades" :key="trade.id">
              <td>
                <router-link
                  :to="{
                    name: 'holding',
                    params: { id: trade.holdingId }
                  }"
                >
                  {{ trade.ticker }}
                </router-link>
              </td>
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
            </tr>
          </tbody>
        </table>
      </div>
    </template>
  </page>
</template>

<script>
import Page from "../components/Page.vue";
import pageMixin from "../mixins/page";
import holdingService from "../services/holdingService";
import tradeService from "../services/tradeService";

export default {
  components: { Page },
  data() {
    return {
      holdings: [],
      trades: [],
      selectedHolding: "all"
    };
  },
  computed: {
    filteredTrades() {
      return this.selectedHolding === "all"
        ? this.trades
        : this.trades.filter(t => t.holdingId === this.selectedHolding);
    }
  },
  mixins: [pageMixin],
  async created() {
    try {
      this.trades = (await tradeService.getTrades(this.accessToken)).data;

      this.holdings = (await holdingService.getHoldings(this.accessToken)).data;

      for (const trade of this.trades) {
        trade["ticker"] = this.holdings.filter(
          h => h.id === trade.holdingId
        )[0].ticker;
      }

      this.isDataReady = true;
    } catch (error) {
      this.notifyError("Unable to retrieve trades", error);
    }
  }
};
</script>
