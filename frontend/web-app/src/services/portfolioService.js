import axios from "axios";

export default {
  async getPortfolios(accessToken) {
    return await axios.get(`${process.env.VUE_APP_BACKEND}/portfolios`, {
      headers: {
        Authorization: `Bearer ${accessToken}`
      }
    });
  },
  async getPortfolio(portfolioId, accessToken) {
    return await axios.get(
      `${process.env.VUE_APP_BACKEND}/portfolios/${portfolioId}`,
      {
        headers: {
          Authorization: `Bearer ${accessToken}`
        }
      }
    );
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
  },
  async updatePortfolio(payload, accessToken) {
    return await axios.put(
      `${process.env.VUE_APP_BACKEND}/portfolios/${payload.id}`,
      payload,
      {
        headers: {
          Authorization: `Bearer ${accessToken}`
        }
      }
    );
  }
};
