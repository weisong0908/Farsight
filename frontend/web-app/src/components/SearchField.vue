<template>
  <div class="field is-fullwidth">
    <div class="label">{{ title }}</div>
    <div class="control is-expanded has-icons-right">
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
</template>

<script>
export default {
  props: ["title", "description"]
};
</script>
