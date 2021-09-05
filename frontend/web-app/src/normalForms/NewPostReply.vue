<template>
  <article class="media">
    <figure class="media-left">
      <p class="image is-48x48">
        <img
          :src="
            user.profilePicture
              ? 'data:image/png;base64,' + user.profilePicture
              : 'https://bulma.io/images/placeholders/96x96.png'
          "
        />
      </p>
    </figure>
    <div class="media-content">
      <div class="field">
        <p class="control">
          <textarea
            class="textarea"
            placeholder="Add a post..."
            v-model="content"
          ></textarea>
        </p>
        <p class="help is-danger" v-if="validationErrors.content">
          {{ validationErrors.content }}
        </p>
      </div>
      <div class="field">
        <p class="control">
          <button class="button is-light" @click="post">Post</button>
        </p>
      </div>
    </div>
  </article>
</template>

<script>
import formMixin from "../mixins/form";
import validationSchemas from "../utils/validationSchemas";
import Joi from "joi";

const schema = Joi.object({
  content: validationSchemas.postContent
});

export default {
  props: ["user"],
  mixins: [formMixin],
  data() {
    return {
      content: ""
    };
  },
  methods: {
    post() {
      if (
        !this.validate(schema, {
          content: this.content
        })
      )
        return;

      this.$emit("addNewPostReply", this.content);
    }
  }
};
</script>
