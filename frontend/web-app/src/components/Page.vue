<template>
  <div class="section">
    <breadcrumb></breadcrumb>
    <h1 class="title">{{ title }}</h1>
    <alert></alert>
    <slot></slot>
  </div>
</template>

<script>
import Breadcrumb from "../components/Breadcrumb.vue";
import Alert from "../components/Alert.vue";
import pageMixin from "../mixins/page";

export default {
  components: {
    Alert,
    Breadcrumb
  },
  data() {
    return {
      title: ""
    };
  },
  mixins: [pageMixin],
  created() {
    this.title = this.$router.options.routes.find(
      r => r.name == this.$route.name
    ).title;
    document.title = process.env.VUE_APP_TITLE + " | " + this.title;
  }
};
</script>
