<template>
  <page>
    <div class="tags has-addons">
      <span class="tag is-dark">Portfolio ID</span>
      <span class="tag is-primary">{{ portfolioId }}</span>
    </div>
    <div class="columns">
      <div class="column">
        <progress
          v-if="!isDataReady"
          class="progress is-small is-primary"
          max="100"
        ></progress>
        <table v-else class="table is-hoverable is-fullwidth">
          <thead>
            <tr>
              <th>Name</th>
              <th>Quantity</th>
              <th>Cost</th>
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
            </tr>
          </tbody>
        </table>
        <pagination
          :currentPageNumber="currentPageNumber"
          :totalPageCount="Math.ceil(holdings.length / pageSize)"
          @goToPage="goToPage"
        ></pagination>
      </div>
      <div class="column"></div>
    </div>
  </page>
</template>

<script>
import Page from "../components/Page.vue";
import Pagination from "../components/Pagination.vue";
import holdingService from "../services/holdingService";

export default {
  components: { Page, Pagination },
  data() {
    return {
      portfolioId: this.$route.params.id,
      isDataReady: false,
      currentPageNumber: 1,
      pageSize: 2,
      holdings: []
    };
  },
  computed: {
    filteredHoldings() {
      return this.holdings.slice(
        (this.currentPageNumber - 1) * this.pageSize,
        this.currentPageNumber * this.pageSize
      );
    }
  },
  created() {
    const accessToken = this.$store.state.auth.accessToken;

    holdingService.getHoldings(this.portfolioId, accessToken).then(resp => {
      this.isDataReady = true;
      this.holdings = resp.data;
    });
  },
  methods: {
    goToPage(pageNumber) {
      this.currentPageNumber = pageNumber;
    }
  }
};
</script>
