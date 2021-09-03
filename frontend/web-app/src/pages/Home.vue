<template>
  <page>
    <div class="columns">
      <div class="column">
        <div class="content">
          <p class="subtitle">What is Farsight?</p>
          <p>Farsight is an investment portfolio tracker that:</p>
          <ul>
            <li>
              manages your holdings in each portfolio,
            </li>
            <li>
              displays stock/portfolio performance over time, and
            </li>
            <li>
              shows component allocation for each portfolio.
            </li>
          </ul>
          <p class="subtitle">Why Farsight?</p>
          <p>
            If the following statements sound like your investment style:
          </p>
          <ol>
            <li>
              You generally <b>buy and hold</b> stocks for a long period of time
            </li>
            <li>
              The allocation of stocks is important in your
              <b>portfolio management</b>
            </li>
            <li>You <b>rebalance</b> your portfolios over time</li>
          </ol>
          <p>
            This app is tailored for you to achieve your investment goals.
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
