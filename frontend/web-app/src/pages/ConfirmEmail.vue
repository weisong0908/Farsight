<template>
  <page></page>
</template>

<script>
import Page from "../components/Page.vue";
import authService from "../services/authService";

export default {
  components: { Page },
  data() {
    return {
      userId: "",
      token: ""
    };
  },
  created() {
    this.confirmEmail();
  },
  beforeRouteUpdate(to, from, next) {
    this.confirmEmail();
    next();
  },
  methods: {
    async confirmEmail() {
      this.userId = this.$route.query.userId;
      this.token = this.$route.query.token;

      try {
        const { data } = authService.confirmEmail(this.userId, this.token);
        await this.$store.dispatch("alert/success", {
          heading: "Email is confirmed",
          message: data
        });
        this.$router.replace({ name: "dashboard" });
      } catch (error) {
        this.notifyError("Unable to confirm email", error);
      }
    }
  }
};
</script>
