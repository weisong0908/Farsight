<template>
  <div class="field">
    <label :for="name" class="label">{{ title }}</label>
    <div :class="icon !== undefined ? 'control has-icons-left' : 'control'">
      <span v-if="icon !== undefined" class="icon is-small is-left">
        <i :class="'fas ' + icon"></i>
      </span>
      <input
        :type="type"
        :id="name"
        :class="anyError ? 'input is-danger' : 'input'"
        :value="value"
        @input="$emit('input', $event.target.value)"
        :readonly="readonly"
      />
    </div>
    <slot name="errorMessages"></slot>
  </div>
</template>
<script>
export default {
  props: [
    "name",
    "title",
    "value",
    "icon",
    "type",
    "validation",
    "anyError",
    "readonly"
  ]
};
</script>
