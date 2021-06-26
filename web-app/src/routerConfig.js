import VueRouter from "vue-router";
import Dashboard from "./pages/Dashboard";
import Portfolios from "./pages/Portfolios";
import Portfolio from "./pages/Portfolio";
import Holdings from "./pages/Holdings";
import Holding from "./pages/Holding";

const routerConfig = new VueRouter({
  mode: "history",
  routes: [
    {
      name: "dashboard",
      path: "/",
      component: Dashboard,
      title: "Dashboard"
    },
    {
      name: "portfolios",
      path: "/portfolios",
      component: Portfolios,
      title: "Portfolios"
    },
    {
      name: "portfolio",
      path: "/portfolios/:id",
      component: Portfolio,
      title: "Portfolio"
    },
    {
      name: "holding",
      path: "/holdings",
      component: Holdings,
      title: "Holdings"
    },
    {
      name: "holding",
      path: "/holdings/:id",
      component: Holding,
      title: "Holding"
    }
  ]
});

export default routerConfig;
