<template>
  <page>
    <p>this is Holding {{ holdingId }}</p>
  </page>
</template>

<script>
import Page from "../components/Page.vue";

export default {
  components: { Page },
  data() {
    return {
      holdingId: ""
    };
  },
  created() {
    this.holdingId = this.$route.params.id;
  }
};
</script>
