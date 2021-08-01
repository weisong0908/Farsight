<template>
  <page>
    <div class="columns">
      <div class="column">
        <ticker-field v-model="ticker"></ticker-field>
        <dropdown-field
          :options="portfolios"
          name="portfolio"
          title="Portfolio"
          v-model="selectedPortfolio"
        ></dropdown-field>
        <date-field
          name="date"
          title="Date"
          icon="fa-calendar"
          v-model="date"
        ></date-field>
        <number-field
          name="quantity"
          title="Quantity"
          icon="fa-calculator"
          v-model="quantity"
        ></number-field>
        <money-field
          name="unitPrice"
          title="Unit Price"
          icon="fa-money-bill"
          v-model="unitPrice"
        ></money-field>
        <money-field
          name="fees"
          title="Fees"
          icon="fa-dollar-sign"
          v-model="fees"
        ></money-field>
        <textarea-field
          name="remarks"
          title="Remarks"
          v-model="remarks"
        ></textarea-field>
        <div class="field is-grouped">
          <div class="control">
            <button class="button" @click="save">Save</button>
          </div>
          <div class="control">
            <button class="button is-warning" @click="cancel">Cancel</button>
          </div>
        </div>
      </div>
      <div class="column">
        <preview
          :date="date"
          :ticker="ticker"
          type="Add holding"
          :portfolio="selectedPortfolio"
          :quantity="quantity"
          :unitPrice="unitPrice"
          :fees="fees"
          :remarks="remarks"
        ></preview>
      </div>
    </div>
  </page>
</template>

<script>
import Page from "../components/Page.vue";
import TickerField from "../components/controls/TickerField.vue";
import DropdownField from "../components/controls/DropdownField.vue";
import DateField from "../components/controls/DateField.vue";
import NumberField from "../components/controls/NumberField.vue";
import MoneyField from "../components/controls/MoneyField.vue";
import TextareaField from "../components/controls/TextareaField.vue";
import Preview from "../components/Preview.vue";
import dateConverter from "../utils/dateConverter";

export default {
  components: {
    Page,
    TickerField,
    DropdownField,
    DateField,
    NumberField,
    MoneyField,
    TextareaField,
    Preview
  },
  data() {
    return {
      portfolios: [
        { id: 1, title: "one" },
        { id: 2, title: "two" }
      ],
      ticker: "AAPL",
      selectedPortfolio: this.$route.query.portfolioId,
      date: dateConverter.toString(new Date()),
      quantity: 0,
      unitPrice: 0,
      fees: 0,
      remarks: ""
    };
  },
  methods: {
    save() {
      alert("save");
    },
    cancel() {
      this.$router.push({ name: "holdings" });
    }
  }
};
</script>
