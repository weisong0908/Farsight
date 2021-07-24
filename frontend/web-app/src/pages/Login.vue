<template>
  <page>
    <div class="columns">
      <div class="column">
        <login-form @submit="login"></login-form>
      </div>
      <div class="column"></div>
    </div>
  </page>
</template>

<script>
import Page from "../components/Page.vue";
import LoginForm from "../normalForms/Login.vue";
import pageMixin from "../mixins/page";
import authService from "../services/authService";

export default {
  components: { Page, LoginForm },
  mixins: [pageMixin],
  methods: {
    login(account) {
      authService
        .login(account.username, account.password)
        .then(resp => {
          this.$store
            .dispatch("auth/login", {
              username: account.username,
              data: resp.data
            })
            .then(() => {
              this.notifySuccess(
                "Logged in",
                `Welcome back, ${account.username}.`
              );
            })
            .then(() => {
              this.$router.push(this.$route.query.redirectTo || "/");
            });
        })
        .catch(err => {
          this.$store.dispatch("auth/logout").then(() => {
            this.notifyError("Unable to log in", err);
          });
        });
    }
  }
};
</script>
