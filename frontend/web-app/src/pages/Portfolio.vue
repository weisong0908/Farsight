<template>
  <page>
    <div class="columns">
      <div class="column">
        <edit-portfolio-form
          v-if="isDataReady"
          :portfolioId="portfolioId"
          :portfolioName="portfolioName"
          @submit="updatePortfolio"
        ></edit-portfolio-form>
      </div>
      <div class="column">
        <p class="subtitle">Holdings</p>
        <add-holding-modal-form
          :isActive="isAddHoldingModalFormActive"
          @close="isAddHoldingModalFormActive = false"
          @submit="addHolding"
        >
        </add-holding-modal-form>
        <div class="buttons">
          <button
            class="button is-primary"
            @click="isAddHoldingModalFormActive = true"
          >
            Add New Holding
          </button>
        </div>
        <progress
          v-if="!isDataReady"
          class="progress is-small is-primary"
          max="100"
        ></progress>
        <table v-else class="table is-hoverable is-fullwidth">
          <thead>
            <tr>
              <th>Name</th>
              <th>Quantity</th>
              <th>Cost</th>
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
              <td>{{ holding.cost }}</td>
              <td>
                <button
                  class="button is-danger is-small"
                  @click="deleteHolding(holding)"
                >
                  Delete
                </button>
              </td>
            </tr>
          </tbody>
        </table>
        <pagination
          :currentPageNumber="currentPageNumber"
          :totalPageCount="Math.ceil(holdings.length / pageSize)"
          @goToPage="goToPage"
        ></pagination>
      </div>
    </div>
    <div class="columns">
      <div class="column">
        <canvas id="positionByHolding"></canvas>
      </div>
      <div class="column">
        <canvas id="positionByGroup"></canvas>
      </div>
      <div class="column">
        <canvas id="positionBySector"></canvas>
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
      currentPageNumber: 1,
      pageSize: 2,
      holdings: [],
      isAddHoldingModalFormActive: false
    };
  },
  mixins: [pageMixin],
  computed: {
    filteredHoldings() {
      return this.holdings.slice(
        (this.currentPageNumber - 1) * this.pageSize,
        this.currentPageNumber * this.pageSize
      );
    }
  },
  created() {
    const accessToken = this.getAccessToken();

    portfolioService.getPortfolio(this.portfolioId, accessToken).then(resp => {
      this.portfolioName = resp.data.name;
      this.holdings = resp.data.holdings;

      charting.plotPositionPie(
        "positionByHolding",
        this.holdings.map(h => {
          return {
            name: h.ticker,
            value: h.quantity * h.cost
          };
        }),
        "Holdings"
      );
      charting.plotPositionPie(
        "positionByGroup",
        this.holdings.map(h => {
          return {
            name: h.ticker,
            value: h.quantity * h.cost
          };
        }),
        "Groups"
      );
      charting.plotPositionPie(
        "positionBySector",
        this.holdings.map(h => {
          return {
            name: h.ticker,
            value: h.quantity * h.cost
          };
        }),
        "Sectors"
      );

      this.isDataReady = true;
    });
  },
  methods: {
    updatePortfolio(portfolio) {
      const accessToken = this.getAccessToken();
      const userId = this.$store.state.auth.user.userId;
      portfolioService
        .updatePortfolio({ ...portfolio, ownerId: userId }, accessToken)
        .then(() => {
          this.notifySuccess(
            "Portfolio updated",
            `Portfolio "${portfolio.name}" has been updated successfully.`
          );
        })
        .catch(err => {
          this.notifyError("Unable to update portfolio", err);
        });
    },
    goToPage(pageNumber) {
      this.currentPageNumber = pageNumber;
    },
    addHolding(holding, trade) {
      const accessToken = this.getAccessToken();

      holdingService
        .createHolding(
          {
            ticker: holding.ticker,
            portfolioId: this.portfolioId
          },
          accessToken
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
            accessToken
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
        .deleteHolding(holding.id, this.getAccessToken())
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
    }
  }
};
</script>
