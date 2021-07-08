<template>
  <page>
    <div class="columns">
      <div class="column">
        <div class="field">
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
        </div>
        <field
          name="username"
          title="Username"
          :value="username"
          type="text"
          icon="fa-user"
          :readonly="true"
        ></field>
        <field
          name="email"
          title="Email"
          :value="email"
          type="email"
          icon="fa-envelope"
          :readonly="true"
        >
          <template v-slot:errorMessages>
            <p class="help is-success" v-if="email_verified">
              Email is verified
            </p>
            <p class="help is-warning" v-else>
              Email has not been verified yet
            </p>
          </template>
        </field>
        <div class="field is-grouped">
          <div class="control">
            <button class="button is-primary" @click="updateUserInfo">
              Update
            </button>
          </div>
        </div>
      </div>
      <div class="column"></div>
    </div>
  </page>
</template>

<script>
import Page from "../components/Page.vue";
import Field from "../components/Field.vue";
import imageConverter from "../utils/imageConverter";
import authService from "../services/authService";

export default {
  components: { Page, Field },
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
    const accessToken = this.$store.state.auth.accessToken;

    authService.getUserInfo(accessToken).then(resp => {
      this.email = resp.data.email;
      this.email_verified = resp.data.email_verified;
      this.name = resp.data.name;
      this.profilePicture = resp.data.picture;
      this.profilePicturePreview = "data:image/png;base64," + resp.data.picture;
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
    },
    updateUserInfo() {
      const accessToken = this.$store.state.auth.accessToken;

      authService
        .updateUserInfo(
          { userId: this.userId, profilePicture: this.profilePicture },
          accessToken
        )
        .then(resp => {
          this.$store.dispatch("alert/success", {
            heading: "User info updated",
            message: resp.data
          });
        })
        .catch(err => {
          const errorDescriptions = err.response.data.map(d => d.description);
          this.$store.dispatch("alert/danger", {
            heading: "Error updating user info",
            message: errorDescriptions.join(" ")
          });
        });
    }
  }
};
</script>
