import jwt from "../utils/jwt";

export default {
  state: {
    isAuth: false,
    user: {
      username: "",
      userId: "",
      profilePicture: ""
    }
  },
  mutations: {
    setStatus(state, status) {
      state.isAuth = status;
    },
    setUser(state, user) {
      state.user = user;
    }
  },
  actions: {
    initialise({ commit }) {
      const isAuth = localStorage.getItem("isAuth");

      if (isAuth) commit("setStatus", true);
    },
    login({ commit }, payload) {
      localStorage.setItem("isAuth", true);
      localStorage.setItem("accessToken", payload.data.access_token);
      localStorage.setItem(
        "expiresAt",
        Date.now() + payload.data.expires_in * 1000
      );
      localStorage.setItem("refreshToken", payload.data.refresh_token);

      const payloadData = jwt.decode(payload.data.access_token);

      commit("setStatus", true);
      commit("setUser", {
        username: payload.username,
        userId: payloadData.sub,
        profilePicture: payloadData.profilePicture
      });
    },
    logout({ commit }) {
      localStorage.removeItem("isAuth");
      localStorage.removeItem("accessToken");
      localStorage.removeItem("expiresAt");
      localStorage.removeItem("refreshToken");

      commit("setStatus", false);
      commit("setUser", null);
    }
  }
};
