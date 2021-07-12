<template>
  <page>
    <div class="columns">
      <div class="column">
        <field
          name="name"
          title="Portfolio Name"
          :value="name"
          type="text"
          icon="fa-align-left"
          v-model="$v.name.$model"
          ><template v-slot:errorMessages>
            <p class="help is-danger" v-if="!$v.name.required">
              Portfolio name is required
            </p>
          </template></field
        >
        <div class="field is-grouped">
          <div class="control">
            <button class="button is-primary" @click="create">
              Create Portfolio
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
import { required } from "vuelidate/lib/validators";
import portfolioService from "../services/portfolioService";

export default {
  components: { Page, Field },
  data() {
    return {
      name: ""
    };
  },
  validations: {
    name: {
      required
    }
  },
  methods: {
    create() {
      this.$v.$touch();
      if (!this.$v.$invalid) {
        const accessToken = this.$store.state.auth.accessToken;
        const userId = this.$store.state.auth.user.userId;

        portfolioService
          .createPortfolio({ name: this.name, ownerId: userId }, accessToken)
          .then(resp => {
            console.log("signed up", resp);
            this.$store
              .dispatch("alert/success", {
                heading: "Portfolio created",
                message: `New portfolio "${resp.data.name}" has been created successfully.`
              })
              .then(() => {
                this.$router.push({ name: "portfolios" });
              });
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
