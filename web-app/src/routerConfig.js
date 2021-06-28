import VueRouter from "vue-router";
import Dashboard from "./pages/Dashboard";
import Portfolios from "./pages/Portfolios";
import Portfolio from "./pages/Portfolio";
import Holdings from "./pages/Holdings";
import Holding from "./pages/Holding";
import AddHoldingForm from "./pages/AddHoldingForm";

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
      name: "holdings",
      path: "/holdings",
      component: Holdings,
      title: "Holdings"
    },
    {
      name: "addNewHolding",
      path: "/holdings/new",
      component: AddHoldingForm,
      title: "Add New Holding"
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
