<template>
  <page>
    <div class="columns">
      <div class="column">
        <field
          name="newPassword"
          title="New Password"
          :value="newPassword"
          icon="fa-lock"
          type="password"
          v-model="$v.newPassword.$model"
          :anyError="$v.newPassword.$anyError"
        >
          <template v-slot:errorMessages>
            <p class="help is-danger" v-if="!$v.newPassword.required">
              Password is required
            </p>
            <p class="help is-danger" v-if="!$v.newPassword.minLength">
              Password must have at least 6 characters
            </p>
            <p class="help is-danger" v-if="!$v.newPassword.containsUppercase">
              Password must have at least 1 uppercase character
            </p>
            <p class="help is-danger" v-if="!$v.newPassword.containsLowercase">
              Password must have at least 1 lowercase character
            </p>
            <p class="help is-danger" v-if="!$v.newPassword.containsNumber">
              Password must have at least 1 digit
            </p>
            <p class="help is-danger" v-if="!$v.newPassword.containsSpecial">
              Password must have at least 1 non-alphanumeric character e.g.
              special characters like '#' or '$'
            </p>
          </template>
        </field>
        <field
          name="confirmPassword"
          title="Please type your password again"
          :value="confirmPassword"
          icon="fa-lock"
          type="password"
          v-model="$v.confirmPassword.$model"
          :anyError="$v.confirmPassword.$anyError"
        >
          <template v-slot:errorMessages>
            <p class="help is-danger" v-if="!$v.confirmPassword.required">
              Password must be confirmed
            </p>
            <p class="help is-danger" v-if="!$v.confirmPassword.sameAsPassword">
              This value must be same as new password
            </p>
          </template></field
        >
        <div class="field is-grouped">
          <div class="control">
            <button class="button is-primary" @click="changePassword">
              Change Password
            </button>
          </div>
        </div>
      </div>
      <div class="column"></div>
    </div>
  </page>
</template>

<script>
import Page from "../components/Page.vue";
import Field from "../components/Field.vue";
import { required, minLength, sameAs } from "vuelidate/lib/validators";
import authService from "../services/authService";

export default {
  components: { Page, Field },
  data() {
    return {
      userId: this.$route.query.userId,
      token: this.$route.query.token,
      newPassword: "",
      confirmPassword: ""
    };
  },
  validations: {
    newPassword: {
      required,
      minLength: minLength(6),
      containsUppercase: function(value) {
        return /[A-Z]/.test(value);
      },
      containsLowercase: function(value) {
        return /[a-z]/.test(value);
      },
      containsNumber: function(value) {
        return /[0-9]/.test(value);
      },
      containsSpecial: function(value) {
        return /[#?!@$%^&*-]/.test(value);
      }
    },
    confirmPassword: {
      required,
      sameAsPassword: sameAs("newPassword")
    }
  },
  methods: {
    changePassword() {
      this.$v.$touch();
      if (!this.$v.$invalid) {
        authService
          .confirmResetPassword({
            userId: this.userId,
            token: this.token,
            newPassword: this.newPassword
          })
          .then(resp => {
            this.$store.dispatch("alert/success", {
              heading: "Password changed",
              message: resp.data
            });
            this.$router.replace({ name: "dashboard" });
          })
          .catch(err => {
            const errorDescriptions = err.response.data.map(d => d.description);
            this.$store.dispatch("alert/danger", {
              heading: "Error changing password",
              message: errorDescriptions.join(" ")
            });
          });
      }
    }
  }
};
</script>
