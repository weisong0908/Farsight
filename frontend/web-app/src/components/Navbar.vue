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
                <span class="icon"> <i class="fas fa-user"></i> </span>
                <span>
                  {{ $store.state.auth.user.username }}
                </span>
              </router-link>
              <a class="button is-primary" @click="logout">Log Out</a>
            </div>
          </div>
        </template>
        <template v-else>
          <div class="navbar-item">
            <div class="buttons">
              <router-link
                class="button is-primary"
                :to="{
                  name: 'signUp',
                  query: { redirectTo: this.$route.path }
                }"
                @click.native="isBurgerMenuActive = false"
                ><strong>Sign Up</strong></router-link
              >

              <router-link
                class="button is-light"
                :to="{ name: 'Login', query: { redirectTo: this.$route.path } }"
                @click.native="isBurgerMenuActive = false"
                >Log In</router-link
              >
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
    logout() {
      this.$store.dispatch("auth/logout");

      this.isBurgerMenuActive = false;
      this.$router.push({ name: "login" });
    }
  }
};
</script>
