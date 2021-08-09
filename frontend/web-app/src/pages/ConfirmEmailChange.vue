<template>
  <page></page>
</template>

<script>
import Page from "../components/Page.vue";
import pageMixin from "../mixins/page";
import authService from "../services/authService";

export default {
  components: { Page },
  data() {
    return {
      userId: "",
      email: "",
      token: ""
    };
  },
  mixins: [pageMixin],
  async created() {
    await this.confirmEmailChange();
  },
  methods: {
    async confirmEmailChange() {
      this.userId = this.$route.query.userId;
      this.email = this.$route.query.email;
      this.token = this.$route.query.token;

      try {
        const { data } = await authService.confirmEmailChange(
          this.userId,
          encodeURIComponent(this.email),
          this.token,
          this.accessToken
        );
        await this.$store.dispatch("alert/success", {
          heading: "Email is changed",
          message: `Your new email address is ${data}.`
        });
        this.$router.replace({ name: "dashboard" });
      } catch (error) {
        this.notifyError("Unable to change email", error);
      }
    }
  }
};
</script>
