import VueRouter from "vue-router";
import store from "./stores/store";

import Dashboard from "./pages/Dashboard";
import Portfolios from "./pages/Portfolios";
import Portfolio from "./pages/Portfolio";
import AddPortfolioForm from "./pages/AddPortfolioForm";
import Holdings from "./pages/Holdings";
import Holding from "./pages/Holding";
import AddHoldingForm from "./pages/AddHoldingForm";
import Login from "./pages/Login";
import Signup from "./pages/Signup";
import ConfirmEmail from "./pages/ConfirmEmail";
import UserInfo from "./pages/UserInfo";
import ChangePassword from "./pages/ChangePassword";
import ResetPassword from "./pages/ResetPassword";
import ConfirmResetPassword from "./pages/ConfirmResetPassword";

const routerConfig = new VueRouter({
  mode: "history",
  routes: [
    {
      name: "portfolios",
      path: "/portfolios",
      component: Portfolios,
      title: "Portfolios",
      parent: "dashboard"
    },
    {
      name: "portfolio",
      path: "/portfolios/:id",
      component: Portfolio,
      title: "Portfolio",
      parent: "portfolios"
    },
    {
      name: "addPortfolioForm",
      path: "/portfolios/new",
      component: AddPortfolioForm,
      title: "Create New Portfolio"
    },
    {
      name: "holdings",
      path: "/holdings",
      component: Holdings,
      title: "Holdings",
      parent: "dashboard"
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
      name: "changePassword",
      path: "/changePassword",
      component: ChangePassword,
      title: "Change Password"
    },
    {
      name: "resetPassword",
      path: "/resetPassword",
      component: ResetPassword,
      title: "Reset Password"
    },
    {
      name: "confirmResetPassword",
      path: "/confirmResetPassword",
      component: ConfirmResetPassword,
      title: "Confirm Reset Password"
    },
    {
      name: "dashboard",
      path: "/",
      component: Dashboard,
      title: "Dashboard"
    }
  ]
});

routerConfig.beforeEach((to, from, next) => {
  const publicPages = [
    "dashboard",
    "login",
    "signup",
    "confirmEmail",
    "resetPassword",
    "confirmResetPassword"
  ];

  if (
    publicPages.find(p => p == to.name) == undefined &&
    store.state.auth.isAuth == false
  )
    next({ name: "login" });
  else next();
});

export default routerConfig;
