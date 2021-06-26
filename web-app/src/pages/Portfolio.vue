<template>
  <page>
    <p>this is portfolio {{ portfolioId }}</p>
    <table class="table is-bordered">
      <thead>
        <tr>
          <th>Holding</th>
          <th></th>
        </tr>
      </thead>
      <tbody>
        <tr>
          <td>Holding 1</td>
          <td>
            <router-link to="/holdings/1">Go</router-link>
          </td>
        </tr>
        <tr>
          <td>Holding 2</td>
          <td>
            <router-link to="/holdings/2">Go</router-link>
          </td>
        </tr>
      </tbody>
    </table>
  </page>
</template>

<script>
import Page from "../components/Page.vue";

export default {
  components: { Page },
  data() {
    return {
      portfolioId: ""
    };
  },
  created() {
    this.portfolioId = this.$route.params.id;
  }
};
</script>
