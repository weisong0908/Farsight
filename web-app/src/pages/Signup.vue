<template>
  <page>
    <div class="columns">
      <div class="column">
        <text-field
          name="username"
          title="User Name"
          :value="username"
          icon="fa-user"
          v-model="username"
        ></text-field>
        <text-field
          name="firstName"
          title="First Name"
          :value="firstName"
          icon="fa-align-left"
          v-model="firstName"
        ></text-field>
        <text-field
          name="lastName"
          title="Last Name"
          :value="lastName"
          icon="fa-align-left"
          v-model="lastName"
        ></text-field>
        <email-field
          name="email"
          title="Email Address"
          :value="email"
          icon="fa-envelope"
          v-model="email"
        ></email-field>
        <password-field
          name="password"
          title="Password"
          :value="password"
          icon="fa-lock"
          v-model="password"
        ></password-field>
        <password-field
          name="confirmPassword"
          title="Please type your password again"
          :value="confirmPassword"
          icon="fa-lock"
          v-model="confirmPassword"
        ></password-field>
        <div class="field is-grouped">
          <div class="control">
            <button class="button is-primary" @click="signup">Sign Up</button>
          </div>
        </div>
      </div>
      <div class="column"></div>
    </div>
  </page>
</template>

<script>
import Page from "../components/Page.vue";
import TextField from "../components/controls/TextField.vue";
import EmailField from "../components/controls/EmailField.vue";
import PasswordField from "../components/controls/PasswordField.vue";

export default {
  components: { Page, TextField, EmailField, PasswordField },
  data() {
    return {
      username: "",
      firstName: "",
      lastName: "",
      email: "",
      password: "",
      confirmPassword: ""
    };
  },
  methods: {
    signup() {
      this.$router.push(this.$route.query.redirectTo || "/");
    }
  }
};
</script>
