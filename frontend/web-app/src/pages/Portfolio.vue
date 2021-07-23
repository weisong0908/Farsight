<template>
  <page>
    <div class="columns">
      <div class="column">
        <app-form title="Portfolio Information">
          <template v-slot:form-fields>
            <form-field
              name="portfolioId"
              title="Portfolio ID"
              type="text"
              icon="fa-dot-circle"
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
          </template>
          <template v-slot:form-buttons>
            <div class="control">
              <button class="button is-primary" @click="updatePortfolio">
                Update Portfolio
              </button>
            </div>
          </template>
        </app-form>
      </div>
      <div class="column">
        <p class="subtitle">Holdings</p>
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
import AppForm from "../components/Form.vue";
import FormField from "../components/FormField.vue";
import Pagination from "../components/Pagination.vue";
import pageMixin from "../mixins/page";
import formMixin from "../mixins/form";
import portfolioService from "../services/portfolioService";
import validationSchemas from "../utils/validationSchemas";
import Joi from "joi";

const schema = Joi.object({
  portfolioName: validationSchemas.portfolioName
});

export default {
  components: { Page, AppForm, FormField, Pagination },
  data() {
    return {
      portfolioId: this.$route.params.id,
      portfolioName: "",
      currentPageNumber: 1,
      pageSize: 2,
      holdings: []
    };
  },
  mixins: [pageMixin, formMixin],
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
      if (
        !this.validate(schema, {
          portfolioName: this.portfolioName
        })
      )
        return;

      const accessToken = this.$store.state.auth.accessToken;
      const userId = this.$store.state.auth.user.userId;
      portfolioService
        .updatePortfolio(
          { id: this.portfolioId, name: this.portfolioName, ownerId: userId },
          accessToken
        )
        .then(() => {
          this.notifySuccess(
            "Portfolio updated",
            `Portfolio "${this.portfolioName}" has been updated successfully.`
          );
        })
        .catch(err => {
          this.notifyError("Unable to update portfolio", err);
        });
    },
    goToPage(pageNumber) {
      this.currentPageNumber = pageNumber;
    }
  }
};
</script>
