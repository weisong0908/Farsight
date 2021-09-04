import axios from "axios";

export default {
  async getPosts(userId, accessToken) {
    const params = new URLSearchParams();
    params.append("userId", userId);

    return await axios.get(
      `${process.env.VUE_APP_BACKEND}/posts?${params.toString()}`,
      {
        headers: {
          Authorization: `Bearer ${accessToken}`
        }
      }
    );
  },
  async createPost(payload, accessToken) {
    return await axios.post(`${process.env.VUE_APP_BACKEND}/posts`, payload, {
      headers: {
        Authorization: `Bearer ${accessToken}`
      }
    });
  },
  async createPostReply(postId, payload, accessToken) {
    return await axios.post(
      `${process.env.VUE_APP_BACKEND}/posts/${postId}`,
      payload,
      {
        headers: {
          Authorization: `Bearer ${accessToken}`
        }
      }
    );
  }
};
