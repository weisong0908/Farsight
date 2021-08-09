<template>
  <nav class="footer navbar is-fixed-bottom level">
    <div class="level-left">
      <div class="level-item">
        <p class="icon-text">
          <span
            :class="status ? 'icon has-text-success' : 'icon has-text-danger'"
          >
            <i class="fas fa-circle"></i>
          </span>
          <span>{{ status ? "Online" : "Offline" }}</span>
        </p>
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
      status: this.$store.state.common.isAppReady,
      announcement: this.$store.state.common.announcement
    };
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
