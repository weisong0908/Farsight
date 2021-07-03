<template>
  <page>
    <div class="columns">
      <div class="column">
        <field
          name="username"
          title="Username"
          :value="username"
          type="text"
          icon="fa-user"
          v-model="$v.username.$model"
          ><template v-slot:errorMessages>
            <p class="help is-danger" v-if="!$v.username.required">
              Username is required
            </p>
          </template></field
        >
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
        <field
          name="password"
          title="Password"
          :value="password"
          icon="fa-lock"
          type="password"
          v-model="$v.password.$model"
          :anyError="$v.password.$anyError"
        >
          <template v-slot:errorMessages>
            <p class="help is-danger" v-if="!$v.password.required">
              Password is required
            </p>
            <p class="help is-danger" v-if="!$v.password.minLength">
              Password must have at least 6 characters
            </p>
            <p class="help is-danger" v-if="!$v.password.containsUppercase">
              Password must have at least 1 uppercase character
            </p>
            <p class="help is-danger" v-if="!$v.password.containsLowercase">
              Password must have at least 1 lowercase character
            </p>
            <p class="help is-danger" v-if="!$v.password.containsNumber">
              Password must have at least 1 digit
            </p>
            <p class="help is-danger" v-if="!$v.password.containsSpecial">
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
              This value must be same as password
            </p>
          </template></field
        >
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
import Field from "../components/Field.vue";
import { required, email, minLength, sameAs } from "vuelidate/lib/validators";
import authService from "../services/authService";

export default {
  components: { Page, Field },
  data() {
    return {
      username: "",
      email: "",
      password: "",
      confirmPassword: ""
    };
  },
  validations: {
    username: {
      required
    },
    email: {
      required,
      email
    },
    password: {
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
      sameAsPassword: sameAs("password")
    }
  },
  methods: {
    signup() {
      this.$v.$touch();
      if (!this.$v.$invalid) {
        authService
          .signup(this.username, this.password, this.email)
          .then(resp => {
            console.log("signed up", resp);
            this.$router.push(this.$route.query.redirectTo || "/");
          });
      }
    }
  }
};
</script>
