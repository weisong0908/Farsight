import axios from "axios";

export default {
  async getPortfolios(accessToken) {
    return await axios.get(`${process.env.VUE_APP_BACKEND}/portfolios`, {
      headers: {
        Authorization: `Bearer ${accessToken}`
      }
    });
  },
  async createPortfolio(payload, accessToken) {
    return await axios.post(
      `${process.env.VUE_APP_BACKEND}/portfolios`,
      payload,
      {
        headers: {
          Authorization: `Bearer ${accessToken}`
        }
      }
    );
  }
};
