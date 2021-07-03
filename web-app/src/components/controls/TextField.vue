<template>
  <div class="field">
    <label :for="name" class="label">{{ title }}</label>
    <div class="control has-icons-left">
      <span class="icon is-small is-left">
        <i :class="'fas ' + icon"></i>
      </span>
      <input
        type="text"
        :id="name"
        :class="anyError ? 'input is-danger' : 'input'"
        :value="value"
        @input="$emit('input', $event.target.value)"
      />
    </div>
    <slot name="errorMessages"></slot>
  </div>
</template>

<script>
export default {
  props: ["name", "title", "value", "icon", "anyError"]
};
</script>
