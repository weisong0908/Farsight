<template>
  <div :class="isActive ? 'modal is-active' : 'modal'">
    <div class="modal-background"></div>
    <div class="modal-card">
      <header class="modal-card-head">
        <p class="modal-card-title">Add New Holding</p>
        <button class="delete" aria-label="close" @click="close"></button>
      </header>
      <section class="modal-card-body">
        <stock-search-bar
          v-if="suggestions.length > 0"
          title="Ticker"
          :suggestions="suggestions"
          :errorMessage="validationErrors.ticker"
          v-model="ticker"
        ></stock-search-bar>
        <form-field
          name="date"
          title="Date"
          type="date"
          icon="fa-calendar"
          v-model="date"
        ></form-field>
        <form-field
          name="quantity"
          title="Quantity"
          type="number"
          icon="fa-calculator"
          min="1"
          v-model="quantity"
          :errorMessage="validationErrors.quantity"
        ></form-field>
        <form-field
          name="unitPrice"
          title="Unit Price"
          type="number"
          min="0"
          step="0.01"
          icon="fa-money-bill"
          v-model="unitPrice"
          :errorMessage="validationErrors.unitPrice"
        ></form-field>
        <form-field
          name="fees"
          title="Fees"
          type="number"
          min="0"
          step="0.01"
          icon="fa-calculator"
          v-model="fees"
          :errorMessage="validationErrors.fees"
        ></form-field>
        <div class="field">
          <label for="remarks" class="label">Remarks</label>
          <div class="control">
            <textarea
              rows="3"
              id="remarks"
              class="textarea"
              v-model="remarks"
            ></textarea>
          </div>
        </div>
      </section>
      <footer class="modal-card-foot">
        <button class="button is-success" @click="submit">Save changes</button>
        <button class="button" @click="close">Cancel</button>
      </footer>
    </div>
  </div>
</template>

<script>
import FormField from "../components/FormField.vue";
import StockSearchBar from "../components/StockSearchBar.vue";
import stockService from "../services/stockService";
import dateConverter from "../utils/dateConverter";
import formMixin from "../mixins/form";
import Joi from "joi";
import validationSchemas from "../utils/validationSchemas";

const schema = Joi.object({
  ticker: validationSchemas.ticker,
  quantity: validationSchemas.quantity,
  fees: validationSchemas.fees,
  unitPrice: validationSchemas.unitPrice
});

export default {
  props: ["isActive"],
  data() {
    return {
      ticker: "",
      date: dateConverter.toString(new Date()),
      quantity: 1,
      unitPrice: 0,
      fees: 0,
      remarks: "",
      suggestions: []
    };
  },
  mixins: [formMixin],
  components: { FormField, StockSearchBar },
  async created() {
    await this.$store.dispatch(
      "stock/setStocks",
      (await stockService.getStocks()).data
    );

    this.suggestions = this.$store.getters["stock/filterStocks"]("");
  },
  methods: {
    submit() {
      const formData = {
        ticker: this.ticker,
        quantity: this.quantity,
        unitPrice: this.unitPrice,
        fees: this.fees
      };

      if (!this.validate(schema, formData)) return;

      this.$emit(
        "submit",
        {
          ticker: this.ticker
        },
        {
          quantity: this.quantity,
          unitPrice: this.unitPrice,
          fees: this.fees,
          remarks: this.remarks,
          date: this.date
        }
      );
    },
    close() {
      this.ticker = "";
      this.date = dateConverter.toString(new Date());
      this.quantity = 1;
      this.unitPrice = 0;
      this.fees = 0;
      this.remarks = "";
      this.$emit("close");
    }
  }
};
</script>
