<template>
  <page>
    <search-field
      :hasButton="true"
      v-model="usernameSearchText"
      @search="searchUsers"
    ></search-field>
    <div class="columns is-multiline">
      <div class="column is-one-quarter" v-for="user in users" :key="user.id">
        <div class="card">
          <div class="card-content">
            <div class="media">
              <div class="media-left">
                <figure class="image is-48x48">
                  <img
                    :src="
                      user.profilePicture
                        ? 'data:image/png;base64,' + user.profilePicture
                        : 'https://bulma.io/images/placeholders/96x96.png'
                    "
                    alt="Profile picture"
                  />
                </figure>
              </div>
              <div class="media-content">
                <p class="title is-5">{{ user.displayName }}</p>
                <p class="subtitle is-6 has-text-grey has-text-weight-light">
                  @{{ user.username }}
                </p>
              </div>
            </div>

            <div class="content">
              {{ user.statusMessage }}
              <br />
            </div>
          </div>
          <footer class="card-footer">
            <a @click="toggleFollow(user)" class="card-footer-item">{{
              user.isFollow ? "Unfollow" : "Follow"
            }}</a>
            <router-link
              class="card-footer-item"
              :to="{
                name: 'user',
                params: { id: user.id }
              }"
            >
              Posts
            </router-link>
          </footer>
        </div>
      </div>
    </div>
    <pagination
      :currentPageNumber="currentPageNumber"
      :totalPageCount="Math.ceil(usersCount / pageSize)"
      @goToPage="goToPage"
    ></pagination>
  </page>
</template>

<script>
import Page from "../components/Page.vue";
import SearchField from "../components/SearchField.vue";
import Pagination from "../components/Pagination.vue";
import pageMixin from "../mixins/page";
import userService from "../services/userService";

export default {
  components: { Page, SearchField, Pagination },
  data() {
    return {
      usernameSearchText: "",
      users: [],
      usersCount: 0,
      currentPageNumber: 1,
      pageSize: 10
    };
  },
  mixins: [pageMixin],
  async created() {
    await this.searchUsers();
  },
  methods: {
    async searchUsers() {
      const { users, count } = (
        await userService.searchUsers(
          this.accessToken,
          this.usernameSearchText,
          this.currentPageNumber,
          this.pageSize
        )
      ).data;

      this.users = users;
      this.usersCount = count;
    },
    toggleFollow(user) {
      alert(user.isFollow ? "Unfollow" : "Follow");
    },
    async goToPage(pageNumber) {
      this.currentPageNumber = pageNumber;
      await this.searchUsers();
    }
  }
};
</script>
