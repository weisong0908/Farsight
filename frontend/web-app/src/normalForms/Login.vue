<template>
  <div class="box">
    <p class="subtitle">Login</p>
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
      name="password"
      title="Password"
      icon="fa-lock"
      type="password"
      v-model="password"
      :errorMessage="validationErrors.password"
    ></form-field>
    <br />
    <div class="field is-grouped">
      <div class="control">
        <button class="button is-primary" @click="login">
          Login
        </button>
      </div>
      <div class="control">
        <router-link class="button is-text" :to="{ name: 'resetPassword' }"
          >Forgot password?</router-link
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
  password: validationSchemas.password
});

export default {
  data() {
    return {
      username: this.username,
      password: this.password
    };
  },
  components: { FormField },
  mixins: [formMixin],
  methods: {
    login() {
      if (
        !this.validate(schema, {
          username: this.username,
          password: this.password
        })
      )
        return;

      this.$emit("submit", {
        username: this.username,
        password: this.password
      });
    }
  }
};
</script>
