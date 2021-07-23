<template>
  <page>
    <div class="columns">
      <div class="column">
        <app-form title="Confirm Reset Password">
          <template v-slot:form-fields>
            <form-field
              name="newPassword"
              title="New Password"
              icon="fa-lock"
              type="password"
              v-model="newPassword"
              :errorMessage="validationErrors.newPassword"
            ></form-field>
            <form-field
              name="repeatedPassword"
              title="Confirm Password"
              icon="fa-lock"
              type="password"
              v-model="repeatedPassword"
              :errorMessage="validationErrors.repeatedPassword"
            ></form-field>
          </template>
          <template v-slot:form-buttons>
            <div class="control">
              <button class="button is-primary" @click="changePassword">
                Change Password
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
  newPassword: validationSchemas.newPassword,
  repeatedPassword: validationSchemas.repeatedPassword
});

export default {
  components: { Page, AppForm, FormField },
  data() {
    return {
      userId: this.$route.query.userId,
      token: this.$route.query.token,
      newPassword: "",
      repeatedPassword: ""
    };
  },
  mixins: [pageMixin, formMixin],
  methods: {
    changePassword() {
      if (
        !this.validate(schema, {
          newPassword: this.newPassword,
          repeatedPassword: this.repeatedPassword
        })
      )
        return;

      authService
        .confirmResetPassword({
          userId: this.userId,
          token: this.token,
          newPassword: this.newPassword
        })
        .then(resp => {
          this.notifySuccess("Password changed", resp.data).then(() => {
            this.$router.replace({ name: "dashboard" });
          });
        })
        .catch(err => {
          this.notifyError("Unable to change password", err);
        });
    }
  }
};
</script>
