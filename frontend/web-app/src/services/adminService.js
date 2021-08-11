import axios from "axios";

export default {
  async getAllUsers(accessToken) {
    return await axios.get(
      `${process.env.VUE_APP_IDENTITY_SERVICE}/admin/allUsers`,
      {
        headers: {
          Authorization: `Bearer ${accessToken}`
        }
      }
    );
  },
  async getAllPortfolios(accessToken) {
    return await axios.get(
      `${process.env.VUE_APP_BACKEND}/admin/allPortfolios`,
      {
        headers: {
          Authorization: `Bearer ${accessToken}`
        }
      }
    );
  },
  async getAllHoldings(accessToken) {
    return await axios.get(`${process.env.VUE_APP_BACKEND}/admin/allHoldings`, {
      headers: {
        Authorization: `Bearer ${accessToken}`
      }
    });
  },
  async getAllTrades(accessToken) {
    return await axios.get(`${process.env.VUE_APP_BACKEND}/admin/allTrades`, {
      headers: {
        Authorization: `Bearer ${accessToken}`
      }
    });
  }
};
