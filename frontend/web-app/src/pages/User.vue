<template>
  <page>
    <div class="columns">
      <div class="column is-one-quarter">
        <div class="card">
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
                Lorem ipsum dolor sit amet, consectetur adipiscing elit.
                Phasellus nec iaculis mauris. <a>@bulmaio</a>.
                <a href="#">#css</a>
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
        ></post>
        <!-- <article class="media">
          <figure class="media-left">
            <p class="image is-64x64">
              <img src="https://bulma.io/images/placeholders/128x128.png" />
            </p>
          </figure>
          <div class="media-content">
            <div class="content">
              <p>
                <strong>Barbara Middleton</strong>
                <br />
                Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis
                porta eros lacus, nec ultricies elit blandit non. Suspendisse
                pellentesque mauris sit amet dolor blandit rutrum. Nunc in
                tempus turpis.
                <br />
                <small><a>Like</a> · <a>Reply</a> · 3 hrs</small>
              </p>
            </div>

            <article class="media">
              <figure class="media-left">
                <p class="image is-48x48">
                  <img src="https://bulma.io/images/placeholders/96x96.png" />
                </p>
              </figure>
              <div class="media-content">
                <div class="content">
                  <p>
                    <strong>Sean Brown</strong>
                    <br />
                    Donec sollicitudin urna eget eros malesuada sagittis.
                    Pellentesque habitant morbi tristique senectus et netus et
                    malesuada fames ac turpis egestas. Aliquam blandit nisl a
                    nulla sagittis, a lobortis leo feugiat.
                    <br />
                    <small><a>Like</a> · <a>Reply</a> · 2 hrs</small>
                  </p>
                </div>
              </div>
            </article>

            <article class="media">
              <figure class="media-left">
                <p class="image is-48x48">
                  <img src="https://bulma.io/images/placeholders/96x96.png" />
                </p>
              </figure>
              <div class="media-content">
                <div class="content">
                  <p>
                    <strong>Kayli Eunice </strong>
                    <br />
                    Sed convallis scelerisque mauris, non pulvinar nunc mattis
                    vel. Maecenas varius felis sit amet magna vestibulum euismod
                    malesuada cursus libero. Vestibulum ante ipsum primis in
                    faucibus orci luctus et ultrices posuere cubilia Curae;
                    Phasellus lacinia non nisl id feugiat.
                    <br />
                    <small><a>Like</a> · <a>Reply</a> · 2 hrs</small>
                  </p>
                </div>
              </div>
            </article>
          </div>
        </article>

        <article class="media">
          <figure class="media-left">
            <p class="image is-64x64">
              <img src="https://bulma.io/images/placeholders/128x128.png" />
            </p>
          </figure>
          <div class="media-content">
            <div class="field">
              <p class="control">
                <textarea
                  class="textarea"
                  placeholder="Add a comment..."
                ></textarea>
              </p>
            </div>
            <div class="field">
              <p class="control">
                <button class="button">Post comment</button>
              </p>
            </div>
          </div>
        </article> -->
      </div>
    </div>
  </page>
</template>

<script>
import Page from "../components/Page.vue";
import Post from "../components/Post.vue";
import pageMixin from "../mixins/page";
import userService from "../services/userService";
import postService from "../services/postService";

export default {
  components: { Page, Post },
  mixins: [pageMixin],
  data() {
    return {
      user: {},
      posts: []
    };
  },
  async created() {
    this.user = (
      await userService.getUser(this.$route.params.id, this.accessToken)
    ).data;

    this.posts = (
      await postService.getPosts(this.$route.params.id, this.accessToken)
    ).data;

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

    this.isDataReady = true;
  },
  methods: {
    toggleFollow() {
      alert("");
    }
  }
};
</script>
