<template>
  <div :class="isActive ? 'modal is-active' : 'modal'">
    <div class="modal-background"></div>
    <div class="modal-card">
      <header class="modal-card-head">
        <p class="modal-card-title">{{ title }}</p>
        <button
          class="delete"
          aria-label="close"
          @click="$emit('close')"
        ></button>
      </header>
      <section class="modal-card-body">
        <slot name="form-fields"></slot>
      </section>
      <footer class="modal-card-foot">
        <slot name="form-buttons"></slot>
        <button class="button" @click="$emit('close')">Cancel</button>
      </footer>
    </div>
  </div>
</template>

<script>
import formMixin from "../mixins/form";

export default {
  props: ["isActive", "title"],
  data() {
    return {
      ticker: ""
    };
  },
  mixins: [formMixin]
};
</script>
