<template>
  <page>
    <div class="columns">
      <div class="column">
        <text-field
          name="username"
          title="User Name"
          :value="username"
          icon="fa-user"
          v-model="username"
        ></text-field>
        <password-field
          name="password"
          title="Password"
          :value="password"
          icon="fa-key"
          v-model="password"
        ></password-field>
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
import TextField from "../components/controls/TextField.vue";
import PasswordField from "../components/controls/PasswordField.vue";
import authService from "../services/authService";

export default {
  components: { Page, TextField, PasswordField },
  data() {
    return {
      username: "",
      password: ""
    };
  },
  methods: {
    login() {
      authService
        .login(this.username, this.password)
        .then(resp => {
          console.log(resp);
          localStorage.setItem("accessToken", "1234");
          this.$store.dispatch("auth/login", { username: this.username });
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
};
</script>
