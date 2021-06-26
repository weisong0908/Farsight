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
        <div class="navbar-item">
          <div class="buttons">
            <a class="button is-primary" @click="signUp">
              <strong>Sign up</strong>
            </a>
            <a class="button is-light" @click="logInOrOut">
              Log in
            </a>
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
  methods: {
    signUp() {
      alert("sign up");
      this.isBurgerMenuActive = false;
    },
    logInOrOut() {
      alert("logInOrOut");
      this.isBurgerMenuActive = false;
    }
  }
};
</script>
