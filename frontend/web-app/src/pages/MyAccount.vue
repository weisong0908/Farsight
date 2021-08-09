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
                User Information
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
          :displayName="displayName"
          :email="email"
          :isEmailVerified="isEmailVerified"
          :profilePicture="profilePicture"
          @resendEmailConfirmation="resendEmailConfirmation"
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
      displayName: "",
      email: "",
      isEmailVerified: false,
      profilePicture: ""
    };
  },
  mixins: [pageMixin],
  async created() {
    try {
      const { data } = await authService.getUserInfo(this.accessToken);
      this.email = data.email;
      this.isEmailVerified = data.email_verified;
      this.displayName = data.displayName;
      this.profilePicture = data.picture;
      this.isDataReady = true;
    } catch (error) {
      this.notifyError("Unable to retrieve user information", error);
    }
  },
  methods: {
    async resendEmailConfirmation(email) {
      alert(encodeURIComponent(email));
      try {
        await authService.resendEmailConfirmation(
          encodeURIComponent(email),
          this.accessToken
        );
      } catch (error) {
        this.notifyError("Unable to send email confirmation", error);
      }
    },
    async updateUserInfo(userInfo) {
      try {
        const { data } = await authService.updateUserInfo(
          {
            userId: this.userId,
            email: userInfo.email,
            displayName: userInfo.displayName,
            profilePicture: userInfo.profilePicture
          },
          this.accessToken
        );

        if (this.$store.state.auth.user.displayName != data.displayName)
          this.$store.commit("auth/setDisplayName", data.displayName);

        this.notifySuccess(
          "User information updated",
          `User information has been updated successfully. If there is an email address update, please confirm your email address by clicking the link sent to your new email.`
        );
      } catch (error) {
        this.notifyError("Unable to update user information", error);
      }
    }
  }
};
</script>
