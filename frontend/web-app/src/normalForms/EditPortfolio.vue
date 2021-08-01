<template>
  <div class="box">
    <p class="subtitle">Edit Portfolio</p>
    <form-field
      name="portfolioId"
      title="Portfolio ID"
      type="text"
      icon="fa-dot-circle"
      v-model="portfolio.id"
      :readonly="true"
    ></form-field>
    <form-field
      name="portfolioName"
      title="Portfolio Name"
      type="text"
      icon="fa-align-left"
      v-model="portfolio.name"
      :errorMessage="validationErrors.portfolioName"
    ></form-field>
    <br />
    <div class="field is-grouped">
      <div class="control">
        <button class="button" @click="updatePortfolio">
          Update Portfolio
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
  portfolioName: validationSchemas.portfolioName
});

export default {
  props: ["portfolioId", "portfolioName"],
  data() {
    return {
      portfolio: {
        id: this.portfolioId,
        name: this.portfolioName
      }
    };
  },
  components: { FormField },
  mixins: [formMixin],
  methods: {
    updatePortfolio() {
      if (
        !this.validate(schema, {
          portfolioName: this.portfolio.name
        })
      )
        return;

      this.$emit("submit", this.portfolio);
    }
  }
};
</script>
