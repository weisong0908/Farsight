import axios from "axios";

export default {
  async isSystemHealthy() {
    try {
      const isBackendHealthy =
        (await axios.get(`${process.env.VUE_APP_BACKEND}/health`)).data ==
        "Healthy";

      const isIdentityServiceHealthy =
        (await axios.get(`${process.env.VUE_APP_IDENTITY_SERVICE}/health`))
          .data == "Healthy";

      const isCommonServiceHealthy =
        (await axios.get(`${process.env.VUE_APP_COMMON_SERVICE}/health`))
          .data == "Healthy";

      return (
        isBackendHealthy && isIdentityServiceHealthy && isCommonServiceHealthy
      );
    } catch (error) {
      console.error(error);
      return false;
    }
  }
};
