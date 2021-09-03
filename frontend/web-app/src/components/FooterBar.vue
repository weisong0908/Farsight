<template>
  <nav class="footer navbar is-fixed-bottom level">
    <div class="level-left">
      <div class="level-item">
        <p class="icon-text">
          <span
            :class="
              isSystemHealthy ? 'icon has-text-success' : 'icon has-text-danger'
            "
          >
            <i class="fas fa-circle"></i>
          </span>
          <span>{{
            isSystemHealthy ? "System Healthy" : "System Unhealthy"
          }}</span>
        </p>
      </div>
      <div class="level-item">
        <div class="tags has-addons">
          <span class="tag is-dark">Version</span>
          <span class="tag is-info">{{ appVersion }}</span>
        </div>
      </div>
      <div class="level-item" v-if="appMode != 'Production'">
        <div class="tags has-addons">
          <span class="tag is-dark">Mode</span>
          <span class="tag is-warning">{{ appMode }}</span>
        </div>
      </div>
    </div>
    <div v-if="announcement !== ''" class="level-item">
      <div class="content">
        <p class="icon-text">
          <span class="icon">
            <i class="fas fa-exclamation"></i>
          </span>
          <span>
            <router-link v-if="announcement.url != ''" :to="announcement.url">
              {{ announcement.title }}
            </router-link>
            <template v-else>
              {{ announcement.title }}
            </template>
          </span>
        </p>
      </div>
    </div>
    <div class="level-right">
      <div class="level-item"></div>
    </div>
  </nav>
</template>

<script>
export default {
  data() {
    return {
      appMode: process.env.VUE_APP_MODE,
      appVersion: process.env.VUE_APP_VERSION,
      announcement: this.$store.state.common.announcement
    };
  },
  computed: {
    isSystemHealthy() {
      return this.$store.state.common.isSystemHealthy;
    }
  },
  created() {
    this.$store.commit("common/setAnnouncement", {
      title: "Check out your list of holdings",
      url: "/holdings"
    });
  }
};
</script>

<style lang="scss" scoped>
@import "~bulma/sass/utilities/_all";

$footer-padding: 0.5rem 1.5rem 0.75rem;

@import "~bulma";
</style>
