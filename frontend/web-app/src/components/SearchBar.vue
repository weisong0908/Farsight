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
            :key="item[indexKey]"
            @click="selectItem(item)"
          >
            <p>{{ item[indexKey] }}</p>
            <p
              v-if="item[indexDescription]"
              class="has-text-grey has-text-weight-light is-size-7"
            >
              {{ item[indexDescription] }}
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
  props: [
    "title",
    "suggestions",
    "indexKey",
    "indexDescription",
    "errorMessage"
  ],
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
      this.isActive = true;

      this.filteredSuggestions = this.$store.getters["stock/filterStocks"](
        this.searchText.toLowerCase()
      );
    },
    selectItem(item) {
      this.searchText = item.ticker;
      this.isActive = false;
      this.$emit("input", item.ticker);
    }
  }
};
</script>
