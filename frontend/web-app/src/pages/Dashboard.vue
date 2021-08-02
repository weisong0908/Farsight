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
        <div class="box"></div>
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

export default {
  components: { Page, PortfoliosWidget, HoldingsWidget },
  data() {
    return {};
  },
  mounted() {}
};
</script>
