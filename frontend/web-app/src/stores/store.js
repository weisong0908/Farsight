import Vue from "vue";
import Vuex from "vuex";
import createPersistedState from "vuex-persistedstate";
import holding from "./holding.js";
import auth from "./auth.js";
import alert from "./alert";
import stock from "./stock";

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
    },
    stock: {
      namespaced: true,
      ...stock
    }
  },
  plugins: [createPersistedState()]
});
