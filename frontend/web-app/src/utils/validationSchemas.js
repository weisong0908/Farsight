import Joi from "joi";

export default {
  username: Joi.string()
    .required()
    .label("Username"),
  email: Joi.string()
    .email({ tlds: { allow: false } })
    .required()
    .label("Email address"),
  password: Joi.string()
    .required()
    .label("Password"),
  newPassword: Joi.string()
    .min(6)
    .required()
    .pattern(/[A-Z]/, "containsUppercase")
    .pattern(/[a-z]/, "containsLowercase")
    .pattern(/[0-9]/, "containsNumber")
    .pattern(/[#?!@$%^&*-]/, "containsSpecial")
    .messages({
      "string.pattern.name":
        '"New Password" must contains at least 1 upper case, at least 1 lower case, at least 1 digit, and at least 1 non-alphanumeric character e.g. special characters like "#" or "$"'
    })
    .label("New Password"),
  repeatedPassword: Joi.equal(Joi.ref("newPassword"))
    .messages({
      "any.only": '"Repeated Password" must be same as "Password"'
    })
    .label("Repeated Password"),
  portfolioName: Joi.string()
    .required()
    .label("Portfolio name")
};
