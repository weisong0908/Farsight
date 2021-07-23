<template>
  <page>
    <div class="columns">
      <div class="column">
        <app-form title="Reset Password">
          <template v-slot:form-fields>
            <form-field
              name="email"
              title="Email Address"
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
import pageMixin from "../mixins/page";
import formMixin from "../mixins/form";
import Joi from "joi";
import authService from "../services/authService";
import validationSchemas from "../utils/validationSchemas";

const schema = Joi.object({
  email: validationSchemas.email
});

export default {
  components: { Page, AppForm, FormField },
  data() {
    return {
      email: ""
    };
  },
  mixins: [pageMixin, formMixin],
  methods: {
    resetPassword() {
      if (
        !this.validate(schema, {
          email: this.email
        })
      )
        return;

      authService
        .resetPassword(this.email)
        .then(resp => {
          this.notifySuccess("Please check your email to continue", resp.data);
        })
        .catch(err => {
          this.notifyError("Unable to reset password", err);
        });
    }
  }
};
</script>
