import jwt_decode from "jwt-decode";

export default {
  decode(token) {
    const data = jwt_decode(token);
    console.log("data", data);
    return data;
  }
};
