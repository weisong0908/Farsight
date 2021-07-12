<template>
  <page>
    <table class="table is-bordered">
      <thead>
        <tr>
          <th>Portfolio</th>
          <th>Holdings</th>
          <th>Action</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="portfolio in portfolios" :key="portfolio.id">
          <td>
            <router-link
              :to="{ name: 'portfolio', params: { id: portfolio.id } }"
            >
              {{ portfolio.name }}
            </router-link>
          </td>
          <td>{{ portfolio.holdingCount }}</td>
          <td>
            <button
              class="button is-danger"
              @click="deletePortfolio(portfolio)"
            >
              Delete
            </button>
          </td>
        </tr>
      </tbody>
    </table>
    <router-link class="button is-primary" :to="{ name: 'addPortfolioForm' }"
      >Create New Portfolio</router-link
    >
  </page>
</template>

<script>
import Page from "../components/Page.vue";
import portfolioService from "../services/portfolioService";

export default {
  components: { Page },
  data() {
    return {
      portfolios: []
    };
  },
  created() {
    const accessToken = this.$store.state.auth.accessToken;

    portfolioService.getPortfolios(accessToken).then(resp => {
      this.portfolios = resp.data;
    });
  },
  methods: {
    deletePortfolio(portfolio) {
      alert(portfolio.name);
      this.$router.go();
    }
  }
};
</script>
