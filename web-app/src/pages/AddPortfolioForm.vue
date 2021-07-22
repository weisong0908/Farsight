<template>
  <page>
    <div class="columns">
      <div class="column">
        <app-form title="New Portfolio">
          <template v-slot:form-fields>
            <form-field
              name="name"
              title="Portfolio Name"
              type="text"
              icon="fa-align-left"
              v-model="name"
              :errorMessage="validationErrors.name"
            ></form-field>
          </template>
          <template v-slot:form-buttons>
            <div class="control">
              <button class="button is-primary" @click="create">
                Create Portfolio
              </button>
            </div>
          </template>
        </app-form>
      </div>
      <div class="column"></div>
    </div>
  </page>
</template>

<script>
import Page from "../components/Page.vue";
import AppForm from "../components/Form.vue";
import FormField from "../components/FormField.vue";
import pageMixin from "../mixins/page";
import formMixin from "../mixins/form";
import Joi from "joi";
import portfolioService from "../services/portfolioService";
import validationSchemas from "../utils/validationSchemas";

const schema = Joi.object({
  name: validationSchemas.portfolioName
});

export default {
  components: { Page, AppForm, FormField },
  data() {
    return {
      name: ""
    };
  },
  mixins: [pageMixin, formMixin],
  methods: {
    create() {
      if (
        !this.validate(schema, {
          name: this.name
        })
      )
        return;

      const accessToken = this.$store.state.auth.accessToken;
      const userId = this.$store.state.auth.user.userId;

      portfolioService
        .createPortfolio({ name: this.name, ownerId: userId }, accessToken)
        .then(resp => {
          this.notifySuccess(
            "Portfolio created",
            `New portfolio "${resp.data.name}" has been created successfully.`
          ).then(() => {
            this.$router.push({ name: "portfolios" });
          });
        })
        .catch(err => {
          this.notifyError("Unable to create portfolio", err);
        });
    }
  }
};
</script>
