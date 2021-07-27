export default {
  state: {
    stocks: []
  },
  getters: {
    filterStocks(state) {
      return searchText => {
        const filteredStocks = state.stocks
          .filter(s =>
            s.ticker.toLowerCase().includes(searchText.toLowerCase())
          )
          .slice(0, 8);

        return filteredStocks;
      };
    }
  },
  mutations: {
    setStocks(state, stocks) {
      state.stocks = stocks;
    }
  },
  actions: {
    setStocks({ commit }, payload) {
      commit("setStocks", payload);
    }
  }
};
