<template>
  <page>
    <div class="columns">
      <div class="column">
        <div class="box">
          <form-field
            name="username"
            title="Username"
            v-model="username"
            type="text"
            icon="fa-user"
            :readonly="true"
          ></form-field>
          <form-field
            name="currentPassword"
            title="Current Password"
            icon="fa-lock"
            type="password"
            v-model="currentPassword"
            :errorMessage="validationErrors.currentPassword"
          >
          </form-field>
          <form-field
            name="newPassword"
            title="New Password"
            icon="fa-lock"
            type="password"
            v-model="newPassword"
            :errorMessage="validationErrors.newPassword"
          >
          </form-field>
          <form-field
            name="repeatedPassword"
            title="Confirm Password"
            icon="fa-lock"
            type="password"
            v-model="repeatedPassword"
            :errorMessage="validationErrors.repeatedPassword"
          >
          </form-field>
          <div class="field is-grouped">
            <div class="control">
              <button class="button is-primary" @click="changePassword">
                Change Password
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
import authService from "../services/authService";

const schema = Joi.object({
  currentPassword: Joi.string()
    .required()
    .label("Current Password"),
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
  repeatedPassword: Joi.equal(Joi.ref("password"))
    .messages({
      "any.only": '"Repeated Password" must be same as "New Password"'
    })
    .label("Repeated Password")
});

export default {
  components: { Page, FormField },
  data() {
    return {
      userId: this.$store.state.auth.user.userId,
      username: this.$store.state.auth.user.username,
      currentPassword: "",
      newPassword: "",
      repeatedPassword: "",
      validationErrors: {}
    };
  },
  methods: {
    changePassword() {
      if (!this.validate()) return;

      const accessToken = this.$store.state.auth.accessToken;

      authService
        .changePassword(
          {
            userId: this.userId,
            oldPassword: this.currentPassword,
            newPassword: this.newPassword
          },
          accessToken
        )
        .then(resp => {
          this.$store.dispatch("alert/success", {
            heading: "Password changed",
            message: resp.data
          });
        })
        .catch(err => {
          const errorDescriptions = err.response
            ? err.response.data.map(d => d.description).join(" ")
            : "No connection";
          this.$store.dispatch("alert/danger", {
            heading: "Error changing password",
            message: errorDescriptions
          });
        });
    },
    validate() {
      const validationResults = schema.validate({
        currentPassword: this.currentPassword,
        newPassword: this.newPassword,
        repeatedPassword: this.repeatedPassword
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
