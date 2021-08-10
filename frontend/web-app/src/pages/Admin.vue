<template>
  <page>
    <div class="tabs is-centered">
      <ul>
        <li
          :class="selectedTab == 'users' ? 'is-active' : ''"
          @click="selectedTab = 'users'"
        >
          <a>Users</a>
        </li>
        <li
          :class="selectedTab == 'portfolios' ? 'is-active' : ''"
          @click="selectedTab = 'portfolios'"
        >
          <a>Portfolios</a>
        </li>
        <li
          :class="selectedTab == 'holdings' ? 'is-active' : ''"
          @click="selectedTab = 'holdings'"
        >
          <a>Holdings</a>
        </li>
        <li
          :class="selectedTab == 'trades' ? 'is-active' : ''"
          @click="selectedTab = 'trades'"
        >
          <a>Trades</a>
        </li>
      </ul>
    </div>
    <div v-if="selectedTab === 'users'">
      <div class="table-container">
        <table class="table is-hoverable">
          <thead>
            <tr>
              <th>User ID</th>
              <th>Username</th>
              <th>Email</th>
              <th>Is Email Verified</th>
              <th>Display Name</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="user in users" :key="user.id">
              <td>
                {{ user.id }}
              </td>
              <td>{{ user.email }}</td>
              <td>{{ user.username }}</td>
              <td>
                {{ user.isEmailVerified ? "Yes" : "No" }}
              </td>
              <td>{{ user.displayName }}</td>
            </tr>
          </tbody>
        </table>
      </div>
    </div>
  </page>
</template>

<script>
import Page from "../components/Page.vue";
import pageMixin from "../mixins/page";
import adminService from "../services/adminService";

export default {
  components: { Page },
  data() {
    return {
      selectedTab: "users",
      users: []
    };
  },
  mixins: [pageMixin],
  async created() {
    const allUsersResp = await adminService.getAllUsers(this.accessToken);
    this.users = allUsersResp.data;
  }
};
</script>
