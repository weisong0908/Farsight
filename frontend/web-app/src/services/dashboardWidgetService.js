import axios from "axios";

export default {
  async getPortfoliosWidgetData(accessToken) {
    return await axios.get(
      `${process.env.VUE_APP_BACKEND}/dashboardwidgets/portfolios`,
      {
        headers: {
          Authorization: `Bearer ${accessToken}`
        }
      }
    );
  },
  async getTopHoldingsWidgetData(accessToken) {
    return await axios.get(
      `${process.env.VUE_APP_BACKEND}/dashboardwidgets/topholdings`,
      {
        headers: {
          Authorization: `Bearer ${accessToken}`
        }
      }
    );
  },
  async getRecentTradesWidgetData(accessToken) {
    return await axios.get(
      `${process.env.VUE_APP_BACKEND}/dashboardwidgets/recenttrades`,
      {
        headers: {
          Authorization: `Bearer ${accessToken}`
        }
      }
    );
  },
  async getSummaryWidgetData(accessToken) {
    return await axios.get(
      `${process.env.VUE_APP_BACKEND}/dashboardwidgets/summary`,
      {
        headers: {
          Authorization: `Bearer ${accessToken}`
        }
      }
    );
  }
};
