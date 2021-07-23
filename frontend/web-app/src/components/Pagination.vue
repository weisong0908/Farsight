<template>
  <nav class="pagination" role="navigation" aria-label="pagination">
    <a
      class="pagination-previous"
      :disabled="currentPageNumber === 1"
      @click="currentPageNumber === 1 ? true : goToPage(currentPageNumber - 1)"
      >Previous</a
    >
    <a
      class="pagination-next"
      :disabled="currentPageNumber >= totalPageCount"
      @click="
        currentPageNumber >= totalPageCount
          ? true
          : goToPage(currentPageNumber + 1)
      "
      >Next page</a
    >
    <ul class="pagination-list">
      <li v-if="currentPageNumber > 1">
        <a class="pagination-link" aria-label="Goto page 1" @click="goToPage(1)"
          >1</a
        >
      </li>
      <li v-if="currentPageNumber > 3">
        <span class="pagination-ellipsis">&hellip;</span>
      </li>
      <li v-if="currentPageNumber > 2">
        <a
          class="pagination-link"
          :aria-label="'Goto page ' + (currentPageNumber - 1)"
          @click="goToPage(currentPageNumber - 1)"
          >{{ currentPageNumber - 1 }}</a
        >
      </li>
      <li>
        <a
          class="pagination-link is-current"
          :aria-label="'Page ' + currentPageNumber"
          aria-current="page"
          @click="goToPage(currentPageNumber)"
          >{{ currentPageNumber }}</a
        >
      </li>
      <li v-if="currentPageNumber < totalPageCount - 1">
        <a
          class="pagination-link"
          :aria-label="'Goto page ' + (currentPageNumber + 1)"
          @click="goToPage(currentPageNumber + 1)"
          >{{ currentPageNumber + 1 }}</a
        >
      </li>
      <li v-if="currentPageNumber < totalPageCount - 2">
        <span class="pagination-ellipsis">&hellip;</span>
      </li>
      <li v-if="currentPageNumber < totalPageCount">
        <a
          class="pagination-link"
          :aria-label="'Goto page ' + totalPageCount"
          @click="goToPage(totalPageCount)"
          >{{ totalPageCount }}</a
        >
      </li>
    </ul>
  </nav>
</template>

<script>
export default {
  props: ["currentPageNumber", "totalPageCount"],
  methods: {
    goToPage(pageNumber) {
      this.$emit("goToPage", pageNumber);
    }
  }
};
</script>
