import VueRouter from "vue-router";
import store from "./stores/store";

import Home from "./pages/Home";
import Dashboard from "./pages/Dashboard";
import Portfolios from "./pages/Portfolios";
import Portfolio from "./pages/Portfolio";
import Holdings from "./pages/Holdings";
import Holding from "./pages/Holding";
import Trades from "./pages/Trades";
import Login from "./pages/Login";
import SignUp from "./pages/SignUp";
import ConfirmEmail from "./pages/ConfirmEmail";
import ConfirmEmailChange from "./pages/ConfirmEmailChange";
import MyAccount from "./pages/MyAccount";
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
      name: "holdings",
      path: "/holdings",
      component: Holdings,
      title: "Holdings",
      parent: "dashboard"
    },
    {
      name: "holding",
      path: "/holdings/:id",
      component: Holding,
      title: "Holding",
      parent: "holdings"
    },
    {
      name: "trades",
      path: "/trades",
      component: Trades,
      title: "Trades",
      parent: "dashboard"
    },
    {
      name: "login",
      path: "/login",
      component: Login,
      title: "Login"
    },
    {
      name: "signUp",
      path: "/signup",
      component: SignUp,
      title: "Sign Up"
    },
    {
      name: "confirmEmail",
      path: "/confirmEmail",
      component: ConfirmEmail,
      title: "Confirm Email"
    },
    {
      name: "confirmEmailChange",
      path: "/confirmEmailChange",
      component: ConfirmEmailChange,
      title: "Confirm Email Change"
    },
    {
      name: "myAccount",
      path: "/MyAccount",
      component: MyAccount,
      title: "My Account"
    },
    {
      name: "changePassword",
      path: "/changePassword",
      component: ChangePassword,
      title: "Change Password"
    },
    {
      name: "resetPassword",
      path: "/login/resetPassword",
      component: ResetPassword,
      title: "Reset Password",
      parent: "login"
    },
    {
      name: "confirmResetPassword",
      path: "/confirmResetPassword",
      component: ConfirmResetPassword,
      title: "Confirm Reset Password"
    },
    {
      name: "dashboard",
      path: "/dashboard",
      component: Dashboard,
      title: "Dashboard"
    },
    {
      name: "home",
      path: "/",
      component: Home,
      title: "Farsight"
    }
  ]
});

routerConfig.beforeEach((to, from, next) => {
  const publicPages = [
    "home",
    "login",
    "signUp",
    "confirmEmail",
    "resetPassword",
    "confirmResetPassword"
  ];

  if (
    publicPages.find(p => p == to.name) == undefined &&
    store.state.auth.isAuth == false
  )
    next({ name: "login" });

  if (store.state.auth.isAuth && (to.path == "/" || to.name == "signUp"))
    next({ name: "dashboard" });

  next();
});

export default routerConfig;
