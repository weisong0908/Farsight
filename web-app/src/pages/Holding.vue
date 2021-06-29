<template>
  <page>
    <p>this is Holding {{ holdingId }}</p>
    <h1 class="title">{{ ticker }}</h1>
    <h2 class="subtitle">{{ details.name }}</h2>
    <figure class="image is-48x48">
      <img :src="details.logo" />
    </figure>
    <p>
      <span>{{ details.sector }}</span> -
      <span>{{ details.industry }}</span>
    </p>
    <p>{{ details.description }}</p>
    <table class="table">
      <thead>
        <tr>
          <th>Open</th>
          <th>Close</th>
          <th>High</th>
          <th>Low</th>
          <th>Volume</th>
        </tr>
      </thead>
      <tbody>
        <tr>
          <td>{{ stock.open }}</td>
          <td>{{ stock.close }}</td>
          <td>{{ stock.high }}</td>
          <td>{{ stock.low }}</td>
          <td>{{ stock.volume }}</td>
        </tr>
      </tbody>
    </table>
    <div>
      <canvas id="historicalPrice"></canvas>
    </div>
  </page>
</template>

<script>
import Page from "../components/Page.vue";
import stockService from "../services/stockService";
import charting from "../utils/charting";

export default {
  components: { Page },
  data() {
    return {
      holdingId: "",
      ticker: "AAPL",
      details: {
        logo: "https://bulma.io/images/placeholders/128x128.png",
        name: "",
        sector: "",
        industry: "",
        description: ""
      },
      stock: {
        open: "",
        close: "",
        high: "",
        low: "",
        volume: ""
      },
      trend: [],
      chartItem: {}
    };
  },
  created() {
    this.holdingId = this.$route.params.id;

    stockService.getDetails("AAPL").then(resp => {
      this.details.logo = resp.logo;
      this.details.name = resp.name;
      this.details.sector = resp.sector;
      this.details.industry = resp.industry;
      this.details.description = resp.description;
    });

    stockService.getPreviousPrice("AAPL").then(resp => {
      const result = resp.results[0];
      this.stock.open = result.o;
      this.stock.close = result.c;
      this.stock.high = result.h;
      this.stock.low = result.l;
      this.stock.volume = result.v;
    });
  },
  mounted() {
    stockService.getTrend("AAPL").then(resp => {
      const trend = resp.results.map(r => {
        return {
          value: r.c,
          cost: 100,
          date: new Date(r.t).toDateString()
        };
      });

      charting.plotPriceTrend("historicalPrice", trend);
    });
  }
};
</script>
