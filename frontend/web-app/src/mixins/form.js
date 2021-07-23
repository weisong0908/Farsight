export default {
  data() {
    return {
      validationErrors: {}
    };
  },
  methods: {
    validate(schema, payload) {
      const validationResults = schema.validate(payload);
      this.validationErrors = {};
      if (validationResults.error) {
        for (let item of validationResults.error.details) {
          const validationError = {};
          validationError[item.path[0]] = item.message;

          this.validationErrors = {
            ...this.validationErrors,
            ...validationError
          };
        }
        return false;
      } else return true;
    }
  }
};
