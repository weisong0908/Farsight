import axios from "axios";

export default {
  async getPortfoliosWidgetData(accessToken) {
    return await axios.get(
      `${process.env.VUE_APP_BACKEND}/dashboardWidgets/portfolios`,
      {
        headers: {
          Authorization: `Bearer ${accessToken}`
        }
      }
    );
  },
  async getTopHoldingsWidgetData(accessToken) {
    return await axios.get(
      `${process.env.VUE_APP_BACKEND}/dashboardWidgets/topHoldings`,
      {
        headers: {
          Authorization: `Bearer ${accessToken}`
        }
      }
    );
  },
  async getRecentTradesWidgetData(accessToken) {
    return await axios.get(
      `${process.env.VUE_APP_BACKEND}/dashboardWidgets/recentTrades`,
      {
        headers: {
          Authorization: `Bearer ${accessToken}`
        }
      }
    );
  },
  async getSummaryWidgetData(accessToken) {
    return await axios.get(
      `${process.env.VUE_APP_BACKEND}/dashboardWidgets/summary`,
      {
        headers: {
          Authorization: `Bearer ${accessToken}`
        }
      }
    );
  }
};
