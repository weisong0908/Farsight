<template>
  <page>
    <p>this is portfolio {{ portfolioId }}</p>
    <holding-list
      :holdings="holdings"
      :totalPageCount="2"
      :pageSize="3"
      :portfolioId="portfolioId"
    ></holding-list>
  </page>
</template>

<script>
import Page from "../components/Page.vue";
import HoldingList from "../components/HoldingList.vue";

export default {
  components: { Page, HoldingList },
  data() {
    return {
      portfolioId: "",
      holdings: [
        {
          id: 1,
          name: "Holding 1",
          quantity: 100,
          cost: 1000,
          marketValue: 1100
        },
        {
          id: 2,
          name: "Holding 2",
          quantity: 100,
          cost: 700,
          marketValue: 2100
        },
        {
          id: 3,
          name: "Holding 3",
          quantity: 10,
          cost: 100,
          marketValue: 390
        },
        {
          id: 4,
          name: "Holding 4",
          quantity: 10,
          cost: 100,
          marketValue: 390
        },
        {
          id: 5,
          name: "Holding 5",
          quantity: 10,
          cost: 100,
          marketValue: 390
        }
      ]
    };
  },
  created() {
    this.portfolioId = this.$route.params.id;
  }
};
</script>
