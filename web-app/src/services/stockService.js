import axios from "axios";

export default {
  async getDetails(ticker) {
    const resp = await axios.get(
      `https://api.polygon.io/v1/meta/symbols/${ticker}/company?apiKey=${process.env.VUE_APP_POLYGON_APIKEY}`
    );

    return resp.data;
  },
  async getPreviousPrice(ticker) {
    const resp = await axios.get(
      `https://api.polygon.io/v2/aggs/ticker/${ticker}/prev?adjusted=true&apiKey=${process.env.VUE_APP_POLYGON_APIKEY}`
    );

    return resp.data;
  },
  async getTrend(ticker) {
    const from = "2021-01-01";
    const to = "2021-06-27";
    const resp = await axios.get(
      `https://api.polygon.io/v2/aggs/ticker/${ticker}/range/1/day/${from}/${to}?adjusted=true&sort=asc&apiKey=${process.env.VUE_APP_POLYGON_APIKEY}`
    );

    return resp.data;
  }
};
