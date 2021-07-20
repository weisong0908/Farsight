<template>
  <page>
    <div class="columns">
      <div class="column">
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
          name="password"
          title="Password"
          :value="password"
          icon="fa-lock"
          type="password"
          v-model="password"
          :errorMessage="validationErrors.password"
        ></form-field>
        <form-field
          name="repeatedPassword"
          title="Confirm Password"
          :value="repeatedPassword"
          icon="fa-lock"
          type="password"
          v-model="repeatedPassword"
          :errorMessage="validationErrors.repeatedPassword"
        ></form-field>
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
import FormField from "../components/FormField.vue";
import Joi from "joi";
import authService from "../services/authService";

const schema = Joi.object({
  username: Joi.string()
    .required()
    .label("Username"),
  email: Joi.string()
    .email({ tlds: { allow: false } })
    .required()
    .label("Email address"),
  password: Joi.string()
    .min(6)
    .required()
    .pattern(/[A-Z]/, "containsUppercase")
    .pattern(/[a-z]/, "containsLowercase")
    .pattern(/[0-9]/, "containsNumber")
    .pattern(/[#?!@$%^&*-]/, "containsSpecial")
    .messages({
      "string.pattern.name":
        '"Password" must contains at least 1 upper case, at least 1 lower case, at least 1 digit, and at least 1 non-alphanumeric character e.g. special characters like "#" or "$"'
    })
    .label("Password"),
  repeatedPassword: Joi.equal(Joi.ref("password"))
    .messages({
      "any.only": '"Repeated Password" must be same as "Password"'
    })
    .label("Repeated Password")
});

export default {
  components: { Page, FormField },
  data() {
    return {
      username: "s",
      email: "",
      password: "",
      repeatedPassword: "",
      validationErrors: {}
    };
  },
  methods: {
    signup() {
      if (!this.validate()) return;
      authService
        .signup(this.username, this.password, this.email)
        .then(resp => {
          console.log("signed up", resp);
          this.$router.push(this.$route.query.redirectTo || "/");
        })
        .catch(err => {
          const errorDescriptions = err.response
            ? err.response.data.map(d => d.description).join(" ")
            : "No connection";
          this.$store.dispatch("alert/danger", {
            heading: "Unable to sign up",
            message: errorDescriptions
          });
        });
    },
    validate() {
      const validationResults = schema.validate({
        username: this.username,
        email: this.email,
        password: this.password,
        repeatedPassword: this.repeatedPassword
      });
      this.validationErrors = {};
      if (validationResults.error) {
        for (let item of validationResults.error.details) {
          const validationError = {};
          validationError[item.path[0]] = item.message;

          this.validationErrors = {
            ...this.validationErrors,
            ...validationError
          };
        }
        return false;
      } else return true;
    }
  }
};
</script>
