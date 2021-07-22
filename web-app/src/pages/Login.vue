<template>
  <page>
    <div class="columns">
      <div class="column">
        <app-form title="Login">
          <template v-slot:form-fields>
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
          </template>
          <template v-slot:form-buttons>
            <div class="control">
              <button class="button is-primary" @click="login">Log In</button>
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
  username: Joi.string()
    .required()
    .label("Username"),
  password: Joi.string()
    .required()
    .label("Password")
});

export default {
  components: { Page, AppForm, FormField },
  data() {
    return {
      username: "",
      password: ""
    };
  },
  mixins: [formMixin],
  methods: {
    login() {
      if (
        !this.validate(schema, {
          username: this.username,
          password: this.password
        })
      )
        return;
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
                heading: "Logged in",
                message: `Welcome back, ${this.username}.`
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
    }
  }
};
</script>
