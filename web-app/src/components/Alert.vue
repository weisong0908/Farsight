<template>
  <article
    v-if="$store.state.alert.isActive"
    :class="'message is-' + $store.state.alert.type"
  >
    <div class="message-header">
      <p>{{ $store.state.alert.heading }}</p>
      <button class="delete" aria-label="delete" @click="hideAlert"></button>
    </div>
    <div class="message-body">
      {{ $store.state.alert.message }}
    </div>
  </article>
</template>

<script>
export default {
  methods: {
    hideAlert() {
      this.$store.dispatch("alert/hideAlert");
    }
  }
};
</script>
