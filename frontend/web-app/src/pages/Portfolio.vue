<template>
  <page>
    <div class="columns">
      <div class="column">
        <edit-portfolio-form
          v-if="isDataReady"
          :portfolioId="portfolioId"
          :portfolioName="portfolioName"
          @submit="updatePortfolio"
        ></edit-portfolio-form>
      </div>
      <div class="column">
        <p class="subtitle">Holdings</p>
        <add-holding-modal-form
          title="Add New Holding"
          :isActive="isAddHoldingModalFormActive"
          @close="isAddHoldingModalFormActive = false"
          @submit="createHolding"
        >
        </add-holding-modal-form>
        <div class="buttons">
          <button
            class="button is-primary"
            @click="isAddHoldingModalFormActive = true"
          >
            Add New Holding
          </button>
        </div>
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
    </div>
  </page>
</template>

<script>
import Page from "../components/Page.vue";
import EditPortfolioForm from "../normalForms/EditPortfolio.vue";
import AddHoldingModalForm from "../modalForms/AddHolding.vue";
import Pagination from "../components/Pagination.vue";
import pageMixin from "../mixins/page";
import portfolioService from "../services/portfolioService";

export default {
  components: {
    Page,
    EditPortfolioForm,
    AddHoldingModalForm,
    Pagination
  },
  data() {
    return {
      portfolioId: this.$route.params.id,
      portfolioName: "",
      currentPageNumber: 1,
      pageSize: 2,
      holdings: [],
      isAddHoldingModalFormActive: false
    };
  },
  mixins: [pageMixin],
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

    portfolioService.getPortfolio(this.portfolioId, accessToken).then(resp => {
      this.isDataReady = true;
      this.portfolioName = resp.data.name;
      this.holdings = resp.data.holdings;
    });
  },
  methods: {
    updatePortfolio(portfolio) {
      const accessToken = this.getAccessToken();
      const userId = this.$store.state.auth.user.userId;
      portfolioService
        .updatePortfolio({ ...portfolio, ownerId: userId }, accessToken)
        .then(() => {
          this.notifySuccess(
            "Portfolio updated",
            `Portfolio "${portfolio.name}" has been updated successfully.`
          );
        })
        .catch(err => {
          this.notifyError("Unable to update portfolio", err);
        });
    },
    goToPage(pageNumber) {
      this.currentPageNumber = pageNumber;
    },
    createHolding(holding) {
      this.isAddHoldingModalFormActive = false;

      alert(holding);
    }
  }
};
</script>
