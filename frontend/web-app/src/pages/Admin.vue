<template>
  <page>
    <nav class="level">
      <div class="level-item has-text-centered">
        <div>
          <p class="heading">Users</p>
          <p class="title">{{ users.length }}</p>
        </div>
      </div>
      <div class="level-item has-text-centered">
        <div>
          <p class="heading">Portfolios</p>
          <p class="title">{{ portfolios.length }}</p>
        </div>
      </div>
      <div class="level-item has-text-centered">
        <div>
          <p class="heading">Holdings</p>
          <p class="title">{{ holdings.length }}</p>
        </div>
      </div>
      <div class="level-item has-text-centered">
        <div>
          <p class="heading">Trades</p>
          <p class="title">{{ trades.length }}</p>
        </div>
      </div>
    </nav>
    <br />
    <div class="tabs is-centered">
      <ul>
        <li
          :class="selectedTab == 'users' ? 'is-active' : ''"
          @click="goToTab('users')"
        >
          <a>Users</a>
        </li>
        <li
          :class="selectedTab == 'portfolios' ? 'is-active' : ''"
          @click="goToTab('portfolios')"
        >
          <a>Portfolios</a>
        </li>
        <li
          :class="selectedTab == 'holdings' ? 'is-active' : ''"
          @click="goToTab('holdings')"
        >
          <a>Holdings</a>
        </li>
        <li
          :class="selectedTab == 'trades' ? 'is-active' : ''"
          @click="goToTab('trades')"
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
            <tr v-for="user in filteredUsers" :key="user.id">
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
      <pagination
        :currentPageNumber="currentPageNumber"
        :totalPageCount="Math.ceil(users.length / pageSize)"
        @goToPage="goToPage"
      ></pagination>
    </div>
    <div v-if="selectedTab === 'portfolios'">
      <div class="table-container">
        <table class="table is-hoverable">
          <thead>
            <tr>
              <th>Portfolio ID</th>
              <th>Name</th>
              <th>Owner Id</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="portfolios in filteredPortfolios" :key="portfolios.id">
              <td>
                {{ portfolios.id }}
              </td>
              <td>{{ portfolios.name }}</td>
              <td>{{ portfolios.ownerId }}</td>
            </tr>
          </tbody>
        </table>
      </div>
      <pagination
        :currentPageNumber="currentPageNumber"
        :totalPageCount="Math.ceil(portfolios.length / pageSize)"
        @goToPage="goToPage"
      ></pagination>
    </div>
    <div v-if="selectedTab === 'holdings'">
      <div class="table-container">
        <table class="table is-hoverable">
          <thead>
            <tr>
              <th>Holding ID</th>
              <th>Ticker</th>
              <th>Portfolio ID</th>
              <th>Owner ID</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="holding in filteredHoldings" :key="holding.id">
              <td>
                {{ holding.id }}
              </td>
              <td>{{ holding.ticker }}</td>
              <td>{{ holding.portfolioId }}</td>
              <td>
                {{
                  portfolios.filter(p => p.id == holding.portfolioId)[0].ownerId
                }}
              </td>
            </tr>
          </tbody>
        </table>
      </div>
      <pagination
        :currentPageNumber="currentPageNumber"
        :totalPageCount="Math.ceil(holdings.length / pageSize)"
        @goToPage="goToPage"
      ></pagination>
    </div>
    <div v-if="selectedTab === 'trades'">
      <div class="table-container">
        <table class="table is-hoverable">
          <thead>
            <tr>
              <th>Trade ID</th>
              <th>Trade Type</th>
              <th>Quantity</th>
              <th>Holding ID</th>
              <th>Owner ID</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="trade in filteredTrades" :key="trade.id">
              <td>
                {{ trade.id }}
              </td>
              <td>{{ trade.tradeType }}</td>
              <td>{{ trade.quantity }}</td>
              <td>{{ trade.holdingId }}</td>
              <td>
                {{
                  portfolios.filter(
                    p =>
                      p.id ==
                      holdings.filter(h => h.id == trade.holdingId)[0]
                        .portfolioId
                  )[0].ownerId
                }}
              </td>
            </tr>
          </tbody>
        </table>
      </div>
      <pagination
        :currentPageNumber="currentPageNumber"
        :totalPageCount="Math.ceil(trades.length / pageSize)"
        @goToPage="goToPage"
      ></pagination>
    </div>
  </page>
</template>

<script>
import Page from "../components/Page.vue";
import Pagination from "../components/Pagination.vue";
import pageMixin from "../mixins/page";
import adminService from "../services/adminService";

export default {
  components: { Page, Pagination },
  data() {
    return {
      selectedTab: "users",
      currentPageNumber: 1,
      pageSize: 10,
      users: [],
      portfolios: [],
      holdings: [],
      trades: []
    };
  },
  computed: {
    filteredUsers() {
      return this.users.slice(
        (this.currentPageNumber - 1) * this.pageSize,
        this.currentPageNumber * this.pageSize
      );
    },
    filteredPortfolios() {
      return this.portfolios.slice(
        (this.currentPageNumber - 1) * this.pageSize,
        this.currentPageNumber * this.pageSize
      );
    },
    filteredHoldings() {
      return this.holdings.slice(
        (this.currentPageNumber - 1) * this.pageSize,
        this.currentPageNumber * this.pageSize
      );
    },
    filteredTrades() {
      return this.trades.slice(
        (this.currentPageNumber - 1) * this.pageSize,
        this.currentPageNumber * this.pageSize
      );
    }
  },
  mixins: [pageMixin],
  async created() {
    this.users = (await adminService.getAllUsers(this.accessToken)).data;
    this.portfolios = (
      await adminService.getAllPortfolios(this.accessToken)
    ).data;
    this.holdings = (await adminService.getAllHoldings(this.accessToken)).data;
    this.trades = (await adminService.getAllTrades(this.accessToken)).data;
  },
  methods: {
    goToTab(tabName) {
      this.selectedTab = tabName;
      this.currentPageNumber = 1;
    },
    goToPage(pageNumber) {
      this.currentPageNumber = pageNumber;
    }
  }
};
</script>
