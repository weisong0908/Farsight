<template>
  <page>
    <p>this is dashboard</p>
    <p>is auth: {{ isAuth }}</p>
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
import authService from "../services/authService";

export default {
  components: { Page },
  data() {
    return {
      isAuth: false
    };
  },
  created() {
    this.isAuth = authService.isAuth(this.$store.state.auth);
  },
  mounted() {
    charting.plotPie("weighting");
  }
};
</script>
