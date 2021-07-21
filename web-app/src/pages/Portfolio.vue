<template>
  <page>
    <div class="columns">
      <div class="column">
        <div class="box">
          <form-field
            name="portfolioId"
            title="Portfolio ID"
            type="text"
            icon="fa-align-left"
            :value="portfolioId"
            :readonly="true"
          ></form-field>
          <form-field
            name="portfolioName"
            title="Portfolio Name"
            type="text"
            icon="fa-align-left"
            v-model="portfolioName"
            :errorMessage="validationErrors.portfolioName"
          ></form-field>
          <div class="field is-grouped">
            <div class="control">
              <button class="button is-primary" @click="updatePortfolio">
                Update Portfolio
              </button>
            </div>
          </div>
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
      <div class="column"></div>
    </div>
  </page>
</template>

<script>
import Page from "../components/Page.vue";
import FormField from "../components/FormField.vue";
import Pagination from "../components/Pagination.vue";
import portfolioService from "../services/portfolioService";
import Joi from "joi";

const schema = Joi.object({
  portfolioName: Joi.string()
    .required()
    .label("Portfolio name")
});

export default {
  components: { Page, FormField, Pagination },
  data() {
    return {
      portfolioId: this.$route.params.id,
      portfolioName: "",
      isDataReady: false,
      currentPageNumber: 1,
      pageSize: 2,
      holdings: [],
      validationErrors: {}
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

    portfolioService.getPortfolio(this.portfolioId, accessToken).then(resp => {
      this.isDataReady = true;
      this.portfolioName = resp.data.name;
      this.holdings = resp.data.holdings;
    });
  },
  methods: {
    updatePortfolio() {
      if (!this.validate()) return;
      const accessToken = this.$store.state.auth.accessToken;
      const userId = this.$store.state.auth.user.userId;
      portfolioService
        .updatePortfolio(
          { id: this.portfolioId, name: this.portfolioName, ownerId: userId },
          accessToken
        )
        .then(() => {
          this.$store.dispatch("alert/success", {
            heading: "Portfolio updated",
            message: `Portfolio "${this.portfolioName}" has been updated successfully.`
          });
        })
        .catch(err => {
          const errorDescriptions = err.response
            ? err.response.data.map(d => d.description).join(" ")
            : "No connection";
          this.$store.dispatch("alert/danger", {
            heading: "Unable to update portfolio",
            message: errorDescriptions
          });
        });
    },
    goToPage(pageNumber) {
      this.currentPageNumber = pageNumber;
    },
    validate() {
      const validationResults = schema.validate({
        portfolioName: this.portfolioName
      });
      this.validationErrors = {};
      if (validationResults.error) {
        for (let item of validationResults.error.details) {
          const validationError = {};
          validationError[item.path[0]] = item.message;

          this.validationErrors = {
            ...this.validationErrors,
            ...validationError
          };
        }
        return false;
      } else return true;
    }
  }
};
</script>
