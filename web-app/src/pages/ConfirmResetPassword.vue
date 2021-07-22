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
              :value="repeatedPassword"
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
import formMixin from "../mixins/form";
import Joi from "joi";
import authService from "../services/authService";

const schema = Joi.object({
  newPassword: Joi.string()
    .min(6)
    .required()
    .pattern(/[A-Z]/, "containsUppercase")
    .pattern(/[a-z]/, "containsLowercase")
    .pattern(/[0-9]/, "containsNumber")
    .pattern(/[#?!@$%^&*-]/, "containsSpecial")
    .messages({
      "string.pattern.name":
        '"New Password" must contains at least 1 upper case, at least 1 lower case, at least 1 digit, and at least 1 non-alphanumeric character e.g. special characters like "#" or "$"'
    })
    .label("New Password"),
  repeatedPassword: Joi.equal(Joi.ref("newPassword"))
    .messages({
      "any.only": '"Repeated Password" must be same as "New Password"'
    })
    .label("Repeated Password")
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
  mixins: [formMixin],
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
          this.$store.dispatch("alert/success", {
            heading: "Password changed",
            message: resp.data
          });
          this.$router.replace({ name: "dashboard" });
        })
        .catch(err => {
          const errorDescriptions = err.response.data.map(d => d.description);
          this.$store.dispatch("alert/danger", {
            heading: "Error changing password",
            message: errorDescriptions.join(" ")
          });
        });
    }
  }
};
</script>
