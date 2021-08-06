export default {
  state: {
    isAppReady: false
  },
  mutations: {
    setAppReadiness(state, status) {
      state.isAppReady = status;
    }
  },
  actions: {
    appIsReady({ commit }) {
      commit("setAppReadiness", true);
    },
    appIsNotReady({ commit }) {
      commit("setAppReadiness", false);
    }
  }
};
