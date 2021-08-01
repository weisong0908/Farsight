<template>
  <div class="box">
    <p class="subtitle">Confirm Reset Password</p>
    <form-field
      name="newPassword"
      title="New Password"
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
        <button class="button" @click="changePassword">
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
  newPassword: validationSchemas.newPassword,
  repeatedPassword: validationSchemas.repeatedPassword
});

export default {
  data() {
    return {
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
          newPassword: this.newPassword,
          repeatedPassword: this.repeatedPassword
        })
      )
        return;

      this.$emit("submit", this.newPassword);
    }
  }
};
</script>
