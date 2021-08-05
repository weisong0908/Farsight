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
    console.log("created");
    if (this.$store.state.auth.isAuth) this.getAccessToken();
    else this.$router.push({ name: "login" });
  },
  methods: {
    async getAccessToken() {
      try {
        const { data } = await authService.refreshAuth(
          this.$store.state.auth.refreshToken
        );

        this.$store.dispatch("auth/login", data);

        this.$store.dispatch("auth/setSilentRefresh", data.expires_in * 1000);

        return;
      } catch (error) {
        this.notifyError("Error logging in", error);
        this.$store.dispatch("auth/logout");
        this.$router.push({ name: "login" });
      }
    }
  }
};
</script>
