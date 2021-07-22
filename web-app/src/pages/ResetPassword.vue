<template>
  <page>
    <div class="columns">
      <div class="column">
        <app-form title="Reset Password">
          <template v-slot:form-fields>
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
          </template>
          <template v-slot:form-buttons>
            <div class="control">
              <button class="button is-primary" @click="resetPassword">
                Reset Password
              </button>
            </div>
          </template>
        </app-form>
      </div>
      <div class="column"></div>
    </div>
  </page>
</template>

<script>
import Page from "../components/Page.vue";
import AppForm from "../components/Form.vue";
import FormField from "../components/FormField.vue";
import formMixin from "../mixins/form";
import Joi from "joi";
import authService from "../services/authService";

const schema = Joi.object({
  email: Joi.string()
    .email({ tlds: { allow: false } })
    .required()
    .label("Email address")
});

export default {
  components: { Page, AppForm, FormField },
  data() {
    return {
      email: ""
    };
  },
  mixins: [formMixin],
  methods: {
    resetPassword() {
      if (
        !this.validate(schema, {
          username: this.email
        })
      )
        return;

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
    }
  }
};
</script>
