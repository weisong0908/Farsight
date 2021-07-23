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
        <app-form title="Change Password">
          <template v-slot:form-fields>
            <form-field
              name="username"
              title="Username"
              v-model="username"
              type="text"
              icon="fa-user"
              :readonly="true"
            ></form-field>
            <form-field
              name="currentPassword"
              title="Current Password"
              icon="fa-lock"
              type="password"
              v-model="currentPassword"
              :errorMessage="validationErrors.currentPassword"
            >
            </form-field>
            <form-field
              name="newPassword"
              title="New Password"
              icon="fa-lock"
              type="password"
              v-model="newPassword"
              :errorMessage="validationErrors.newPassword"
            >
            </form-field>
            <form-field
              name="repeatedPassword"
              title="Confirm Password"
              icon="fa-lock"
              type="password"
              v-model="repeatedPassword"
              :errorMessage="validationErrors.repeatedPassword"
            >
            </form-field>
          </template>
          <template v-slot:form-buttons>
            <div class="control">
              <button class="button is-primary" @click="changePassword">
                Change Password
              </button>
            </div>
          </template>
        </app-form>
      </div>
    </div>
  </page>
</template>

<script>
import Page from "../components/Page.vue";
import AppForm from "../components/Form.vue";
import FormField from "../components/FormField.vue";
import pageMixin from "../mixins/page";
import formMixin from "../mixins/form";
import Joi from "joi";
import authService from "../services/authService";
import validationSchemas from "../utils/validationSchemas";

const schema = Joi.object({
  currentPassword: validationSchemas.password,
  newPassword: validationSchemas.newPassword,
  repeatedPassword: validationSchemas.repeatedPassword
});

export default {
  components: { Page, AppForm, FormField },
  data() {
    return {
      userId: this.$store.state.auth.user.userId,
      username: this.$store.state.auth.user.username,
      currentPassword: "",
      newPassword: "",
      repeatedPassword: ""
    };
  },
  mixins: [pageMixin, formMixin],
  methods: {
    changePassword() {
      if (
        !this.validate(schema, {
          currentPassword: this.currentPassword,
          newPassword: this.newPassword,
          repeatedPassword: this.repeatedPassword
        })
      )
        return;

      const accessToken = this.$store.state.auth.accessToken;

      authService
        .changePassword(
          {
            userId: this.userId,
            oldPassword: this.currentPassword,
            newPassword: this.newPassword
          },
          accessToken
        )
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
