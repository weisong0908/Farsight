<template>
  <div class="box">
    <p class="subtitle">Reset Password</p>
    <form-field
      name="email"
      title="Email"
      icon="fa-envelope"
      type="email"
      v-model="email"
      :errorMessage="validationErrors.email"
    ></form-field>
    <br />
    <div class="field is-grouped">
      <div class="control">
        <button class="button is-light" @click="resetPassword">
          Reset Password
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
  email: validationSchemas.email
});

export default {
  data() {
    return {
      email: ""
    };
  },
  components: { FormField },
  mixins: [formMixin],
  methods: {
    resetPassword() {
      if (
        !this.validate(schema, {
          email: this.email
        })
      )
        return;

      this.$emit("submit", this.email);
    }
  }
};
</script>
