export default {
  state: {
    isActive: false,
    type: "",
    heading: "",
    message: ""
  },
  mutations: {
    update(state, payload) {
      state.isActive = payload.isActive;
      state.type = payload.type;
      state.heading = payload.heading;
      state.message = payload.message;
    }
  },
  actions: {
    danger({ commit }, payload) {
      commit("update", { isActive: true, type: "danger", ...payload });
    },
    warning({ commit }, payload) {
      commit("update", { isActive: true, type: "warning", ...payload });
    },
    success({ commit }, payload) {
      commit("update", { isActive: true, type: "success", ...payload });
    },
    info({ commit }, payload) {
      commit("update", { isActive: true, type: "info", ...payload });
    },
    hideAlert({ commit }) {
      commit("update", {
        isActive: false,
        type: "",
        heading: "",
        message: ""
      });
    }
  }
};
