<template>
  <div class="field is-fullwidth" @blur="isActive = false">
    <div class="label">{{ title }}</div>
    <div :class="isActive ? 'dropdown is-active' : 'dropdown'">
      <div class="dropdown-trigger" @click="isActive = !isActive">
        <div class="control is-expanded has-icons-right">
          <input
            class="input"
            type="search"
            placeholder="Search..."
            v-model="searchText"
            @input="filterSuggestions"
          />
          <span class="icon is-small is-right"
            ><i class="fas fa-search"></i
          ></span>
        </div>
      </div>
      <div class="dropdown-menu" id="dropdown-menu" role="menu">
        <div class="dropdown-content">
          <a
            class="dropdown-item"
            v-for="item in filteredSuggestions"
            :key="item.value"
            @click="selectItem(item)"
          >
            <p>{{ item.value }}</p>
            <p
              v-if="item.description"
              class="has-text-grey has-text-weight-light is-size-7"
            >
              {{ item.description }}
            </p>
          </a>
        </div>
      </div>
    </div>
    <p class="help is-danger" v-if="errorMessage">{{ errorMessage }}</p>
  </div>
</template>

<script>
export default {
  props: ["title", "suggestions", "errorMessage"],
  data() {
    return {
      isActive: false,
      searchText: "",
      filteredSuggestions: []
    };
  },
  created() {
    this.filteredSuggestions = this.suggestions;
  },
  methods: {
    filterSuggestions() {
      console.log("filterSuggestions");
      this.filteredSuggestions = this.suggestions.filter(o =>
        o.value.toLowerCase().includes(this.searchText.toLowerCase())
      );
    },
    selectItem(item) {
      this.searchText = item.value;
      this.isActive = false;
      this.$emit("input", item.value);
    }
  }
};
</script>
