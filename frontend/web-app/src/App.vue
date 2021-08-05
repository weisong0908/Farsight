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
  created() {
    if (this.$store.state.auth.isAuth) this.getAccessToken();
  },
  methods: {
    async getAccessToken() {
      const expiresAt = new Date(this.$store.state.auth.expiresAt);
      const now = new Date();
      if (expiresAt && expiresAt > now)
        return this.$store.state.auth.accessToken;

      try {
        const { data } = await authService.refreshAuth(
          this.$store.state.auth.refreshToken
        );

        this.$store.dispatch("auth/login", {
          username: data.username,
          data: data
        });

        return this.$store.state.auth.accessToken;
      } catch (error) {
        this.notifyError("Error logging in", error);
        this.$store.dispatch("auth/logout");
        this.$router.push({ name: "login" });
      }
    }
  }
};
</script>
