import axios from "axios";

export default {
  async login(username, password) {
    const params = new URLSearchParams();
    params.append("grant_type", "password");
    params.append(
      "scope",
      "read write openid profile email role offline_access"
    );
    params.append("client_id", "webapp");
    params.append("client_secret", process.env.VUE_APP_CLIENT_SECRET);
    params.append("username", username);
    params.append("password", password);
    return await axios.post(
      `${process.env.VUE_APP_IDENTITY_SERVICE}/connect/token`,
      params,
      {
        headers: {
          "Content-Type": "application/x-www-form-urlencoded"
        }
      }
    );
  },
  isAuth() {
    return true;
  },
  async signup(username, password, email) {
    return await axios.post(
      `${process.env.VUE_APP_IDENTITY_SERVICE}/accounts/signup`,
      {
        username: username,
        email: email,
        password: password
      }
    );
  },
  async confirmEmail(userId, token) {
    return await axios.get(
      `${process.env.VUE_APP_IDENTITY_SERVICE}/accounts/confirmEmail?userId=${userId}&token=${token}`
    );
  },
  async getUserInfo(accessToken) {
    return await axios.get(
      `${process.env.VUE_APP_IDENTITY_SERVICE}/connect/userinfo`,
      {
        headers: {
          Authorization: `Bearer ${accessToken}`
        }
      }
    );
  },
  async updateUserInfo(userInfo, accessToken) {
    return await axios.put(
      `${process.env.VUE_APP_IDENTITY_SERVICE}/accounts/update`,
      userInfo,
      {
        headers: {
          Authorization: `Bearer ${accessToken}`
        }
      }
    );
  }
};
