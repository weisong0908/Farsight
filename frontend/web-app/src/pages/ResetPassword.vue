<template>
  <page>
    <div class="columns">
      <div class="column">
        <reset-password-form @submit="resetPassword"></reset-password-form>
      </div>
      <div class="column"></div>
    </div>
  </page>
</template>

<script>
import Page from "../components/Page.vue";
import ResetPasswordForm from "../normalForms/ResetPassword.vue";
import pageMixin from "../mixins/page";
import authService from "../services/authService";

export default {
  components: { Page, ResetPasswordForm },
  mixins: [pageMixin],
  methods: {
    resetPassword(email) {
      authService
        .resetPassword(email)
        .then(resp => {
          this.notifySuccess("Please check your email to continue", resp.data);
        })
        .catch(err => {
          this.notifyError("Unable to reset password", err);
        });
    }
  }
};
</script>
