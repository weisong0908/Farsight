<template>
  <page>
    <div class="columns">
      <div class="column">
        <form-field
          name="email"
          title="Email Address"
          :value="email"
          type="email"
          icon="fa-envelope"
          v-model="email"
          :errorMessage="validationErrors.email"
        >
        </form-field>
        <div class="field is-grouped">
          <div class="control">
            <button class="button is-primary" @click="resetPassword">
              Reset Password
            </button>
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
import authService from "../services/authService";

const schema = Joi.object({
  email: Joi.string()
    .email({ tlds: { allow: false } })
    .required()
    .label("Email address")
});

export default {
  components: { Page, FormField },
  data() {
    return {
      email: "",
      validationErrors: {}
    };
  },
  methods: {
    resetPassword() {
      if (!this.validate()) return;

      authService
        .resetPassword(this.email)
        .then(resp => {
          this.$store.dispatch("alert/success", {
            heading: "Please check your email to continue",
            message: resp.data
          });
        })
        .catch(err => {
          const errorDescriptions = err.response.data.map(d => d.description);
          this.$store.dispatch("alert/danger", {
            heading: "Error resetting password",
            message: errorDescriptions.join(" ")
          });
        });
    },
    validate() {
      const validationResults = schema.validate({
        username: this.email
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
