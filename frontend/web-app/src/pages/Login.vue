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
import pageMixin from "../mixins/page";
import formMixin from "../mixins/form";
import Joi from "joi";
import authService from "../services/authService";
import validationSchemas from "../utils/validationSchemas";

const schema = Joi.object({
  username: validationSchemas.username,
  password: validationSchemas.password
});

export default {
  components: { Page, AppForm, FormField },
  data() {
    return {
      username: "",
      password: ""
    };
  },
  mixins: [pageMixin, formMixin],
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
              this.notifySuccess(
                "Logged in",
                `Welcome back, ${this.username}.`
              );
            })
            .then(() => {
              this.$router.push(this.$route.query.redirectTo || "/");
            });
        })
        .catch(err => {
          this.$store.dispatch("auth/logout").then(() => {
            console.log("ERR", err.response);
            this.notifyError("Unable to log in", err);
          });
        });
    }
  }
};
</script>
