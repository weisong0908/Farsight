<template>
  <div class="field">
    <label for="ticker" class="label">Ticker</label>
    <div class="control has-icons-left">
      <span class="icon is-small is-left">
        <i class="fas fa-dot-circle"></i>
      </span>
      <input
        type="text"
        id="ticker"
        class="input"
        :value="value"
        @input="$emit('input', $event.target.value)"
      />
    </div>
  </div>
</template>

<script>
export default {
  props: ["value"]
};
</script>
