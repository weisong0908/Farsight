export default {
  state: {
    isAppReady: false,
    isBurgerMenuActive: false
  },
  mutations: {
    setAppStatus(state, status) {
      state.isAppReady = status;
    },
    setNavbarBurgerMenuStatus(state, status) {
      state.isBurgerMenuActive = status;
    }
  },
  actions: {
    appIsReady({ commit }) {
      commit("setAppStatus", true);
    },
    appIsNotReady({ commit }) {
      commit("setAppStatus", false);
    },
    openNavbarBurgerMenu({ commit }) {
      commit("setNavbarBurgerMenuStatus", true);
    },
    closeNavbarBurgerMenu({ commit }) {
      commit("setNavbarBurgerMenuStatus", false);
    }
  }
};
