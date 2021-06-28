import Vue from "vue";
import Vuex from "vuex";
import holding from "./holding.js";

Vue.use(Vuex);

export default new Vuex.Store({
  modules: {
    holding: {
      namespaced: true,
      ...holding
    }
  }
});
