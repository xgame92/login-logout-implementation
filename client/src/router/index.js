import Vue from 'vue'
import Router from 'vue-router'
import { sync } from 'vuex-router-sync'
import store from '../store'
import routes from './routes'

Vue.use(Router)

const router = createRouter()

router.beforeEach((to, from, next) => {
  if (to.name === 'private') {
    const token = localStorage.getItem('token')
    if (token === null) {
      next({ name: 'login' })
    }
    next()
  } else {
    next()
  }
})

sync(store, router)

export default router

/**
 * Create a new router instance.
 *
 * @return {Router}
 */
function createRouter() {
  const router = new Router({
    mode: 'history',
    routes,
  })
  return router
}
