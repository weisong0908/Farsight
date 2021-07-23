<template>
  <div :class="isActive ? 'modal is-active' : 'modal'">
    <div class="modal-background"></div>
    <div class="modal-card">
      <header class="modal-card-head">
        <p class="modal-card-title">Add New Portfolio</p>
        <button class="delete" aria-label="close" @click="close"></button>
      </header>
      <section class="modal-card-body">
        <form-field
          name="name"
          title="Portfolio Name"
          type="text"
          icon="fa-align-left"
          v-model="name"
          :errorMessage="validationErrors.name"
        ></form-field>
      </section>
      <footer class="modal-card-foot">
        <button class="button is-success" @click="submit">Save changes</button>
        <button class="button" @click="close">Cancel</button>
      </footer>
    </div>
  </div>
</template>

<script>
import FormField from "../components/FormField.vue";
import formMixin from "../mixins/form";
import Joi from "joi";
import validationSchemas from "../utils/validationSchemas";

const schema = Joi.object({
  name: validationSchemas.portfolioName
});

export default {
  props: ["isActive"],
  data() {
    return {
      name: ""
    };
  },
  mixins: [formMixin],
  components: { FormField },
  methods: {
    submit() {
      const portfolio = {
        name: this.name
      };

      if (!this.validate(schema, portfolio)) return;

      this.$emit("submit", portfolio);
    },
    close() {
      this.name = "";

      this.$emit("close");
    }
  }
};
</script>
