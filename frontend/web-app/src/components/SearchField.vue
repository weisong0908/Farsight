<template>
  <div v-if="!hasButton" class="field">
    <div class="label">{{ title }}</div>
    <div class="control has-icons-right">
      <span class="icon is-small is-right"><i class="fas fa-search"></i></span>
      <input
        type="search"
        placeholder="Search..."
        class="input"
        @input="$emit('input', $event.target.value)"
      />
    </div>
    <p class="help" v-if="description">{{ description }}</p>
  </div>
  <div v-else class="field has-addons">
    <div class="control">
      <input
        class="input"
        type="text"
        placeholder="Search..."
        @input="$emit('input', $event.target.value)"
      />
    </div>
    <div class="control">
      <button @click="$emit('search')" class="button is-light-outlined">
        Search
      </button>
    </div>
  </div>
</template>

<script>
export default {
  props: ["title", "description", "hasButton"]
};
</script>
