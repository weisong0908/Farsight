<template>
  <div class="box">
    <p class="subtitle">Change Password</p>
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
    <br />
    <div class="field is-grouped">
      <div class="control">
        <button class="button is-primary" @click="changePassword">
          Change Password
        </button>
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
  currentPassword: validationSchemas.password,
  newPassword: validationSchemas.newPassword,
  repeatedPassword: validationSchemas.repeatedPassword
});

export default {
  props: ["username"],
  data() {
    return {
      currentPassword: "",
      newPassword: "",
      repeatedPassword: ""
    };
  },
  components: { FormField },
  mixins: [formMixin],
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

      this.$emit("submit", {
        username: this.username,
        currentPassword: this.currentPassword,
        newPassword: this.newPassword,
        repeatedPassword: this.repeatedPassword
      });
    }
  }
};
</script>
