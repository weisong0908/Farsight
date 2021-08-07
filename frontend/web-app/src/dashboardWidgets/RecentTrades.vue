<template>
  <div>
    <h1 class="subtitle">Recent Trades</h1>
    <table class="table is-fullwidth">
      <tbody>
        <tr
          v-for="trade in recentTrades"
          :key="trade.ticker + trade.tradeType + trade.quantity"
        >
          <td>{{ trade.ticker }}</td>
          <td>
            <span class="icon-text">
              <span v-if="trade.tradeType == 'Buy'" class="icon is-success">
                <i class="fas fa-caret-left"></i>
              </span>
              <span v-else-if="trade.tradeType == 'Sell'" class="icon">
                <i class="fas fa-caret-right"></i>
              </span>
              <span>{{ trade.tradeType }}</span>
            </span>
          </td>
          <td>{{ trade.quantity }}</td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script>
import dashboardWidgetsService from "../services/dashboardWidgetService";
import pageMixin from "../mixins/page";

export default {
  data() {
    return {
      recentTrades: []
    };
  },
  mixins: [pageMixin],
  async created() {
    const { data } = await dashboardWidgetsService.getRecentTradesWidgetData(
      this.accessToken
    );
    this.recentTrades = data;
  }
};
</script>
