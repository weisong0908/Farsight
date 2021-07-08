import jwt from "../utils/jwt";

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
    expiresIn: ""
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
    setExpiresIn(state, expiresIn) {
      state.expiresIn = expiresIn;
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
      commit("setExpiresIn", expires_in);
    },
    logout({ commit }) {
      commit("setStatus", false);
      commit("setUser", null);
      commit("setAccessToken", "");
      commit("setRefreshToken", "");
      commit("setExpiresIn", "");
    }
  }
};
