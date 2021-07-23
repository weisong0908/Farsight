import jwt_decode from "jwt-decode";

export default {
  decode(token) {
    const data = jwt_decode(token);
    return data;
  }
};
