<template>
  <page>
    <p>User Information</p>
    <div class="columns">
      <div class="column">
        <figure class="image is-128x128">
          <img
            :src="
              profilePicture == undefined
                ? defaultProfilePicture
                : profilePicturePreview
            "
          />
        </figure>
        <div class="file has-name">
          <label class="file-label">
            <input
              class="file-input"
              type="file"
              name="resume"
              @change="uploadProfilePicture"
            />
            <span class="file-cta">
              <span class="file-icon">
                <i class="fas fa-upload"></i>
              </span>
              <span class="file-label">
                Choose a file…
              </span>
            </span>
            <span class="file-name">
              {{ profilePictureName }}
            </span>
          </label>
        </div>
        <p>Username: {{ username }}</p>
        <p>Email: {{ email }}</p>
        <p>Is email verified: {{ email_verified }}</p>
        <p>Name: {{ name }}</p>
        <p>Name: {{ name }}</p>
      </div>
      <div class="column"></div>
    </div>
  </page>
</template>

<script>
import Page from "../components/Page.vue";
import imageConverter from "../utils/imageConverter";
import authService from "../services/authService";

export default {
  components: { Page },
  data() {
    return {
      userId: this.$store.state.auth.user.userId,
      username: this.$store.state.auth.user.username,
      email: "",
      email_verified: false,
      name: "",
      profilePicture: "",
      profilePictureName: "",
      profilePicturePreview: "",
      defaultProfilePicture: "https://bulma.io/images/placeholders/128x128.png"
    };
  },
  created() {
    const accessToken = localStorage.getItem("accessToken");

    authService.getUserInfo(accessToken).then(resp => {
      console.log("user info", resp.data);
      this.email = resp.data.email;
      this.email_verified = resp.data.email_verified;
      this.name = resp.data.name;
      this.profilePicture = resp.data.profilePicture;
    });
  },
  methods: {
    uploadProfilePicture(e) {
      const file = e.currentTarget.files[0];
      this.profilePictureName = file.name;
      this.profilePicturePreview = window.URL.createObjectURL(file);

      imageConverter.imageToBase64(file).then(result => {
        this.profilePicture = result.split(",")[1];
      });
    }
  }
};
</script>
