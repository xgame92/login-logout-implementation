import Vue from 'vue'
import Vuex from 'vuex'
import AuthStore from './modules/auth'
Vue.use(Vuex)

const modules = {
  auth: AuthStore,
}
export default new Vuex.Store({
  modules,
})
