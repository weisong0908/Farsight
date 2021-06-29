export default {
  state: {
    isAuth: false,
    accessToken: localStorage.getItem("accessToken") || "",
    idToken: localStorage.getItem("idToken") || "",
    user: null
  },
  mutations: {
    setStatus(state, status) {
      state.isAuth = status;
    },
    setUser(state, user) {
      state.user = user;
    }
  },
  actions: {
    login({ commit }, user) {
      commit("setStatus", true);
      commit("setUser", user);
    },
    logout({ commit }) {
      commit("setStatus", false);
      commit("setUser", null);
    }
  }
};
