export default {
  state: {
    isAppReady: false,
    isBurgerMenuActive: false,
    announcement: {
      title: "",
      url: ""
    }
  },
  mutations: {
    setAppStatus(state, status) {
      state.isAppReady = status;
    },
    setNavbarBurgerMenuStatus(state, status) {
      state.isBurgerMenuActive = status;
    },
    setAnnouncement(state, announcement) {
      state.announcement = announcement;
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
