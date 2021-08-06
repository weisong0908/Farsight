<template>
  <page>
    <div class="columns">
      <div class="column">
        <div class="box">
          <div class="content">
            <p class="subtitle">Portfolio Summary</p>
            <p class="has-text-grey">{{ portfolioName }}</p>
          </div>
          <nav class="level">
            <div class="level-item has-text-centered">
              <div>
                <p class="heading">Market Value</p>
                <p class="title">{{ portfolioMarketValue.toFixed(2) }}</p>
              </div>
            </div>
            <div class="level-item has-text-centered">
              <div>
                <p class="heading">Cost</p>
                <p class="title">{{ portfolioCost.toFixed(2) }}</p>
              </div>
            </div>
            <div class="level-item has-text-centered">
              <div>
                <p class="heading">Profit/Loss</p>
                <p
                  :class="
                    portfolioReturn < 0
                      ? 'title has-text-danger'
                      : 'title has-text-success'
                  "
                >
                  <span> {{ portfolioReturn.toFixed(2) }}</span>
                  <span>
                    ({{
                      ((portfolioReturn / portfolioCost) * 100).toFixed(2)
                    }}%)
                  </span>
                </p>
              </div>
            </div>
          </nav>
          <nav class="level">
            <div class="level-item has-text-centered">
              <div>
                <p class="heading">Number of Holdings</p>
                <p class="title">{{ holdings.length }}</p>
              </div>
            </div>
            <div class="level-item has-text-centered">
              <div>
                <p class="heading">Number of Types</p>
                <p class="title">
                  {{ groupHoldingsBy("type").length }}
                </p>
              </div>
            </div>
            <div class="level-item has-text-centered">
              <div>
                <p class="heading">Number of Sectors</p>
                <p class="title">
                  {{ groupHoldingsBy("sector").length }}
                </p>
              </div>
            </div>
          </nav>
        </div>
      </div>
      <div class="column">
        <edit-portfolio-form
          v-if="isDataReady"
          :portfolioId="portfolioId"
          :portfolioName="portfolioName"
          @submit="updatePortfolio"
        ></edit-portfolio-form>
      </div>
    </div>
    <div class="columns">
      <div class="column">
        <div class="box">
          <div class="content">
            <p class="subtitle">Holdings</p>
          </div>
          <add-holding-modal-form
            :isActive="isAddHoldingModalFormActive"
            @close="isAddHoldingModalFormActive = false"
            @submit="addHolding"
          >
          </add-holding-modal-form>
          <div class="buttons">
            <button
              class="button is-light"
              @click="isAddHoldingModalFormActive = true"
            >
              <span>Add new holding</span>
              <span class="icon is-small">
                <i class="fas fa-plus"></i>
              </span>
            </button>
          </div>
          <progress
            v-if="!isDataReady"
            class="progress is-small"
            max="100"
          ></progress>
          <div v-else class="table-container">
            <table class="table is-hoverable is-fullwidth">
              <thead>
                <tr>
                  <th>Name</th>
                  <th>Quantity</th>
                  <th>Market Price</th>
                  <th>Unit Cost</th>
                  <th>Action</th>
                </tr>
              </thead>
              <tbody>
                <tr v-for="holding in filteredHoldings" :key="holding.id">
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
                  <td>{{ holding.marketPrice }}</td>
                  <td>{{ holding.unitCost }}</td>
                  <td>
                    <button
                      class="button is-danger is-light is-small"
                      @click="deleteHolding(holding)"
                    >
                      Delete
                    </button>
                  </td>
                </tr>
              </tbody>
            </table>
          </div>
          <pagination
            :currentPageNumber="currentPageNumber"
            :totalPageCount="Math.ceil(holdings.length / pageSize)"
            @goToPage="goToPage"
          ></pagination>
        </div>
      </div>
    </div>
    <div class="box">
      <div class="columns">
        <div class="column">
          <canvas id="positionByHolding"></canvas>
        </div>
        <div class="column">
          <canvas id="positionByType"></canvas>
        </div>
        <div class="column">
          <canvas id="positionBySector"></canvas>
        </div>
      </div>
    </div>
  </page>
</template>

<script>
import Page from "../components/Page.vue";
import EditPortfolioForm from "../normalForms/EditPortfolio.vue";
import AddHoldingModalForm from "../modalForms/AddHolding.vue";
import Pagination from "../components/Pagination.vue";
import pageMixin from "../mixins/page";
import portfolioService from "../services/portfolioService";
import holdingService from "../services/holdingService";
import tradeService from "../services/tradeService";
import charting from "../utils/charting";

export default {
  components: {
    Page,
    EditPortfolioForm,
    AddHoldingModalForm,
    Pagination
  },
  data() {
    return {
      portfolioId: this.$route.params.id,
      portfolioName: "",
      portfolioMarketValue: 0,
      portfolioCost: 0,
      currentPageNumber: 1,
      pageSize: 2,
      holdings: [],
      isAddHoldingModalFormActive: false
    };
  },
  mixins: [pageMixin],
  computed: {
    portfolioReturn() {
      return this.portfolioMarketValue - this.portfolioCost;
    },
    filteredHoldings() {
      return this.holdings.slice(
        (this.currentPageNumber - 1) * this.pageSize,
        this.currentPageNumber * this.pageSize
      );
    }
  },
  async created() {
    const { data } = await portfolioService.getPortfolio(
      this.portfolioId,
      this.accessToken
    );

    this.portfolioName = data.name;
    this.holdings = data.holdings;
    this.portfolioMarketValue = data.marketValue;
    this.portfolioCost = data.cost;

    const holdingsBySector = this.groupHoldingsBy("sector");
    const holdingsByType = this.groupHoldingsBy("type");

    charting.plotPositionPie(
      "positionByHolding",
      this.holdings.map(h => {
        return {
          name: h.ticker,
          value: h.quantity * h.marketPrice
        };
      }),
      "Holdings"
    );
    charting.plotPositionPie("positionByType", holdingsByType, "Types");
    charting.plotPositionPie("positionBySector", holdingsBySector, "Sectors");

    this.isDataReady = true;
  },
  methods: {
    async updatePortfolio(portfolio) {
      const userId = this.$store.state.auth.user.userId;

      try {
        await portfolioService.updatePortfolio(
          { ...portfolio, ownerId: userId },
          this.accessToken
        );

        this.notifySuccess(
          "Portfolio updated",
          `Portfolio "${portfolio.name}" has been updated successfully.`
        );

        this.$router.go();
      } catch (error) {
        this.notifyError("Unable to update portfolio", error);
      }
    },
    goToPage(pageNumber) {
      this.currentPageNumber = pageNumber;
    },
    addHolding(holding, trade) {
      holdingService
        .createHolding(
          {
            ticker: holding.ticker,
            portfolioId: this.portfolioId
          },
          this.accessToken
        )
        .then(resp =>
          tradeService.createTrade(
            {
              tradeType: "Buy",
              quantity: trade.quantity,
              unitPrice: trade.unitPrice,
              fees: trade.fees,
              remarks: trade.remarks,
              date: trade.date,
              holdingId: resp.data.id
            },
            this.accessToken
          )
        )
        .then(resp => {
          this.notifySuccess("Holding added", resp.data);
        })
        .catch(err => {
          this.notifyError("Unable to add holding", err);
        })
        .finally(() => {
          this.isAddHoldingModalFormActive = false;
          this.$router.go();
        });
    },
    deleteHolding(holding) {
      holdingService
        .deleteHolding(holding.id, this.accessToken)
        .then(() => {
          this.notifySuccess(
            "Holding deleted",
            `Holding ${holding.ticker} has been deleted.`
          ).then(() => {
            this.$router.go();
          });
        })
        .catch(err => {
          this.notifyError("Unable to delete holding", err);
        });
    },
    groupHoldingsBy(property) {
      const group = this.holdings.reduce((pv, cv) => {
        (pv[cv[property]] = pv[cv[property]] || []).push(cv);
        return pv;
      }, {});

      const groupedData = [];
      const keys = Object.keys(group);
      for (const key of keys) {
        const holdings = group[key];
        const totalHoldingValue = holdings.reduce(
          (pv, cv) => pv + cv.quantity * cv.marketPrice,
          0
        );
        groupedData.push({
          name: key,
          value: totalHoldingValue
        });
      }

      return groupedData;
    }
  }
};
</script>
