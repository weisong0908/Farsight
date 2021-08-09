<template>
  <div id="app">
    <navbar></navbar>
    <router-view></router-view>
    <footer-bar></footer-bar>
  </div>
</template>

<script>
import Navbar from "./components/Navbar.vue";
import FooterBar from "./components/FooterBar.vue";
import authService from "./services/authService";

export default {
  components: { Navbar, FooterBar },
  name: "App",
  async created() {
    if (this.$store.state.auth.isAuth) {
      await this.$store.dispatch("common/appIsNotReady");
      await this.getAccessToken();
    }
    await this.$store.dispatch("common/appIsReady");

    await this.$store.dispatch("common/clearSilentHealthCheck");
    this.$store.commit("common/setIsSystemHealthy", false);
    await this.$store.dispatch("common/setSilentHealthCheck");
  },
  async updated() {
    await this.$store.dispatch("common/closeNavbarBurgerMenu");
  },
  methods: {
    async getAccessToken() {
      const expiresAt = new Date(this.$store.state.auth.expiresAt);
      const now = new Date();
      if (expiresAt && expiresAt > now) return;

      try {
        const { data } = await authService.refreshAuth(
          this.$store.state.auth.refreshToken
        );

        this.$store.dispatch("auth/login", data);

        this.$store.dispatch("auth/setSilentRefresh", data.expires_in * 1000);
      } catch (error) {
        this.notifyError("Error logging in", error);
        this.$store.dispatch("auth/logout");
        this.$router.push({ name: "login" });
      }
    }
  }
};
</script>
