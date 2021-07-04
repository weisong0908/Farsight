import VueRouter from "vue-router";
import Dashboard from "./pages/Dashboard";
import Portfolios from "./pages/Portfolios";
import Portfolio from "./pages/Portfolio";
import Holdings from "./pages/Holdings";
import Holding from "./pages/Holding";
import AddHoldingForm from "./pages/AddHoldingForm";
import Login from "./pages/Login";
import Signup from "./pages/Signup";
import ConfirmEmail from "./pages/ConfirmEmail";
import UserInfo from "./pages/UserInfo";

const routerConfig = new VueRouter({
  mode: "history",
  routes: [
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
    },
    {
      name: "login",
      path: "/login",
      component: Login,
      title: "Log In"
    },
    {
      name: "signup",
      path: "/signup",
      component: Signup,
      title: "Sign Up"
    },
    {
      name: "confirmEmail",
      path: "/confirmEmail",
      component: ConfirmEmail,
      title: "Confirm Email"
    },
    {
      name: "userInfo",
      path: "/userinfo",
      component: UserInfo,
      title: "User Information"
    },
    {
      name: "dashboard",
      path: "/",
      component: Dashboard,
      title: "Dashboard"
    }
  ]
});

export default routerConfig;
