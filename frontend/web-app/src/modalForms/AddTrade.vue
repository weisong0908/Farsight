<template>
  <div :class="isActive ? 'modal is-active' : 'modal'">
    <div class="modal-background"></div>
    <div class="modal-card">
      <header class="modal-card-head">
        <p class="modal-card-title">Add Trade</p>
        <button class="delete" aria-label="close" @click="close"></button>
      </header>
      <section class="modal-card-body">
        <div class="field">
          <div class="label">Trade Type</div>
          <div class="control">
            <div class="select">
              <select v-model="tradeType">
                <option value="Buy">Buy</option>
                <option value="Sell">Sell</option>
              </select>
            </div>
          </div>
        </div>
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
import dateConverter from "../utils/dateConverter";
import formMixin from "../mixins/form";
import Joi from "joi";
import validationSchemas from "../utils/validationSchemas";

const schema = Joi.object({
  quantity: validationSchemas.quantity,
  fees: validationSchemas.fees,
  unitPrice: validationSchemas.unitPrice
});

export default {
  props: ["isActive"],
  data() {
    return {
      tradeType: "Buy",
      date: dateConverter.toString(new Date()),
      quantity: 1,
      unitPrice: 0,
      fees: 0,
      remarks: ""
    };
  },
  mixins: [formMixin],
  components: { FormField },
  methods: {
    submit() {
      const formData = {
        quantity: this.quantity,
        unitPrice: this.unitPrice,
        fees: this.fees
      };

      if (!this.validate(schema, formData)) return;

      this.$emit("submit", {
        quantity: this.quantity,
        unitPrice: this.unitPrice,
        fees: this.fees,
        remarks: this.remarks,
        date: this.date,
        tradeType: this.tradeType
      });
    },
    close() {
      this.tradeType = "Buy";
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
