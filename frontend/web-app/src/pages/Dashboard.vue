<template>
  <page>
    <p>this is dashboard</p>
    <div class="columns">
      <div class="column">
        <canvas id="weighting"></canvas>
      </div>
      <div class="column">
        <h1 class="title">Summary</h1>
        <p>Capital Gain: xx</p>
        <p>Dividend: xx</p>
        <p>Total Return: xx</p>
      </div>
    </div>
  </page>
</template>

<script>
import Page from "../components/Page.vue";
import charting from "../utils/charting";

export default {
  components: { Page },
  data() {
    return {};
  },
  mounted() {
    charting.plotPie("weighting");
  }
};
</script>
