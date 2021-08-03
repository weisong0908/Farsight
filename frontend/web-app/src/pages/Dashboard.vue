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
          <top-holdings-widget></top-holdings-widget>
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
          <summary-widget></summary-widget>
        </div>
      </div>
      <div class="column"></div>
      <div class="column"></div>
    </div>
  </page>
</template>

<script>
import Page from "../components/Page.vue";
import PortfoliosWidget from "../dashboardWidgets/Portfolios.vue";
import TopHoldingsWidget from "../dashboardWidgets/TopHoldings.vue";
import RecentTradesWidget from "../dashboardWidgets/RecentTrades.vue";
import SummaryWidget from "../dashboardWidgets/Summary.vue";

export default {
  components: {
    Page,
    PortfoliosWidget,
    TopHoldingsWidget,
    RecentTradesWidget,
    SummaryWidget
  },
  data() {
    return {};
  },
  mounted() {}
};
</script>
