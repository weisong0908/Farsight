<template>
  <nav class="navbar" role="navigation" aria-label="main navigation">
    <div class="navbar-brand">
      <router-link class="navbar-item" to="/">
        <img
          src="https://bulma.io/images/bulma-logo.png"
          width="112"
          height="28"
        />
      </router-link>

      <a
        role="button"
        :class="
          isBurgerMenuActive ? 'navbar-burger is-active' : 'navbar-burger'
        "
        aria-label="menu"
        aria-expanded="false"
        data-target="navbar"
        @click="isBurgerMenuActive = !isBurgerMenuActive"
      >
        <span aria-hidden="true"></span>
        <span aria-hidden="true"></span>
        <span aria-hidden="true"></span>
      </a>
    </div>

    <div
      id="navbar"
      :class="isBurgerMenuActive ? 'navbar-menu is-active' : 'navbar-menu'"
    >
      <div class="navbar-start">
        <router-link
          to="/portfolios"
          class="navbar-item"
          @click.native="isBurgerMenuActive = false"
        >
          Portfolios
        </router-link>

        <router-link
          to="/holdings"
          class="navbar-item"
          @click.native="isBurgerMenuActive = false"
        >
          Holdings
        </router-link>

        <router-link
          to="/userinfo"
          class="navbar-item"
          @click.native="isBurgerMenuActive = false"
        >
          User Info
        </router-link>
        <router-link
          to="/changePassword"
          class="navbar-item"
          @click.native="isBurgerMenuActive = false"
        >
          Change Password
        </router-link>
      </div>

      <div class="navbar-end">
        <div class="navbar-item">
          <div class="buttons">
            <router-link
              class="button is-primary"
              :to="{ name: 'signup', query: { redirectTo: this.$route.path } }"
              @click.native="isBurgerMenuActive = false"
              ><strong>Sign Up</strong></router-link
            >
            <a v-if="isAuth == true" class="button is-warning" @click="logout"
              >Log Out</a
            >
            <router-link
              v-else
              class="button is-light"
              :to="{ name: 'login', query: { redirectTo: this.$route.path } }"
              @click.native="isBurgerMenuActive = false"
              >Log In</router-link
            >
          </div>
        </div>
      </div>
    </div>
  </nav>
</template>

<script>
export default {
  data() {
    return {
      isBurgerMenuActive: false
    };
  },
  computed: {
    isAuth() {
      return this.$store.state.auth.isAuth;
    }
  },
  methods: {
    logout() {
      this.$store.dispatch("auth/logout");

      this.isBurgerMenuActive = false;
      this.$router.push({ name: "login" });
    }
  }
};
</script>
