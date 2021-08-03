<template>
  <page>
    <div class="columns">
      <div class="column">
        <div class="box">
          <portfolios-widget></portfolios-widget>
        </div>
      </div>
      <div class="column">
        <div class="box">
          <holdings-widget></holdings-widget>
        </div>
      </div>
      <div class="column">
        <div class="box">
          <recent-trades-widget></recent-trades-widget>
        </div>
      </div>
    </div>
    <div class="columns">
      <div class="column">
        <div class="box">
          <h1 class="title">Summary</h1>
          <p>Capital Gain: xx</p>
          <p>Dividend: xx</p>
          <p>Total Return: xx</p>
        </div>
      </div>
    </div>
  </page>
</template>

<script>
import Page from "../components/Page.vue";
import PortfoliosWidget from "../dashboardWidgets/Portfolios.vue";
import HoldingsWidget from "../dashboardWidgets/Holdings.vue";
import RecentTradesWidget from "../dashboardWidgets/RecentTrades.vue";

export default {
  components: { Page, PortfoliosWidget, HoldingsWidget, RecentTradesWidget },
  data() {
    return {};
  },
  mounted() {}
};
</script>
