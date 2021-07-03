import axios from "axios";

export default {
  async login(username, password) {
    return new Promise((resolve, reject) =>
      username === "username" && password === "password"
        ? resolve("logged in")
        : reject(new Error("wrong username or password"))
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
  async confirmEmail(userId, code) {
    return await axios.get(
      `${process.env.VUE_APP_IDENTITY_SERVICE}/accounts/confirmEmail?userId=${userId}&code=${code}`
    );
  }
};
