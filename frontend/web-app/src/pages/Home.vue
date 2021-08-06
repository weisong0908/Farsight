<template>
  <page>
    <div class="columns">
      <div class="column">
        <div class="content">
          <p class="subtitle">What is Farsight?</p>
          <p>Farsight is an investment portfolio tracker that:</p>
          <ul>
            <li>
              manages holdings in each portfolio
            </li>
            <li>
              displays portfolio performance over time
            </li>
            <li>
              shows component allocation for each portfolio.
            </li>
          </ul>
          <p class="subtitle">Why Farsight?</p>
          <p>
            Lorem ipsum dolor sit amet consectetur adipisicing elit. Tenetur
            neque veniam nisi, earum culpa reiciendis esse cum exercitationem
            eveniet distinctio odio recusandae eos quas illum praesentium a
            dicta vitae. Quasi.
          </p>
        </div>
      </div>
      <div class="column">
        <sign-up-form @submit="signUp"></sign-up-form>
      </div>
    </div>
  </page>
</template>

<script>
import Page from "../components/Page.vue";
import SignUpForm from "../normalForms/SignUp.vue";
import authService from "../services/authService";

export default {
  components: { Page, SignUpForm },
  methods: {
    async signUp(account) {
      try {
        const resp = await authService.signup(
          account.username,
          account.newPassword,
          account.email
        );

        await this.notifySuccess("Signed up successfully", resp);

        this.$router.push(
          this.$route.query.redirectTo || { name: "dashboard" }
        );
      } catch (error) {
        this.notifyError("Unable to sign up", error);
      }
    }
  }
};
</script>
