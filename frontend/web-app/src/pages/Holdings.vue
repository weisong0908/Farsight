<template>
  <page>
    <progress
      v-if="!isDataReady"
      class="progress is-small"
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
      <div class="table-container">
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
              <td>{{ holding.unitCost }}</td>
              <td>
                {{ holding.portfolioName }}
              </td>
            </tr>
          </tbody>
        </table>
      </div>
    </template>
  </page>
</template>

<script>
import Page from "../components/Page.vue";
import pageMixin from "../mixins/page";
import holdingService from "../services/holdingService";
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
        : this.holdings.filter(h => h.portfolioId === this.selectedPortfolio);
    }
  },
  mixins: [pageMixin],
  async created() {
    const accessToken = this.getAccessToken();

    try {
      this.portfolios = (
        await portfolioService.getPortfolios(accessToken)
      ).data;

      this.holdings = (await holdingService.getHoldings(accessToken)).data;

      for (const holding of this.holdings) {
        holding["portfolioName"] = this.portfolios.filter(
          p => p.id === holding.portfolioId
        )[0].name;
      }

      this.isDataReady = true;
    } catch (error) {
      this.notifyError("Unable to retreve holdings", error);
    }
  }
};
</script>
