<template>
  <page>
    <div class="table-container">
      <table class="table is-hoverable">
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
                class="button is-danger is-light is-small"
                @click="deletePortfolio(portfolio.id)"
              >
                Delete
              </button>
            </td>
          </tr>
        </tbody>
      </table>
    </div>
    <add-portfolio-modal-form
      title="Add New Portfolio"
      :isActive="isAddPortfolioModalFormActive"
      @close="isAddPortfolioModalFormActive = false"
      @submit="createPortfolio"
    ></add-portfolio-modal-form>
    <button
      class="button is-light"
      @click="isAddPortfolioModalFormActive = true"
    >
      <span>Create new portfolio</span>
      <span class="icon is-small">
        <i class="fas fa-plus"></i>
      </span>
    </button>
  </page>
</template>

<script>
import Page from "../components/Page.vue";
import AddPortfolioModalForm from "../modalForms/AddPortfolio.vue";
import pageMixin from "../mixins/page";
import portfolioService from "../services/portfolioService";

export default {
  components: { Page, AddPortfolioModalForm },
  data() {
    return {
      portfolios: [],
      isAddPortfolioModalFormActive: false
    };
  },
  mixins: [pageMixin],
  async created() {
    try {
      const { data } = await portfolioService.getPortfolios(this.accessToken);
      this.portfolios = data;
    } catch (error) {
      this.notifyError("Unable to retrieve portfolios", error);
    }
  },
  methods: {
    async createPortfolio(portfolio) {
      this.isAddPortfolioModalFormActive = false;
      const userId = this.$store.state.auth.user.userId;

      try {
        const { data } = await portfolioService.createPortfolio(
          { ...portfolio, ownerId: userId },
          this.accessToken
        );

        await this.notifySuccess(
          "Portfolio created",
          `New portfolio "${data.name}" has been created successfully.`
        );
        this.$router.go();
      } catch (error) {
        this.notifyError("Unable to create portfolio", error);
      }
    },
    async deletePortfolio(portfolioId) {
      try {
        await portfolioService.deletePortfolio(portfolioId, this.accessToken);
        await this.notifySuccess(
          "Portfolio deleted",
          `Portfolio ${portfolioId} has been deleted.`
        );
        this.$router.go();
      } catch (error) {
        this.notifyError("Unable to delete portfolio", error);
      }
    }
  }
};
</script>
