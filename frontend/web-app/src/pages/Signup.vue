<template>
  <page>
    <div class="columns">
      <div class="column">
        <app-form title="Sign Up">
          <template v-slot:form-fields>
            <form-field
              name="username"
              title="Username"
              v-model="username"
              type="text"
              icon="fa-user"
              :errorMessage="validationErrors.username"
            >
            </form-field>
            <form-field
              name="email"
              title="Email Address"
              v-model="email"
              type="email"
              icon="fa-envelope"
              :errorMessage="validationErrors.email"
            >
            </form-field>
            <form-field
              name="newPassword"
              title="Password"
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
              <button class="button is-primary" @click="signup">Sign Up</button>
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
  username: validationSchemas.username,
  email: validationSchemas.email,
  newPassword: validationSchemas.newPassword,
  repeatedPassword: validationSchemas.repeatedPassword
});

export default {
  components: { Page, AppForm, FormField },
  data() {
    return {
      username: "s",
      email: "",
      newPassword: "",
      repeatedPassword: ""
    };
  },
  mixins: [pageMixin, formMixin],
  methods: {
    signup() {
      if (
        !this.validate(schema, {
          username: this.username,
          email: this.email,
          newPassword: this.newPassword,
          repeatedPassword: this.repeatedPassword
        })
      )
        return;
      authService
        .signup(this.username, this.newPassword, this.email)
        .then(resp => {
          this.notifySuccess("Signed up successfully", resp).then(() => {
            this.$router.push(this.$route.query.redirectTo || "/");
          });
        })
        .catch(err => {
          this.notifyError("Unable to sign up", err);
        });
    }
  }
};
</script>
