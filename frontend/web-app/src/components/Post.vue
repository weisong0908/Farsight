<template>
  <article class="media">
    <figure class="media-left">
      <p class="image is-64x64">
        <img
          :src="
            user.profilePicture
              ? 'data:image/png;base64,' + user.profilePicture
              : 'https://bulma.io/images/placeholders/128x128.png'
          "
        />
      </p>
    </figure>
    <div class="media-content">
      <div class="content">
        <p>
          <strong>{{ user.displayName }}</strong>
          <br />
          {{ post.content }}
          <br />
          <small
            ><a>Like</a> &middot;
            <a @click="showReplyForm = !showReplyForm">Reply</a> &middot;
            <a
              v-if="post.authorId == $store.state.auth.user.userId"
              @click="deletePost"
              class="has-text-danger"
              >Delete</a
            >
            {{ getElapsed(post.dateTime) }}
          </small>
        </p>
      </div>

      <article class="media" v-for="reply in post.replies" :key="reply.id">
        <figure class="media-left">
          <p class="image is-48x48">
            <img
              :src="
                reply.user.profilePicture && reply.user.profilePicture != ''
                  ? 'data:image/png;base64,' + reply.user.profilePicture
                  : 'https://bulma.io/images/placeholders/96x96.png'
              "
            />
          </p>
        </figure>
        <div class="media-content">
          <div class="content">
            <p>
              <strong>{{ reply.user.displayName }}</strong>
              <br />
              {{ reply.content }}
              <br />
              <small
                ><a>Like</a> &middot;
                <a @click="showReplyForm = !showReplyForm">Reply</a> &middot;
                <a
                  v-if="reply.authorId == $store.state.auth.user.userId"
                  @click="deletePostReply(reply.id)"
                  class="has-text-danger"
                  >Delete</a
                >
                {{ getElapsed(reply.dateTime) }}</small
              >
            </p>
          </div>
        </div>
      </article>
      <new-post-reply-form
        v-if="showReplyForm"
        :user="user"
        @addNewPostReply="addNewPostReply"
      ></new-post-reply-form>
    </div>
  </article>
</template>

<script>
import NewPostReplyForm from "../normalForms/NewPostReply.vue";

export default {
  props: ["post", "user"],
  data() {
    return {
      showReplyForm: false
    };
  },
  components: { NewPostReplyForm },
  methods: {
    getElapsed(dateTimeString) {
      const seconds = parseInt((new Date() - new Date(dateTimeString)) / 1000);
      if (seconds < 60) return `${seconds} sec`;
      const minutes = parseInt(seconds / 60);
      if (minutes < 60) return `${minutes} min`;
      const hours = parseInt(minutes / 60);
      if (hours == 1) return `${hours} hr`;
      if (hours < 24) return `${hours} hrs`;
      const days = parseInt(days / 24);
      if (days == 1) return `${days} day`;
      if (days < 7) return `${days} days`;
      const weeks = parseInt(days / 7);
      if (weeks == 1) return `${weeks} wk`;
      return `${weeks} wks`;
    },
    addNewPostReply(content) {
      this.$emit("addNewPostReply", this.post.id, content);
    },
    deletePost() {
      this.$emit("deletePost", this.post.id);
    },
    deletePostReply(replyId) {
      this.$emit("deletePostReply", replyId);
    }
  }
};
</script>
