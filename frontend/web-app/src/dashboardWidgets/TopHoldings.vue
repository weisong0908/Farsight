<template>
  <div>
    <h1 class="subtitle">Top Holdings</h1>
    <table class="table is-fullwidth">
      <tbody>
        <tr v-for="holding in holdings" :key="holding.ticker">
          <td>{{ holding.ticker }}</td>
          <td>{{ holding.marketValue }}</td>
        </tr>
      </tbody>
    </table>
    <router-link class="button is-light" :to="{ name: 'holdings' }">
      <span>See all holdings</span>
      <span class="icon is-small">
        <i class="fas fa-arrow-right"></i>
      </span>
    </router-link>
  </div>
</template>

<script>
import dashboardWidgetsService from "../services/dashboardWidgetService";
import pageMixin from "../mixins/page";

export default {
  data() {
    return {
      holdings: []
    };
  },
  mixins: [pageMixin],
  async created() {
    const accessToken = this.getAccessToken();

    const { data } = await dashboardWidgetsService.getTopHoldingsWidgetData(
      accessToken
    );
    this.holdings = data;
  }
};
</script>
