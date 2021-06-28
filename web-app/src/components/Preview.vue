<template>
  <div>
    <h1 class="title">Preview</h1>
    <p>Ticker: {{ ticker }}</p>
    <p>Type: {{ type }}</p>
    <p>Portfolio: {{ portfolio }}</p>
    <p>Date: {{ date }}</p>
    <p>Quantity: {{ quantity }}</p>
    <p>Unit Price: $ {{ unitPrice }}</p>
    <p>Fees: $ {{ fees }}</p>
    <p>Total: $ {{ total }}</p>
    <p>Remarks: {{ remarks }}</p>
  </div>
</template>

<script>
export default {
  props: [
    "ticker",
    "type",
    "portfolio",
    "date",
    "quantity",
    "unitPrice",
    "fees",
    "remarks"
  ],
  computed: {
    total() {
      const quantity = parseInt(this.quantity);
      const unitPrice = parseFloat(this.unitPrice);
      const fees = parseFloat(this.fees);
      return (quantity * unitPrice + fees).toFixed(2);
    }
  }
};
</script>
