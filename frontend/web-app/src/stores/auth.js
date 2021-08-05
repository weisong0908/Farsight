import jwt from "../utils/jwt";
import moment from "moment";
import authService from "../services/authService";

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
    expiresAt: "",
    silentRefresh: null
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
    },
    setSilentRefresh(state, silentRefresh) {
      state.silentRefresh = silentRefresh;
    }
  },
  actions: {
    login({ commit }, payload) {
      const { access_token, refresh_token, expires_in } = payload;
      const { sub, profilePicture, username } = jwt.decode(access_token);

      commit("setStatus", true);
      commit("setUser", {
        userId: sub,
        username: username,
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
    },
    setSilentRefresh(context, interval) {
      const silentRefresh = setInterval(async () => {
        console.log("silent refresh");
        try {
          const { data } = await authService.refreshAuth(
            context.state.refreshToken
          );

          context.commit("setAccessToken", data.access_token);
          context.commit("setRefreshToken", data.refresh_token);
          const expiresAt = moment(new Date())
            .add(data.expires_in, "s")
            .toDate()
            .toString();
          context.commit("setExpiresAt", expiresAt);
        } catch (error) {
          console.log("error refresh login silently", error);
        }
      }, interval);

      context.commit("setSilentRefresh", silentRefresh);
    },
    clearSilentRefresh(context) {
      clearInterval(context.state.silentRefresh);
      context.commit("setSilentRefresh", null);
    }
  }
};
