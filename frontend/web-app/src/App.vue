<template>
  <div id="app">
    <navbar></navbar>
    <router-view></router-view>
  </div>
</template>

<script>
import Navbar from "./components/Navbar.vue";
import authService from "./services/authService";

export default {
  components: { Navbar },
  name: "App",
  async created() {
    if (this.$store.state.auth.isAuth) {
      await this.$store.dispatch("common/appIsNotReady");
      await this.getAccessToken();
      await this.$store.dispatch("common/appIsReady");
    }
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
