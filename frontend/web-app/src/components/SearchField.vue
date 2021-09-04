<template>
  <div v-if="hasButton" class="field has-addons">
    <div class="control">
      <input
        class="input"
        type="text"
        :placeholder="description"
        @input="$emit('input', $event.target.value)"
      />
    </div>
    <div class="control">
      <button @click="$emit('search')" class="button is-light-outlined">
        Search
      </button>
    </div>
  </div>
  <div v-else class="field">
    <div class="control has-icons-right">
      <span class="icon is-small is-right"><i class="fas fa-search"></i></span>
      <input
        type="search"
        :placeholder="description"
        class="input"
        @input="$emit('input', $event.target.value)"
      />
    </div>
  </div>
</template>

<script>
export default {
  props: ["description", "hasButton"]
};
</script>
