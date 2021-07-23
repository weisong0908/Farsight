import jwt from "../utils/jwt";
import moment from "moment";

export default {
  state: {
    isAuth: false,
    user: {
      userId: "",
      username: "",
      profilePicture: ""
    },
    accessToken: "",
    refreshToken: "",
    expiresAt: ""
  },
  mutations: {
    setStatus(state, status) {
      state.isAuth = status;
    },
    setUser(state, user) {
      state.user = user;
    },
    setAccessToken(state, accessToken) {
      state.accessToken = accessToken;
    },
    setRefreshToken(state, refreshToken) {
      state.refreshToken = refreshToken;
    },
    setExpiresAt(state, expiresAt) {
      state.expiresAt = expiresAt;
    }
  },
  actions: {
    login({ commit }, payload) {
      console.log("payload", payload);
      const { access_token, refresh_token, expires_in } = payload.data;
      const { sub, profilePicture } = jwt.decode(access_token);

      commit("setStatus", true);
      commit("setUser", {
        userId: sub,
        username: payload.username,
        profilePicture: profilePicture
      });
      commit("setAccessToken", access_token);
      commit("setRefreshToken", refresh_token);
      const expiresAt = moment(new Date())
        .add(expires_in, "s")
        .toDate()
        .toString();
      commit("setExpiresAt", expiresAt);
    },
    logout({ commit }) {
      commit("setStatus", false);
      commit("setUser", null);
      commit("setAccessToken", "");
      commit("setRefreshToken", "");
      commit("setExpiresAt", null);
    }
  }
};
