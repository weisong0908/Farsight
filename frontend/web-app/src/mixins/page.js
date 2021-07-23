import authService from "../services/authService";

export default {
  data() {
    return {
      isPageReady: false,
      isDataReady: false
    };
  },
  methods: {
    /**
     * Create a success notification
     * @param {String} heading
     * @param {String} message
     * @returns {Promise}
     */
    notifySuccess(heading, message) {
      return this.$store.dispatch("alert/success", {
        heading,
        message
      });
    },
    /**
     * Create an error notification
     * @param {String} heading
     * @param {Error} err
     * @returns {Promise}
     */
    notifyError(heading, err) {
      const message = err.response
        ? err.response.data.error_description ||
          err.response.data.title ||
          err.response.statusText
        : "No connection";
      return this.$store.dispatch("alert/danger", {
        heading,
        message
      });
    },
    getAccessToken() {
      const expiresAt = new Date(this.$store.state.auth.expiresAt);
      const now = new Date();
      if (expiresAt && expiresAt > now)
        return this.$store.state.auth.accessToken;

      authService
        .refreshAuth(this.$store.state.auth.refreshToken)
        .then(resp => {
          this.$store.dispatch("auth/login", {
            username: this.username,
            data: resp.data
          });
        });

      return this.$store.state.auth.accessToken;
    }
  }
};
