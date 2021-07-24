import axios from "axios";

export default {
  async getHoldings(portfolioId, accessToken) {
    return await axios.get(
      `${process.env.VUE_APP_BACKEND}/holdings?portfolioId=${portfolioId}`,
      {
        headers: {
          Authorization: `Bearer ${accessToken}`
        }
      }
    );
  },
  async getHolding(holdingId, accessToken) {
    return await axios.get(
      `${process.env.VUE_APP_BACKEND}/holdings/${holdingId}`,
      {
        headers: {
          Authorization: `Bearer ${accessToken}`
        }
      }
    );
  },
  async createHolding(payload, accessToken) {
    return await axios.post(
      `${process.env.VUE_APP_BACKEND}/holdings`,
      payload,
      {
        headers: {
          Authorization: `Bearer ${accessToken}`
        }
      }
    );
  },
  async deleteHolding(holdingId, accessToken) {
    return await axios.delete(
      `${process.env.VUE_APP_BACKEND}/holdings/${holdingId}`,
      {
        headers: {
          Authorization: `Bearer ${accessToken}`
        }
      }
    );
  }
};
