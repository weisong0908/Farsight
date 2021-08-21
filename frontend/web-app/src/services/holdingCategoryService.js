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
  },
  async deleteHoldingCategory(holdingCategoryId, accessToken) {
    return await axios.delete(
      `${process.env.VUE_APP_BACKEND}/holdingCategories/${holdingCategoryId}`,
      {
        headers: {
          Authorization: `Bearer ${accessToken}`
        }
      }
    );
  }
};
