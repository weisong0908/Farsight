<template>
  <div class="field">
    <label :for="name" class="label">{{ title }}</label>
    <div class="control">
      <textarea
        rows="3"
        :id="name"
        class="textarea"
        :value="value"
        @input="$emit('input', $event.target.value)"
      ></textarea>
    </div>
  </div>
</template>

<script>
export default {
  props: ["name", "title", "value"]
};
</script>
