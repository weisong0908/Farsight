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
      <div class="level-item has-text-centered">
        <div>
          <p class="heading">Healthy System</p>
          <p class="title">
            {{
              systemHealth.filter(s => s.status == "Healthy").length
            }}&frasl;{{ systemHealth.length }}
          </p>
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
        <li
          :class="selectedTab == 'system' ? 'is-active' : ''"
          @click="goToTab('system')"
        >
          <a>System</a>
        </li>
      </ul>
    </div>
    <div v-if="selectedTab === 'users'">
      <search-field
        title="Search Users"
        description="Search with username"
        v-model="usernameSearchText"
      ></search-field>
      <br />
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
              <td>{{ user.username }}</td>
              <td>{{ user.email }}</td>
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
      <search-field
        title="Search Portfolios"
        description="Search with owner name"
        v-model="portfolioOwnerSearchText"
      ></search-field>
      <div class="table-container">
        <table class="table is-hoverable">
          <thead>
            <tr>
              <th>Portfolio ID</th>
              <th>Name</th>
              <th>Owner</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="portfolios in filteredPortfolios" :key="portfolios.id">
              <td>
                {{ portfolios.id }}
              </td>
              <td>{{ portfolios.name }}</td>
              <td>{{ portfolios.owner }}</td>
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
      <search-field
        title="Search Holdings"
        description="Search with ticker"
        v-model="holdingTickerSearchText"
      ></search-field>
      <div class="table-container">
        <table class="table is-hoverable">
          <thead>
            <tr>
              <th>Holding ID</th>
              <th>Ticker</th>
              <th>Portfolio</th>
              <th>Owner</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="holding in filteredHoldings" :key="holding.id">
              <td>
                {{ holding.id }}
              </td>
              <td>{{ holding.ticker }}</td>
              <td>{{ holding.portfolio }}</td>
              <td>{{ holding.owner }}</td>
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
              <th>Holding</th>
              <th>Portfolio</th>
              <th>Owner</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="trade in filteredTrades" :key="trade.id">
              <td>
                {{ trade.id }}
              </td>
              <td>{{ trade.tradeType }}</td>
              <td>{{ trade.quantity }}</td>
              <td>{{ trade.holding }}</td>
              <td>{{ trade.portfolio }}</td>
              <td>{{ trade.owner }}</td>
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
    <div v-if="selectedTab === 'system'">
      <div class="table-container">
        <table class="table is-hoverable">
          <thead>
            <tr>
              <th>Service</th>
              <th>Url</th>
              <th>Health Status</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="service in systemHealth" :key="service.name">
              <td>{{ service.name }}</td>
              <td>{{ service.url }}</td>
              <td>
                <span class="icon-text">
                  <span
                    :class="
                      service.status == 'Healthy'
                        ? 'icon has-text-success'
                        : 'icon has-text-danger'
                    "
                  >
                    <i class="fas fa-circle"></i>
                  </span>
                  <span>{{ service.status }}</span>
                </span>
              </td>
            </tr>
          </tbody>
        </table>
      </div>
    </div>
  </page>
</template>

<script>
import Page from "../components/Page.vue";
import Pagination from "../components/Pagination.vue";
import SearchField from "../components/SearchField.vue";
import pageMixin from "../mixins/page";
import adminService from "../services/adminService";
import healthCheckService from "../services/healthCheckService";

export default {
  components: { Page, Pagination, SearchField },
  data() {
    return {
      selectedTab: "users",
      currentPageNumber: 1,
      pageSize: 10,
      users: [],
      portfolios: [],
      holdings: [],
      trades: [],
      systemHealth: [
        {
          name: "Backend",
          url: process.env.VUE_APP_BACKEND,
          status: ""
        },
        {
          name: "Identity Service",
          url: process.env.VUE_APP_IDENTITY_SERVICE,
          status: ""
        },
        {
          name: "Common Service",
          url: process.env.VUE_APP_COMMON_SERVICE,
          status: ""
        }
      ],
      usernameSearchText: "",
      portfolioOwnerSearchText: "",
      holdingTickerSearchText: ""
    };
  },
  computed: {
    filteredUsers() {
      return this.users
        .filter(u =>
          u.username
            .toLowerCase()
            .includes(this.usernameSearchText.toLowerCase())
        )
        .slice(
          (this.currentPageNumber - 1) * this.pageSize,
          this.currentPageNumber * this.pageSize
        );
    },
    filteredPortfolios() {
      return this.portfolios
        .filter(p =>
          p.owner
            .toLowerCase()
            .includes(this.portfolioOwnerSearchText.toLowerCase())
        )
        .slice(
          (this.currentPageNumber - 1) * this.pageSize,
          this.currentPageNumber * this.pageSize
        );
    },
    filteredHoldings() {
      return this.holdings
        .filter(h =>
          h.ticker
            .toLowerCase()
            .includes(this.holdingTickerSearchText.toLowerCase())
        )
        .slice(
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

    const portfolios = (await adminService.getAllPortfolios(this.accessToken))
      .data;
    this.portfolios = portfolios.map(p => {
      return {
        ...p,
        owner: this.users.filter(u => u.id === p.ownerId)[0].username
      };
    });

    const holdings = (await adminService.getAllHoldings(this.accessToken)).data;
    this.holdings = holdings.map(h => {
      return {
        ...h,
        portfolio: this.portfolios.filter(p => p.id === h.portfolioId)[0].name,
        owner: this.portfolios.filter(p => p.id === h.portfolioId)[0].owner
      };
    });

    const trades = (await adminService.getAllTrades(this.accessToken)).data;
    this.trades = trades.map(t => {
      return {
        ...t,
        holding: this.holdings.filter(h => h.id === t.holdingId)[0].ticker,
        portfolio: this.holdings.filter(h => h.id === t.holdingId)[0].portfolio,
        owner: this.holdings.filter(h => h.id === t.holdingId)[0].owner
      };
    });

    for (const system of this.systemHealth) {
      system.status = await healthCheckService.getSystemHealth(system.url);
    }
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
