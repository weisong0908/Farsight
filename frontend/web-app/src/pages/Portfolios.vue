<template>
  <page>
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
              class="button is-danger"
              @click="deletePortfolio(portfolio.id)"
            >
              Delete
            </button>
          </td>
        </tr>
      </tbody>
    </table>
    <add-portfolio-modal-form
      title="Add New Portfolio"
      :isActive="isAddPortfolioModalFormActive"
      @close="isAddPortfolioModalFormActive = false"
      @submit="createPortfolio"
    ></add-portfolio-modal-form>
    <button
      class="button is-primary"
      @click="isAddPortfolioModalFormActive = true"
    >
      Create New Portfolio
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
  created() {
    portfolioService
      .getPortfolios(this.getAccessToken())
      .then(resp => {
        this.portfolios = resp.data;
      })
      .catch(err => {
        this.notifyError("Unable to retrieve portfolios", err);
      });
  },
  methods: {
    createPortfolio(portfolio) {
      this.isAddPortfolioModalFormActive = false;
      const accessToken = this.getAccessToken();
      const userId = this.$store.state.auth.user.userId;

      portfolioService
        .createPortfolio({ ...portfolio, ownerId: userId }, accessToken)
        .then(resp => {
          this.notifySuccess(
            "Portfolio created",
            `New portfolio "${resp.data.name}" has been created successfully.`
          ).then(() => {
            this.$router.go();
          });
        })
        .catch(err => {
          this.notifyError("Unable to create portfolio", err);
        });
    },
    deletePortfolio(portfolioId) {
      portfolioService
        .deletePortfolio(portfolioId, this.getAccessToken())
        .then(() => {
          this.notifySuccess(
            "Portfolio deleted",
            `Portfolio ${portfolioId} has been deleted.`
          ).then(() => {
            this.$router.go();
          });
        })
        .catch(err => {
          this.notifyError("Unable to delete portfolio", err);
        });
    }
  }
};
</script>
