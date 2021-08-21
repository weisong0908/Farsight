import axios from "axios";

export default {
  async getHoldingCategories(accessToken, portfolioId) {
    return await axios.get(
      `${process.env.VUE_APP_BACKEND}/holdingCategories?portfolioId=${portfolioId}`,
      {
        headers: {
          Authorization: `Bearer ${accessToken}`
        }
      }
    );
  }
};
