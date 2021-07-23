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
        ? err.response.data.error_description || err.response.data.title
        : "No connection";
      return this.$store.dispatch("alert/danger", {
        heading,
        message
      });
    }
  }
};
