<template>
  <page>
    <div class="columns">
      <div class="column">
        <sign-up-form @submit="signUp"></sign-up-form>
      </div>
      <div class="column"></div>
    </div>
  </page>
</template>

<script>
import Page from "../components/Page.vue";
import SignUpForm from "../normalForms/SignUp.vue";
import pageMixin from "../mixins/page";
import authService from "../services/authService";

export default {
  components: { Page, SignUpForm },
  mixins: [pageMixin],
  methods: {
    async signUp(account) {
      try {
        const resp = await authService.signup(
          account.username,
          account.newPassword,
          account.email
        );

        await this.notifySuccess("Signed up successfully", resp);

        this.$router.push(this.$route.query.redirectTo || "/");
      } catch (error) {
        this.notifyError("Unable to sign up", error);
      }
    }
  }
};
</script>
