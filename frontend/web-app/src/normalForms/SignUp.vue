<template>
  <div class="box">
    <p class="subtitle">Sign Up</p>
    <form-field
      name="username"
      title="Username"
      v-model="username"
      type="text"
      icon="fa-user"
      :errorMessage="validationErrors.username"
    >
    </form-field>
    <form-field
      name="email"
      title="Email Address"
      v-model="email"
      type="email"
      icon="fa-envelope"
      :errorMessage="validationErrors.email"
    >
    </form-field>
    <form-field
      name="newPassword"
      title="Password"
      icon="fa-lock"
      type="password"
      v-model="newPassword"
      :errorMessage="validationErrors.newPassword"
    ></form-field>
    <form-field
      name="repeatedPassword"
      title="Confirm Password"
      icon="fa-lock"
      type="password"
      v-model="repeatedPassword"
      :errorMessage="validationErrors.repeatedPassword"
    ></form-field>
    <br />
    <div class="field is-grouped">
      <div class="control">
        <button class="button is-primary" @click="signUp">
          Sign Up
        </button>
      </div>
      <div class="control">
        <router-link class="button is-text" :to="{ name: 'login' }"
          >Already a member? Login here</router-link
        >
      </div>
    </div>
  </div>
</template>

<script>
import FormField from "../components/FormField.vue";
import formMixin from "../mixins/form";
import validationSchemas from "../utils/validationSchemas";
import Joi from "joi";

const schema = Joi.object({
  username: validationSchemas.username,
  email: validationSchemas.email,
  newPassword: validationSchemas.newPassword,
  repeatedPassword: validationSchemas.repeatedPassword
});

export default {
  data() {
    return {
      username: "",
      email: "",
      newPassword: "",
      repeatedPassword: ""
    };
  },
  components: { FormField },
  mixins: [formMixin],
  methods: {
    signUp() {
      if (
        !this.validate(schema, {
          username: this.username,
          email: this.email,
          newPassword: this.newPassword,
          repeatedPassword: this.repeatedPassword
        })
      )
        return;

      this.$emit("submit", {
        username: this.username,
        email: this.email,
        newPassword: this.newPassword,
        repeatedPassword: this.repeatedPassword
      });
    }
  }
};
</script>
