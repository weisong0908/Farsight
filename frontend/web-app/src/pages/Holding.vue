<template>
  <page>
    <router-link
      class="button is-light"
      :to="{ name: 'portfolio', params: { id: holding.portfolioId } }"
    >
      <span class="icon is-small">
        <i class="fas fa-arrow-left"></i>
      </span>
      <span>Back to portfolio</span>
    </router-link>
    <br />
    <br />
    <progress
      v-if="!isDataReady"
      class="progress is-small"
      max="100"
    ></progress>
    <template v-else>
      <div class="card">
        <div class="card-content">
          <div class="media">
            <div class="media-left">
              <figure class="image is-48x48">
                <img
                  :src="
                    stockInfo.logo ||
                      'https://bulma.io/images/placeholders/128x128.png'
                  "
                  alt="logo"
                />
              </figure>
            </div>
            <div class="media-content">
              <p class="title is-4">{{ stockInfo.name }}</p>
              <p class="subtitle is-6">{{ stockInfo.symbol }}</p>
            </div>
          </div>

          <div class="content">
            <p>
              {{ stockInfo.description }}
            </p>
            <p v-if="stockInfo.sector != '-'">
              {{ stockInfo.sector }}&nbsp;&#8208;&nbsp;{{ stockInfo.industry }}
            </p>
            <hr />
            <p>
              <span
                :class="
                  holding.hasPosition
                    ? 'tag is-primary is-light'
                    : 'tag is-danger is-light'
                "
                >{{ holding.hasPosition ? "Open" : "Closed" }} Position</span
              >
            </p>
            <p>
              Invested <strong>{{ holding.quantity }}</strong> share(s) @
              <strong>{{ holding.investedAmount.toFixed(2) }}</strong>
            </p>
            <p class="has-text-grey">Holding ID: {{ holding.id }}</p>
          </div>
        </div>
      </div>
      <br />
      <edit-holding-form
        :holding="holding"
        :categories="holdingCategories"
        @submit="updateHolding"
        @removeCategory="removeHoldingCategory"
      ></edit-holding-form>
      <div class="box">
        <p class="subtitle">Trade History</p>
        <add-trade-modal-form
          :isActive="isAddTradeModalFormActive"
          :maxSellableQuantity="holding.quantity"
          @close="isAddTradeModalFormActive = false"
          @submit="addTrade"
        ></add-trade-modal-form>
        <div class="buttons">
          <button class="button" @click="isAddTradeModalFormActive = true">
            Add New Trade
          </button>
        </div>
        <div v-if="holding.trades" class="table-container">
          <table class="table is-hoverable is-fullwidth">
            <thead>
              <tr>
                <th>Date</th>
                <th>Quantity</th>
                <th>Unit Price</th>
                <th>Fees</th>
                <th>Type</th>
                <th>Action</th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="trade in holding.trades" :key="trade.id">
                <td>{{ trade.date }}</td>
                <td>{{ trade.quantity }}</td>
                <td>{{ trade.unitPrice.toFixed(2) }}</td>
                <td>{{ trade.fees.toFixed(2) }}</td>
                <td>
                  <span class="icon-text">
                    <span v-if="trade.tradeType == 'Buy'" class="icon">
                      <i class="fas fa-caret-left"></i>
                    </span>
                    <span v-else-if="trade.tradeType == 'Sell'" class="icon">
                      <i class="fas fa-caret-right"></i>
                    </span>
                    <span>{{ trade.tradeType }}</span>
                  </span>
                </td>
                <td>
                  <div class="buttons">
                    <button
                      class="button is-small"
                      @click="updateEditTradeModalForm(trade)"
                    >
                      Edit
                    </button>
                    <button
                      class="button is-danger is-light is-small"
                      @click="deleteTrade(trade)"
                    >
                      Delete
                    </button>
                  </div>
                </td>
              </tr>
            </tbody>
          </table>
        </div>
        <article v-else class="message is-warning">
          <div class="message-body">
            There is no trade record found for this holding
          </div>
        </article>
      </div>
      <edit-trade-modal-form
        :selectedTrade="selectedTrade"
        :isActive="isEditTradeModalFormActive"
        :maxSellableQuantity="maxSellableQuantity"
        @close="isEditTradeModalFormActive = false"
        @submit="updateTrade"
      ></edit-trade-modal-form>
    </template>
    <div class="box">
      <canvas id="historicalPrice"></canvas>
    </div>
  </page>
</template>

<script>
import Page from "../components/Page.vue";
import AddTradeModalForm from "../modalForms/AddTrade.vue";
import EditTradeModalForm from "../modalForms/EditTrade.vue";
import EditHoldingForm from "../normalForms/EditHolding.vue";
import pageMixin from "../mixins/page";
import holdingService from "../services/holdingService";
import stockService from "../services/stockService";
import tradeService from "../services/tradeService";
import holdingCategoryService from "../services/holdingCategoryService";
import dateConverter from "../utils/dateConverter";
import charting from "../utils/charting";

export default {
  components: { Page, AddTradeModalForm, EditTradeModalForm, EditHoldingForm },
  data() {
    return {
      holding: {},
      stockInfo: {},
      historicalPrices: [],
      chartItem: {},
      isEditTradeModalFormActive: false,
      selectedTrade: {},
      isAddTradeModalFormActive: false,
      holdingCategories: []
    };
  },
  mixins: [pageMixin],
  computed: {
    totalCost() {
      return this.holding.trades
        .map(t => t.unitPrice * t.quantity + t.fees)
        .reduce((a, b) => a + b, 0);
    },
    maxSellableQuantity() {
      const trades = this.holding.trades.filter(
        t => t.id != this.selectedTrade.id
      );

      return trades.reduce((pv, cv) => {
        return cv.tradeType === "Buy" ? pv + cv.quantity : pv - cv.quantity;
      }, 0);
    }
  },
  async created() {
    this.holding = (
      await holdingService.getHolding(this.$route.params.id, this.accessToken)
    ).data;

    if (this.holding.trades.length > 0) {
      const stockClosePrices = (
        await stockService.getPerformance(
          this.holding.ticker,
          this.holding.trades[0].date,
          dateConverter.toString(new Date()),
          this.accessToken
        )
      ).data;

      let cost = this.holding.costHistory
        .filter(ch => ch.date <= stockClosePrices[0].date)
        .pop().cost;
      for (const stockClosePrice of stockClosePrices) {
        const index = this.holding.costHistory.findIndex(
          ch => ch.date === stockClosePrice.date
        );
        if (index != -1) {
          cost = this.holding.costHistory[index].cost;
        }
        this.historicalPrices.push({
          date: stockClosePrice.date,
          closePrice: stockClosePrice.closePrice,
          cost
        });
      }

      charting.plotPriceTrend(
        "historicalPrice",
        this.historicalPrices,
        this.holding.ticker
      );
    }

    this.stockInfo = (
      await stockService.getInfo(this.holding.ticker, this.accessToken)
    ).data;

    this.holdingCategories = (
      await holdingCategoryService.getHoldingCategories(
        this.accessToken,
        this.holding.portfolioId
      )
    ).data;

    this.isDataReady = true;
  },
  methods: {
    async updateHolding(holding) {
      try {
        await holdingService.updateHolding(
          {
            id: this.holding.id,
            ticker: this.holding.ticker,
            portfolioId: this.holding.portfolioId,
            categoryName: holding.categoryName
          },
          this.accessToken
        );

        this.notifySuccess(
          "Holding updated",
          `Holding "${this.holding.ticker}" has been updated successfully.`
        );
        this.$router.go();
      } catch (error) {
        this.notifyError("Unable to update holding", error);
      }
    },
    async removeHoldingCategory(holdingCategory) {
      try {
        await holdingCategoryService.deleteHoldingCategory(
          holdingCategory.id,
          this.accessToken
        );

        this.notifySuccess(
          "Holding category removed",
          `Holding category "${holdingCategory.name}" has been removed successfully.`
        );
        this.$router.go();
      } catch (error) {
        this.notifyError("Unable to remove holding category", error);
      }
    },
    updateEditTradeModalForm(trade) {
      this.selectedTrade = trade;
      this.isEditTradeModalFormActive = true;
    },
    async addTrade(trade) {
      try {
        await tradeService.createTrade(
          {
            ...trade,
            holdingId: this.holdingId
          },
          this.accessToken
        );
        await this.notifySuccess("Trade added", `Trade has been added.`);
      } catch (error) {
        this.notifyError("Unable to add trade", error);
      }

      this.isAddTradeModalFormActive = false;
      this.$router.go();
    },
    async updateTrade(trade) {
      try {
        await tradeService.updateTrade(
          {
            id: this.selectedTrade.id,
            ...trade,
            holdingId: this.holdingId
          },
          this.accessToken
        );

        await this.notifySuccess(
          "Trade updated",
          `Trade ${trade.id} has been updated.`
        );
      } catch (error) {
        this.notifyError("Unable to update trade", error);
      }

      this.isEditTradeModalFormActive = false;
      this.$router.go();
    },
    async deleteTrade(trade) {
      try {
        await tradeService.deleteTrade(trade.id, this.accessToken);

        await this.notifySuccess(
          "Trade deleted",
          `Trade ${trade.id} has been deleted.`
        );

        this.$router.go();
      } catch (error) {
        this.notifyError("Unable to delete trade", error.resp);
      }
    }
  }
};
</script>
