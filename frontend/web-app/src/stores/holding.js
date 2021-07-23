export default {
  state: {
    holding: {}
  },
  mutations: {
    setTicker(state, ticker) {
      state.holding.ticker = ticker;
    },
    setDate(state, date) {
      state.holding.date = date;
    },
    setQuantity(state, quantity) {
      state.holding.quantity = quantity;
    },
    setUnitPrice(state, unitPrice) {
      state.holding.unitPrice = unitPrice;
    },
    setFees(state, fees) {
      state.holding.fees = fees;
    },
    setRemarks(state, remarks) {
      state.holding.remarks = remarks;
    }
  }
};
