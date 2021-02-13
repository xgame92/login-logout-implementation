<template>
  <div id="app">
    <div id="nav">
      <router-link
        to="/"
        class="btn btn-info m-2 text-white"
      >
        Home
      </router-link>
      <router-link
        to="/login"
        class="btn btn-info m-2 text-white"
      >
        Login
      </router-link>
      <router-link
        v-if="isUserLoggedIn"
        to="/private"
        class="btn btn-info m-2 text-white"
      >
        Private Page
      </router-link>
      <router-link
        v-if="isUserLoggedIn"
        to="/"
        class="btn btn-info m-2 text-white"
      >
        <span @click="onClickLogOut">Log Out</span>
      </router-link>
    </div>
    <router-view />
  </div>
</template>
<script>
import { mapGetters } from 'vuex'
import Vue from 'vue'

export default {
  name: 'App',
  components: {},
  data() {
    return {
    }
  },
  computed: {
    ...mapGetters('auth', ['isUserLoggedIn']),
  },
  methods: {
    onClickLogOut() {
      localStorage.removeItem('token')
      location.reload()
    },
  },
}
</script>
<style lang="scss">
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  display: flex;
  flex-direction: column;
}

#nav {
  padding: 30px;
}

#nav a {
  font-weight: bold;
  color: #2c3e50;
}

#nav a.router-link-exact-active {
  color: #42b983;
}
</style>
