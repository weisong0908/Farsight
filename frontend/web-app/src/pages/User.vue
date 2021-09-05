<template>
  <page>
    <div class="columns">
      <div class="column is-one-quarter">
        <div class="card" v-if="isDataReady">
          <div class="card-image">
            <figure class="image is-square">
              <img
                :src="
                  user.profilePicture
                    ? 'data:image/png;base64,' + user.profilePicture
                    : 'https://bulma.io/images/placeholders/256x256.png'
                "
                alt="Profile picture"
              />
            </figure>
          </div>
          <div class="card-content">
            <div class="media">
              <div class="media-content">
                <p class="title is-5">{{ user.displayName }}</p>
                <p class="subtitle is-6 has-text-grey has-text-weight-light">
                  @{{ user.username }}
                </p>
                <div class="tags">
                  <span class="tag">Diamond hands</span>
                  <span class="tag">Multibaggers</span>
                  <span class="tag">Dividend machine</span>
                  <span class="tag">On the moon</span>
                  <span class="tag">Back to earth</span>
                </div>
              </div>
            </div>

            <div class="content">
              <p>
                {{ user.statusMessage }}
              </p>
              <p>
                <small
                  >{{ user.followerCount }} followers &middot;
                  {{ user.followCount }} following</small
                >
              </p>
            </div>
          </div>
          <footer class="card-footer">
            <a @click="toggleFollow" class="card-footer-item">{{
              user.isFollow ? "Unfollow" : "Follow"
            }}</a>
          </footer>
        </div>
      </div>
      <div class="column" v-if="isDataReady">
        <post
          v-for="post in posts"
          :key="post.id"
          :post="post"
          :user="user"
          @addNewPostReply="addNewPostReply"
          @deletePost="deletePost"
          @deletePostReply="deletePostReply"
        ></post>
        <new-post-form
          v-if="user.id == $store.state.auth.user.userId"
          :user="user"
          @addNewPost="addNewPost"
        ></new-post-form>
      </div>
    </div>
  </page>
</template>

<script>
import Page from "../components/Page.vue";
import Post from "../components/Post.vue";
import NewPostForm from "../normalForms/NewPost.vue";
import pageMixin from "../mixins/page";
import userService from "../services/userService";
import postService from "../services/postService";

export default {
  components: { Page, Post, NewPostForm },
  mixins: [pageMixin],
  data() {
    return {
      user: "",
      posts: []
    };
  },
  async beforeRouteUpdate(to, from, next) {
    this.isDataReady = false;
    await this.loadUser(to.params.id);
    await this.loadPosts(to.params.id);
    this.isDataReady = true;
    next();
  },
  async created() {
    await this.loadUser(this.$route.params.id);
    await this.loadPosts(this.$route.params.id);
    this.isDataReady = true;
  },
  methods: {
    async loadUser(userId) {
      this.user = (await userService.getUser(userId, this.accessToken)).data;
    },
    async loadPosts(userId) {
      this.posts = (await postService.getPosts(userId, this.accessToken)).data;

      const users = [this.user];
      for (const post of this.posts) {
        for (const reply of post.replies) {
          const user = users.find(u => u.id == reply.authorId);
          if (user) {
            reply.user = user;
          } else {
            reply.user = (
              await userService.getUser(reply.authorId, this.accessToken)
            ).data;
            users.push(reply.user);
          }
        }
      }
    },
    toggleFollow() {
      alert("");
    },
    async addNewPost(content) {
      try {
        await postService.createPost({ content: content }, this.accessToken);
        this.isDataReady = false;
        await this.loadPosts(this.$route.params.id);
        this.isDataReady = true;
      } catch (error) {
        this.notifyError("Unable to post", error);
      }
    },
    async addNewPostReply(postId, content) {
      try {
        await postService.createPostReply(
          postId,
          { content },
          this.accessToken
        );
        this.isDataReady = false;
        await this.loadPosts(this.$route.params.id);
        this.isDataReady = true;
      } catch (error) {
        this.notifyError("Unable to post reply", error);
      }
    },
    async deletePost(postId) {
      try {
        await postService.deletePost(postId, this.accessToken);
        this.isDataReady = false;
        await this.loadPosts(this.$route.params.id);
        this.isDataReady = true;
      } catch (error) {
        this.notifyError("Unable to delete post", error);
      }
    },
    async deletePostReply(replyId) {
      try {
        await postService.deletePostReply(replyId, this.accessToken);
        this.isDataReady = false;
        await this.loadPosts(this.$route.params.id);
        this.isDataReady = true;
      } catch (error) {
        this.notifyError("Unable to delete post reply", error);
      }
    }
  }
};
</script>
