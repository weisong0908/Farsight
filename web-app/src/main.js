import Vue from "vue";
import App from "./App.vue";
import VueRouter from "vue-router";
import routerConfig from "./routerConfig";

import "../node_modules/bulma/css/bulma.css";

Vue.config.productionTip = false;

Vue.use(VueRouter);

new Vue({
  render: h => h(App),
  router: routerConfig
}).$mount("#app");
