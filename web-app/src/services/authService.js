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
  }
};
