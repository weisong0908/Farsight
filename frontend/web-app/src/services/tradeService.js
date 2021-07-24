import axios from "axios";

export default {
  async createTrade(payload, accessToken) {
    return await axios.post(`${process.env.VUE_APP_BACKEND}/trades`, payload, {
      headers: {
        Authorization: `Bearer ${accessToken}`
      }
    });
  }
};
