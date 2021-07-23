<template>
  <nav class="breadcrumb" aria-label="breadcrumbs">
    <ul>
      <span class="icon">
        <i class="fas fa-compass"></i>
      </span>
      <li
        v-for="path in paths"
        :key="path.name"
        :class="path.isCurrent ? 'is-active' : ''"
      >
        <router-link :to="{ name: path.name }">{{ path.title }}</router-link>
      </li>
    </ul>
  </nav>
</template>

<script>
export default {
  data() {
    return {
      currentRouteName: "",
      paths: []
    };
  },
  created() {
    const routes = this.$router.options.routes;
    const currentRoute = routes.find(r => r.name === this.$route.name);
    this.paths.push({
      name: currentRoute.name,
      title: currentRoute.title,
      isCurrent: true
    });
    let parent = routes.find(r => r.name === currentRoute.parent);
    if (parent != null) {
      this.paths.splice(0, 0, {
        name: parent.name,
        title: parent.title
      });
      if (parent.parent != null) {
        parent = routes.find(r => r.name === parent.parent);
        if (parent != null) {
          this.paths.splice(0, 0, {
            name: parent.name,
            title: parent.title
          });
        }
      }
    }
  }
};
</script>
