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
    changePassword(password) {
      authService
        .confirmResetPassword({
          userId: this.userId,
          token: this.token,
          newPassword: password
        })
        .then(resp => {
          this.notifySuccess("Password changed", resp.data).then(() => {
            this.$router.replace({ name: "dashboard" });
          });
        })
        .catch(err => {
          this.notifyError("Unable to change password", err);
        });
    }
  }
};
</script>
