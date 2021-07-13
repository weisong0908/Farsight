<template>
  <page>
    <p>this is portfolio {{ portfolioId }}</p>
    <holding-list
      :holdings="fakeHoldings"
      :totalPageCount="2"
      :pageSize="3"
      :portfolioId="portfolioId"
    ></holding-list>
    <br /><br />
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
        <tr v-for="holding in holdings" :key="holding.id">
          <td>
            <router-link
              :to="{
                name: 'holding',
                params: { id: holding.id }
              }"
            >
              {{ holding.ticker }}
            </router-link>
          </td>
          <td>{{ holding.quantity }}</td>
          <td>{{ holding.cost }}</td>
          <td>{{ holding.marketValue }}</td>
        </tr>
      </tbody>
    </table>
  </page>
</template>

<script>
import Page from "../components/Page.vue";
import HoldingList from "../components/HoldingList.vue";
import holdingService from "../services/holdingService";

export default {
  components: { Page, HoldingList },
  data() {
    return {
      portfolioId: this.$route.params.id,
      fakeHoldings: [
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
      ],
      holdings: []
    };
  },
  created() {
    const accessToken = this.$store.state.auth.accessToken;

    holdingService.getHoldings(this.portfolioId, accessToken).then(resp => {
      this.holdings = resp.data;
    });
  }
};
</script>
