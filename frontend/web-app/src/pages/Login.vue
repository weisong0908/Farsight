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
    async login(account) {
      try {
        const { data } = await authService.login(
          account.username,
          account.password
        );
        await this.$store.dispatch("auth/login", data);

        this.$store.dispatch("auth/setSilentRefresh", data.expires_in * 1000);

        this.notifySuccess("Logged in", `Welcome back, ${account.username}.`);

        this.$router.push(this.$route.query.redirectTo || "/");
      } catch (error) {
        await this.$store.dispatch("auth/logout");
        this.notifyError("Unable to log in", error);
      }
    }
  }
};
</script>
