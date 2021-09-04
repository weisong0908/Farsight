import axios from "axios";

export default {
  async searchUsers(accessToken, searchText, pageNumber, pageSize) {
    const params = new URLSearchParams();
    params.append("searchText", searchText);
    params.append("pageNumber", pageNumber);
    params.append("pageSize", pageSize);
    return await axios.get(
      `${process.env.VUE_APP_IDENTITY_SERVICE}/users?${params.toString()}`,
      {
        headers: {
          Authorization: `Bearer ${accessToken}`
        }
      }
    );
  }
};
