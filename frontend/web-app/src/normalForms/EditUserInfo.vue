<template>
  <div class="box">
    <p class="subtitle">Edit User Information</p>
    <p class="has-text-grey has-text-weight-light">@{{ username }}</p>
    <br />
    <div class="field">
      <label :for="profilePicture" class="label">Profile Picture</label>
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
            id="profilePicture"
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
      name="displayName"
      title="Display Name"
      v-model="userInfo.displayName"
      type="text"
      icon="fa-user"
      :errorMessage="validationErrors.displayName"
    ></form-field>
    <form-field
      name="statusMessage"
      title="Status Message"
      v-model="userInfo.statusMessage"
      type="text"
      icon="fa-user"
      :errorMessage="validationErrors.statusMessage"
      :maxLength="255"
    ></form-field>
    <form-field
      name="email"
      title="Email"
      v-model="userInfo.email"
      type="email"
      icon="fa-envelope"
      :errorMessage="validationErrors.email"
    >
    </form-field>
    <article v-if="!isEmailVerified" class="message is-warning">
      <div class="message-body">
        <p>
          This email address has not been verified. Please check your
          <strong>inbox/junk mails</strong> for an email titled "Confirm Email"
          and follow the instructions. Alternatively, you can also generate the
          email confirmation again by clicking the button below.
        </p>
        <br />
        <button class="button is-outlined" @click="resendEmailConfirmation">
          Resend Email Confirmation
        </button>
      </div>
    </article>
    <br />
    <div class="field is-grouped">
      <div class="control">
        <button class="button is-light" @click="updateUserInfo">
          <span>Update user information</span>
          <span class="icon is-small">
            <i class="fas fa-edit"></i>
          </span>
        </button>
      </div>
    </div>
  </div>
</template>

<script>
import FormField from "../components/FormField.vue";
import formMixin from "../mixins/form";
import imageConverter from "../utils/imageConverter";
import validationSchemas from "../utils/validationSchemas";
import Joi from "joi";

const schema = Joi.object({
  email: validationSchemas.email,
  displayName: validationSchemas.displayName,
  statusMessage: validationSchemas.statusMessage
});

export default {
  props: [
    "username",
    "email",
    "isEmailVerified",
    "displayName",
    "statusMessage",
    "profilePicture"
  ],
  data() {
    return {
      userInfo: {
        profilePicture: this.profilePicture,
        displayName: this.displayName,
        statusMessage: this.statusMessage,
        email: this.email
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
    resendEmailConfirmation() {
      this.$emit("resendEmailConfirmation", this.email);
    },
    updateUserInfo() {
      if (
        !this.validate(schema, {
          email: this.userInfo.email,
          displayName: this.userInfo.displayName,
          statusMessage: this.userInfo.statusMessage
        })
      )
        return;

      this.$emit("submit", this.userInfo);
    }
  }
};
</script>
