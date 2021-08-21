<template>
  <div class="box">
    <p class="subtitle">Edit Holding {{ holding.ticker }}</p>
    <form-field
      name="category"
      title="Category"
      type="text"
      icon="fa-align-left"
      v-model="categoryName"
      helpMessage="Type to create a new category or simply select the available options below:"
    ></form-field>
    <div class="tags">
      <span class="tag" v-for="category in categories" :key="category.id">
        <a @click="selectCategory(category)">
          {{ category.name }}
        </a>
        <button
          class="delete is-small"
          @click="removeCategory(category)"
        ></button>
      </span>
    </div>
    <br />
    <div class="field is-grouped">
      <div class="control">
        <button class="button is-light" @click="updateHolding">
          <span>Update holding</span>
          <span class="icon is-small">
            <i class="fas fa-edit"></i>
          </span>
        </button>
      </div>
    </div>
  </div>
</template>

<script>
import FormField from "../components/FormField.vue";
import formMixin from "../mixins/form";

export default {
  props: ["holding", "categories"],
  data() {
    return {
      categoryName: ""
    };
  },
  components: { FormField },
  mixins: [formMixin],
  created() {
    this.categoryName = this.holding.category ? this.holding.category.name : "";
  },
  methods: {
    updateHolding() {
      this.$emit("submit", { categoryName: this.categoryName });
    },
    selectCategory(category) {
      this.categoryName = category.name;
    },
    removeCategory(category) {
      this.$emit("removeCategory", category);
    }
  }
};
</script>
