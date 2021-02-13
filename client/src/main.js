import Vue from 'vue'
import { BootstrapVue, IconsPlugin } from 'bootstrap-vue'
import axios from 'axios'
import App from './App.vue'
import store from './store'
import router from './router'


// Import Bootstrap an BootstrapVue CSS files (order is important)
import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap-vue/dist/bootstrap-vue.css'

Vue.config.productionTip = false

// Install BootstrapVue
Vue.use(BootstrapVue)

// Optionally install the BootstrapVue icon components plugin
Vue.use(IconsPlugin)

const axiosInstance = axios.create({
  baseURL: process.env.VUE_APP_API_SERVER_URL,
  timeout: 10000,
})

Vue.$axios = axiosInstance

new Vue({
  store,
  router,
  render: (h) => h(App),
}).$mount('#app')
