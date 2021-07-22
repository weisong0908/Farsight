<template>
  <div class="box">
    <p class="subtitle">{{ title }}</p>
    <slot name="form-fields"></slot>
    <br />
    <div class="field is-grouped">
      <slot name="form-buttons"></slot>
    </div>
  </div>
</template>

<script>
import formMixin from "../mixins/form";

export default {
  props: ["title"],
  mixins: [formMixin]
};
</script>
