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
            ><a>Like</a> &middot; <a>Reply</a> &middot;
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
                ><a>Like</a> &middot; <a>Reply</a> &middot;
                {{ getElapsed(reply.dateTime) }}</small
              >
            </p>
          </div>
        </div>
      </article>
    </div>
  </article>
</template>

<script>
export default {
  props: ["post", "user"],
  methods: {
    getElapsed(dateTimeString) {
      const seconds = parseInt((new Date() - new Date(dateTimeString)) / 1000);
      if (seconds < 60) return `${seconds} sec`;
      const minutes = parseInt(seconds / 60);
      if (minutes < 60) return `${minutes} min`;
      const hours = parseInt(minutes / 60);
      if (hours == 1) return `${hours} hr`;
      return `${hours} hrs`;
    }
  }
};
</script>
