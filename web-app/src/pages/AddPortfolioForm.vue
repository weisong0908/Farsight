<template>
  <page>
    <div class="columns">
      <div class="column">
        <div class="box">
          <form-field
            name="name"
            title="Portfolio Name"
            type="text"
            icon="fa-align-left"
            v-model="name"
            :errorMessage="validationErrors.name"
          ></form-field>
          <div class="field is-grouped">
            <div class="control">
              <button class="button is-primary" @click="create">
                Create Portfolio
              </button>
            </div>
          </div>
        </div>
      </div>
      <div class="column"></div>
    </div>
  </page>
</template>

<script>
import Page from "../components/Page.vue";
import FormField from "../components/FormField.vue";
import Joi from "joi";
import portfolioService from "../services/portfolioService";

const schema = Joi.object({
  name: Joi.string()
    .required()
    .label("Portfolio Name")
});

export default {
  components: { Page, FormField },
  data() {
    return {
      name: "",
      validationErrors: {}
    };
  },

  methods: {
    create() {
      if (!this.validate()) return;

      const accessToken = this.$store.state.auth.accessToken;
      const userId = this.$store.state.auth.user.userId;

      portfolioService
        .createPortfolio({ name: this.name, ownerId: userId }, accessToken)
        .then(resp => {
          this.$store
            .dispatch("alert/success", {
              heading: "Portfolio created",
              message: `New portfolio "${resp.data.name}" has been created successfully.`
            })
            .then(() => {
              this.$router.push({ name: "portfolios" });
            });
        })
        .catch(err => {
          const errorDescriptions = err.response
            ? err.response.data.map(d => d.description).join(" ")
            : "No connection";
          this.$store.dispatch("alert/danger", {
            heading: "Unable to create portfolio",
            message: errorDescriptions
          });
        });
    },
    validate() {
      const validationResults = schema.validate({
        name: this.name
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
