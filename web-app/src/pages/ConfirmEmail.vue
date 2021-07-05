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
    confirmEmail() {
      this.userId = this.$route.query.userId;
      this.token = this.$route.query.token;

      authService.confirmEmail(this.userId, this.token).then(resp => {
        this.$store.dispatch("alert/success", {
          heading: "Email is confirmed",
          message: resp.data
        });
        this.$router.replace({ name: "dashboard" });
      });
    }
  }
};
</script>
