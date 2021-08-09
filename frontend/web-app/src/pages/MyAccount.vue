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
              <router-link class="is-active" to="/myAccount">
                My Account
              </router-link>
            </li>
            <li>
              <router-link to="/changePassword">
                Change Password
              </router-link>
            </li>
          </ul>
        </aside>
      </div>
      <div class="column">
        <edit-user-info-form
          v-if="isDataReady"
          :username="username"
          :email="email"
          :email_verified="email_verified"
          :profilePicture="profilePicture"
          @submit="updateUserInfo"
        ></edit-user-info-form>
      </div>
    </div>
  </page>
</template>

<script>
import Page from "../components/Page.vue";
import EditUserInfoForm from "../normalForms/EditUserInfo.vue";
import pageMixin from "../mixins/page";
import authService from "../services/authService";

export default {
  components: { Page, EditUserInfoForm },
  data() {
    return {
      userId: this.$store.state.auth.user.userId,
      username: this.$store.state.auth.user.username,
      email: "",
      email_verified: false,
      profilePicture: ""
    };
  },
  mixins: [pageMixin],
  async created() {
    try {
      const { data } = await authService.getUserInfo(this.accessToken);
      this.email = data.email;
      this.email_verified = data.email_verified;
      this.profilePicture = data.picture;
      this.isDataReady = true;
    } catch (error) {
      this.notifyError("Unable to retrieve user information", error);
    }
  },
  methods: {
    async updateUserInfo(userInfo) {
      try {
        await authService.updateUserInfo(
          { userId: this.userId, profilePicture: userInfo.profilePicture },
          this.accessToken
        );
        this.notifySuccess(
          "User information updated",
          `User information has been updated successfully.`
        );
      } catch (error) {
        this.notifyError("Unable to update user information", error);
      }
    }
  }
};
</script>
