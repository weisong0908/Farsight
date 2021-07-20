<template>
  <page>
    <div class="columns">
      <div class="column">
        <form-field
          name="username"
          title="Username"
          type="text"
          icon="fa-user"
          v-model="username"
          :errorMessage="validationErrors.username"
        ></form-field>
        <form-field
          name="password"
          title="Password"
          :value="password"
          type="password"
          icon="fa-key"
          v-model="password"
          :errorMessage="validationErrors.password"
        >
        </form-field>
        <div class="field is-grouped">
          <div class="control">
            <button class="button is-primary" @click="login">Log In</button>
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
  username: Joi.string()
    .required()
    .label("Username"),
  password: Joi.string()
    .required()
    .label("Password")
});

export default {
  components: { Page, FormField },
  data() {
    return {
      username: "",
      password: "",
      validationErrors: {}
    };
  },

  methods: {
    login() {
      if (!this.validate()) return;
      authService
        .login(this.username, this.password)
        .then(resp => {
          this.$store
            .dispatch("auth/login", {
              username: this.username,
              data: resp.data
            })
            .then(() => {
              this.$store.dispatch("alert/success", {
                heading: "Logged in"
              });
            })
            .then(() => {
              this.$router.push(this.$route.query.redirectTo || "/");
            });
        })
        .catch(() => {
          this.$store.dispatch("auth/logout").then(() => {
            this.$store.dispatch("alert/danger", {
              heading: "Unable to log in",
              message: "Incorrect username or password."
            });
          });
        });
    },
    validate() {
      const validationResults = schema.validate({
        username: this.username,
        password: this.password
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
