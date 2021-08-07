<template>
  <page>
    <div class="columns">
      <div class="column">
        <confirm-reset-password
          @submit="changePassword"
        ></confirm-reset-password>
      </div>
      <div class="column"></div>
    </div>
  </page>
</template>

<script>
import Page from "../components/Page.vue";
import ConfirmResetPassword from "../normalForms/ConfirmResetPassword.vue";
import pageMixin from "../mixins/page";
import authService from "../services/authService";

export default {
  components: { Page, ConfirmResetPassword },
  data() {
    return {
      userId: this.$route.query.userId,
      token: this.$route.query.token
    };
  },
  mixins: [pageMixin],
  methods: {
    async changePassword(password) {
      try {
        const { data } = await authService.confirmResetPassword({
          userId: this.userId,
          token: this.token,
          newPassword: password
        });

        await this.notifySuccess("Password changed", data);

        this.$router.replace({ name: "login" });
      } catch (error) {
        this.notifyError("Unable to change password", error);
      }
    }
  }
};
</script>
