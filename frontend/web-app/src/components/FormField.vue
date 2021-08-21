<template>
  <div class="field">
    <label :for="name" class="label">{{ title }}</label>
    <div :class="icon ? 'control has-icons-left' : 'control'">
      <span v-if="icon !== undefined" class="icon is-small is-left">
        <i :class="'fas ' + icon"></i>
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
    <p class="help" v-if="helpMessage">{{ helpMessage }}</p>
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
    "errorMessage",
    "helpMessage",
    "readonly",
    "min",
    "step"
  ]
};
</script>
