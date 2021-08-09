import healthCheckService from "../services/healthCheckService";

export default {
  state: {
    isAppReady: false,
    isBurgerMenuActive: false,
    announcement: {
      title: "",
      url: ""
    },
    silentHealthCheck: null,
    isSystemHealthy: false
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
    },
    setSilentHealthCheck(state, silentHealthCheck) {
      state.silentHealthCheck = silentHealthCheck;
    },
    setIsSystemHealthy(state, isSystemHealthy) {
      state.isSystemHealthy = isSystemHealthy;
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
    },
    async setSilentHealthCheck({ commit }) {
      console.log("silent health check enabled");
      const result = await healthCheckService.isSystemHealthy();
      commit("setIsSystemHealthy", result);

      const silentHealthCheck = setInterval(async () => {
        console.log("silent health check");
        const result = await healthCheckService.isSystemHealthy();
        commit("setIsSystemHealthy", result);
      }, 600000);

      commit("setSilentHealthCheck", silentHealthCheck);
    },
    clearSilentHealthCheck(context) {
      clearInterval(context.state.silentHealthCheck);
      context.commit("setSilentHealthCheck", null);
    }
  }
};
