<template>
  <div class="box">
    <p class="subtitle">Edit User Information</p>
    <div class="field">
      <figure class="image is-128x128">
        <img
          :src="
            userInfo.profilePicture == ''
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
    <form-field
      name="username"
      title="Username"
      v-model="username"
      type="text"
      icon="fa-user"
      :readonly="true"
    ></form-field>
    <form-field
      name="email"
      title="Email"
      v-model="email"
      type="email"
      icon="fa-envelope"
      :readonly="true"
      :errorMessage="email_verified ? '' : 'Email has not been verified yet'"
    >
    </form-field>
    <br />
    <div class="field is-grouped">
      <div class="control">
        <button class="button is-primary" @click="updateUserInfo">
          Update
        </button>
      </div>
    </div>
  </div>
</template>

<script>
import FormField from "../components/FormField.vue";
import formMixin from "../mixins/form";
import imageConverter from "../utils/imageConverter";

export default {
  props: ["username", "email", "email_verified", "profilePicture"],
  data() {
    return {
      userInfo: {
        profilePicture: this.profilePicture
      },
      profilePictureName: "",
      profilePicturePreview: "data:image/png;base64," + this.profilePicture,
      defaultProfilePicture: "https://bulma.io/images/placeholders/128x128.png"
    };
  },
  components: { FormField },
  mixins: [formMixin],
  methods: {
    uploadProfilePicture(e) {
      const file = e.currentTarget.files[0];
      this.profilePictureName = file.name;
      this.profilePicturePreview = window.URL.createObjectURL(file);

      imageConverter.imageToBase64(file).then(result => {
        this.userInfo.profilePicture = result.split(",")[1];
      });
    },
    updateUserInfo() {
      this.$emit("submit", this.userInfo);
    }
  }
};
</script>
