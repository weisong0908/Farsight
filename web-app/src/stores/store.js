import Vue from "vue";
import Vuex from "vuex";
import holding from "./holding.js";
import auth from "./auth.js";
import alert from "./alert";

Vue.use(Vuex);

export default new Vuex.Store({
  modules: {
    holding: {
      namespaced: true,
      ...holding
    },
    auth: {
      namespaced: true,
      ...auth
    },
    alert: {
      namespaced: true,
      ...alert
    }
  }
});
