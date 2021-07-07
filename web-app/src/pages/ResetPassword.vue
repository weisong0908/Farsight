<template>
  <page>
    <div class="columns">
      <div class="column">
        <field
          name="email"
          title="Email Address"
          :value="email"
          type="email"
          icon="fa-envelope"
          v-model="$v.email.$model"
          :anyError="$v.email.$anyError"
        >
          <template v-slot:errorMessages>
            <p class="help is-danger" v-if="!$v.email.required">
              Field is required
            </p>
            <p class="help is-danger" v-if="!$v.email.email">
              Field is not a valid email address
            </p>
          </template></field
        >
        <div class="field is-grouped">
          <div class="control">
            <button class="button is-primary" @click="resetPassword">
              Reset Password
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
import { required, email } from "vuelidate/lib/validators";
import authService from "../services/authService";

export default {
  components: { Page, Field },
  data() {
    return {
      email: ""
    };
  },
  validations: {
    email: {
      required,
      email
    }
  },
  methods: {
    resetPassword() {
      this.$v.$touch();
      if (!this.$v.$invalid) {
        authService
          .resetPassword(this.email)
          .then(resp => {
            this.$store.dispatch("alert/success", {
              heading: "Please check your email to continue",
              message: resp.data
            });
          })
          .catch(err => {
            const errorDescriptions = err.response.data.map(d => d.description);
            this.$store.dispatch("alert/danger", {
              heading: "Error resetting password",
              message: errorDescriptions.join(" ")
            });
          });
      }
    }
  }
};
</script>
