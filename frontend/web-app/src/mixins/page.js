export default {
  data() {
    return {
      isDataReady: false
    };
  },
  computed: {
    isAppReady() {
      return this.$store.state.common.isAppReady;
    },
    accessToken() {
      return this.$store.state.auth.accessToken;
    }
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
    }
  }
};
