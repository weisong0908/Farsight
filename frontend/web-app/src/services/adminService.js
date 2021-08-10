import axios from "axios";

export default {
  async getAllUsers(accessToken) {
    return await axios.get(
      `${process.env.VUE_APP_IDENTITY_SERVICE}/admin/allUsers`,
      {
        headers: {
          Authorization: `Bearer ${accessToken}`
        }
      }
    );
  }
};
