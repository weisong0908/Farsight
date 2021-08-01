import Vue from "vue";
import App from "./App.vue";
import VueRouter from "vue-router";
import routerConfig from "./routerConfig.js";
import Vuex from "vuex";
import store from "./stores/store.js";
// import "../node_modules/bulma/css/bulma.css";
import "./assets/main.scss";

Vue.config.productionTip = false;

Vue.use(VueRouter);
Vue.use(Vuex);

new Vue({
  render: h => h(App),
  router: routerConfig,
  store: store
}).$mount("#app");
