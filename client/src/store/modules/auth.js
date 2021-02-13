import Vue from 'vue'
import Swal from 'sweetalert2'

export default {
  namespaced: true,
  state: {
    isLoading: false,
  },
  getters: {
    isUserLoggedIn: (state) => {
      return !!localStorage.getItem('token')
    },
  },
  mutations: {
    setState(state, isLoading) {
      state.isLoading = isLoading
    },
  },
  actions: {
    async login({ commit, dispatch, state }, user) {
      commit('setState', true)
      Vue.$axios.post(`/api/v1/auth/login`, {
        username: user.username,
        password: user.password,
      })
      .then((response) => {
        const { token } = response.data
        commit('setState', false)
        localStorage.setItem('token', token)
        setTimeout(() => {
          user.router.push({ path: '/private' })
          location.reload()
        }, 1500)
      }).catch((error) => {
        commit('setState', false)
        Swal.fire({
          icon: 'error',
          title: 'Oops...',
          text: error.response.data.errorMessage,
          reverseButtons: true,
          confirmButtonText: 'Ok',
          cancelButtonText: 'Cancel',
        })
      })
    },
  },
}
