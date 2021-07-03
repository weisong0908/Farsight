<template>
  <page>
    <div class="columns">
      <div class="column">
        <field
          name="username"
          title="Username"
          :value="username"
          type="text"
          icon="fa-user"
          v-model="$v.username.$model"
          ><template v-slot:errorMessages>
            <p class="help is-danger" v-if="!$v.username.required">
              Username is required
            </p>
          </template></field
        >
        <field
          name="password"
          title="Password"
          :value="password"
          type="password"
          icon="fa-key"
          v-model="$v.password.$model"
        >
          <template v-slot:errorMessages>
            <p class="help is-danger" v-if="!$v.password.required">
              Password is required
            </p>
          </template></field
        >
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
import Field from "../components/Field.vue";
import authService from "../services/authService";
import { required } from "vuelidate/lib/validators";

export default {
  components: { Page, Field },
  data() {
    return {
      username: "",
      password: ""
    };
  },
  validations: {
    username: {
      required
    },
    password: {
      required
    }
  },
  methods: {
    login() {
      this.$v.$touch();
      if (!this.$v.$invalid) {
        authService
          .login(this.username, this.password)
          .then(resp => {
            this.$store.dispatch("auth/login", {
              username: this.username,
              data: resp.data
            });
            this.$store.dispatch("alert/success", {
              heading: "Logged in"
            });
            this.$router.push(this.$route.query.redirectTo || "/");
          })
          .catch(resp => {
            console.log(resp);
            localStorage.removeItem("accessToken");
            this.$store.dispatch("auth/logout");
            this.$store.dispatch("alert/danger", {
              heading: "Unable to log in",
              message: "Wrong username or password."
            });
          });
      }
    }
  }
};
</script>
