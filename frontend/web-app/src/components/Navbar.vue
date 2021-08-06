<template>
  <nav class="navbar" role="navigation" aria-label="main navigation">
    <div class="navbar-brand">
      <router-link class="navbar-item" to="/">
        <img src="../assets/logo.png" width="112" height="28" />
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
      </div>

      <div class="navbar-end">
        <template v-if="isAuth">
          <div class="navbar-item">
            <div class="buttons">
              <router-link
                class="button is-light"
                :to="{
                  name: 'myAccount',
                  query: { redirectTo: this.$route.path }
                }"
                @click.native="isBurgerMenuActive = false"
              >
                <span>
                  {{ $store.state.auth.user.username }}
                </span>
                <span class="icon"> <i class="fas fa-user"></i> </span>
              </router-link>
              <a class="button is-light is-warning" @click="logout">
                <span>
                  Log Out
                </span>
                <span class="icon"> <i class="fas fa-sign-out-alt"></i> </span>
              </a>
            </div>
          </div>
        </template>
        <template v-else>
          <div class="navbar-item">
            <div class="buttons">
              <router-link
                class="button is-dark"
                :to="{
                  name: 'signUp',
                  query: { redirectTo: this.$route.path }
                }"
                @click.native="isBurgerMenuActive = false"
                ><strong>Sign Up</strong></router-link
              >

              <router-link
                class="button is-light"
                :to="{ name: 'login', query: { redirectTo: this.$route.path } }"
                @click.native="isBurgerMenuActive = false"
              >
                <span>
                  Log In
                </span>
                <span class="icon"> <i class="fas fa-sign-in-alt"></i> </span>
              </router-link>
            </div>
          </div>
        </template>
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
    async logout() {
      await this.$store.dispatch("auth/logout");
      await this.$store.dispatch("auth/clearSilentRefresh");
      this.isBurgerMenuActive = false;
      this.$router.push({ name: "login" });
    }
  }
};
</script>
