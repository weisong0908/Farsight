<template>
  <div class="field">
    <label :for="name" class="label">{{ title }}</label>
    <div
      :class="
        icon || icon2 ? 'control has-icons-left has-icons-right' : 'control'
      "
    >
      <span v-if="icon !== undefined" class="icon is-small is-left">
        <i :class="'fas ' + icon"></i>
      </span>
      <span v-if="icon2 !== undefined" class="icon is-small is-right">
        <i :class="'fas ' + icon2"></i>
      </span>
      <input
        :type="type"
        :id="name"
        :value="value"
        :class="errorMessage ? 'input is-danger' : 'input'"
        @input="$emit('input', $event.target.value)"
        :readonly="readonly"
        :min="min"
        :step="step"
      />
    </div>
    <p class="help is-danger" v-if="errorMessage">{{ errorMessage }}</p>
  </div>
</template>

<script>
export default {
  props: [
    "name",
    "title",
    "value",
    "icon",
    "icon2",
    "type",
    "errorMessage",
    "readonly",
    "min",
    "step"
  ]
};
</script>
