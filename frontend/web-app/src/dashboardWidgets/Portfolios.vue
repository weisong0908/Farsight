<template>
  <div>
    <h1 class="subtitle">My Portfolios</h1>
    <table class="table is-fullwidth">
      <tbody>
        <tr v-for="portfolio in portfolios" :key="portfolio.name">
          <td>{{ portfolio.name }}</td>
        </tr>
      </tbody>
    </table>
    <router-link class="button is-light" :to="{ name: 'portfolios' }">
      <span>See all portfolios</span>
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
      portfolios: []
    };
  },
  mixins: [pageMixin],
  async created() {
    const accessToken = this.getAccessToken();
    const { data } = await dashboardWidgetsService.getPortfoliosWidgetData(
      accessToken
    );
    this.portfolios = data;
  }
};
</script>
