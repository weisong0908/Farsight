<template>
  <page>
    <div class="columns">
      <div class="column is-one-quarter">
        <aside class="menu">
          <p class="menu-label">
            Account Settings
          </p>
          <ul class="menu-list">
            <li>
              <router-link to="/myAccount">
                My Account
              </router-link>
            </li>
            <li>
              <router-link class="is-active" to="/changePassword">
                Change Password
              </router-link>
            </li>
          </ul>
        </aside>
      </div>
      <div class="column">
        <change-password-form
          :username="username"
          @submit="changePassword"
        ></change-password-form>
      </div>
    </div>
  </page>
</template>

<script>
import Page from "../components/Page.vue";
import ChangePasswordForm from "../normalForms/ChangePassword.vue";
import pageMixin from "../mixins/page";
import authService from "../services/authService";

export default {
  components: { Page, ChangePasswordForm },
  data() {
    return {
      userId: this.$store.state.auth.user.userId,
      username: this.$store.state.auth.user.username
    };
  },
  mixins: [pageMixin],
  methods: {
    async changePassword(credentials) {
      const accessToken = this.getAccessToken();

      try {
        const { data } = await authService.changePassword(
          {
            userId: this.userId,
            oldPassword: credentials.currentPassword,
            newPassword: credentials.newPassword
          },
          accessToken
        );
        await this.notifySuccess("Password changed", data);

        this.$router.replace({ name: "dashboard" });
      } catch (error) {
        this.notifyError("Unable to change password", error);
      }
    }
  }
};
</script>
