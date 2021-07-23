<template>
  <div class="field">
    <label :for="name" class="label">{{ title }}</label>
    <div class="control">
      <div class="select">
        <select
          :id="name"
          @change="$emit('input', $event.target.value)"
          :value="value"
        >
          <option>Select dropdown</option>
          <option v-for="option in options" :key="option.id" :value="option.id">
            {{ option.title }}</option
          >
        </select>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  props: ["name", "title", "options", "value"]
};
</script>
