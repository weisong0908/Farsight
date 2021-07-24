<template>
  <page>
    <progress
      v-if="!isDataReady"
      class="progress is-small is-primary"
      max="100"
    ></progress>
    <template v-else>
      <div class="select">
        <select v-model="selectedPortfolio">
          <option value="all">All portfolios</option>
          <option
            v-for="portfolio in portfolios"
            :key="portfolio.id"
            :value="portfolio.id"
            >{{ portfolio.name }}</option
          >
        </select>
      </div>
      <table class="table is-hoverable is-fullwidth">
        <thead>
          <tr>
            <th>Name</th>
            <th>Quantity</th>
            <th>Unit Cost</th>
            <th>Portfolio</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="holding in filteredHoldings" :key="holding.id">
            <td>
              <router-link
                :to="{
                  name: 'holding',
                  params: { id: holding.id }
                }"
              >
                {{ holding.ticker }}
              </router-link>
            </td>
            <td>{{ holding.quantity }}</td>
            <td>{{ holding.cost }}</td>
            <td>
              {{ holding.portfolio.name }}
            </td>
          </tr>
        </tbody>
      </table>
    </template>
  </page>
</template>

<script>
import Page from "../components/Page.vue";
import pageMixin from "../mixins/page";
import portfolioService from "../services/portfolioService";

export default {
  components: { Page },
  data() {
    return {
      portfolios: [],
      holdings: [],
      selectedPortfolio: "all"
    };
  },
  computed: {
    filteredHoldings() {
      return this.selectedPortfolio === "all"
        ? this.holdings
        : this.holdings.filter(h => h.portfolio.id === this.selectedPortfolio);
    }
  },
  mixins: [pageMixin],
  created() {
    const accessToken = this.getAccessToken();

    portfolioService
      .getPortfolios(accessToken)
      .then(resp => {
        this.portfolios = resp.data;
      })
      .then(() => {
        this.portfolios.forEach(p => {
          portfolioService.getPortfolio(p.id, accessToken).then(resp => {
            resp.data.holdings.forEach(h => {
              this.holdings.push({
                ...h,
                portfolio: { id: resp.data.id, name: resp.data.name }
              });
            });
          });
        });
      })
      .then(() => {
        this.isDataReady = true;
      });
  }
};
</script>
