<template>
  <div>
    <table class="table is-hoverable">
      <thead>
        <tr>
          <th>Name</th>
          <th>Quantity</th>
          <th>Cost</th>
          <th>Market Value</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="holding in rows" :key="holding.id">
          <td>
            <router-link
              :to="{
                name: 'holding',
                params: { id: holding.id }
              }"
            >
              {{ holding.name }}
            </router-link>
          </td>
          <td>{{ holding.quantity }}</td>
          <td>{{ holding.cost }}</td>
          <td>{{ holding.marketValue }}</td>
        </tr>
      </tbody>
    </table>
    <pagination
      :currentPageNumber="currentPageNumber"
      :totalPageCount="2"
      @goToPage="goToPage"
    ></pagination>
  </div>
</template>

<script>
import Pagination from "./Pagination.vue";

export default {
  components: { Pagination },
  props: ["holdings", "totalPageCount", "pageSize"],
  data() {
    return {
      currentPageNumber: 1
    };
  },
  computed: {
    rows() {
      return this.holdings.slice(
        (this.currentPageNumber - 1) * this.pageSize,
        this.currentPageNumber * this.pageSize
      );
    }
  },
  methods: {
    goToPage(pageNumber) {
      this.currentPageNumber = pageNumber;
    }
  }
};
</script>
