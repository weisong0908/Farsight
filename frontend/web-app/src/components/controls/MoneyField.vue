<template>
  <div class="field">
    <label :for="name" class="label">{{ title }}</label>
    <div class="control has-icons-left">
      <span class="icon is-small is-left">
        <i :class="'fas ' + icon"></i>
      </span>
      <input
        type="number"
        min="0"
        step="0.01"
        :id="name"
        class="input"
        :value="value"
        @input="$emit('input', $event.target.value)"
      />
    </div>
  </div>
</template>

<script>
export default {
  props: ["name", "title", "value", "icon"]
};
</script>
