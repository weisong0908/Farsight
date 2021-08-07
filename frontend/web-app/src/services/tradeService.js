import axios from "axios";

export default {
  async getTrades(accessToken) {
    return await axios.get(`${process.env.VUE_APP_BACKEND}/trades`, {
      headers: {
        Authorization: `Bearer ${accessToken}`
      }
    });
  },
  async createTrade(payload, accessToken) {
    return await axios.post(`${process.env.VUE_APP_BACKEND}/trades`, payload, {
      headers: {
        Authorization: `Bearer ${accessToken}`
      }
    });
  },
  async updateTrade(payload, accessToken) {
    return await axios.put(
      `${process.env.VUE_APP_BACKEND}/trades/${payload.id}`,
      payload,
      {
        headers: {
          Authorization: `Bearer ${accessToken}`
        }
      }
    );
  },
  async deleteTrade(tradeId, accessToken) {
    return await axios.delete(
      `${process.env.VUE_APP_BACKEND}/trades/${tradeId}`,
      {
        headers: {
          Authorization: `Bearer ${accessToken}`
        }
      }
    );
  }
};
