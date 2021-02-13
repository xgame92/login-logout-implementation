<template>
  <div class="container">
    <div class="row justify-content-md-center">
      <div class="col-8">
        <div class="card">
          <div class="card-header mb-2">
            Login Page
          </div>
          <div class="mb-3 row">
            <label
              for="username"
              class="col-sm-2 col-form-label"
            >Username</label>
            <div class="col-sm-10">
              <input
                id="username"
                v-model="username"
                type="text"
                :class="[!!usernameError === true ? 'form-control is-invalid': 'form-control']"
              >
              <div
                v-if="usernameError "
                :class="{'invalid-feedback text-align-left':!!usernameError===true}"
              >
                {{ usernameError }}
              </div>
            </div>
          </div>
          <div class="mb-3 row">
            <label
              for="inputPassword"
              class="col-sm-2 col-form-label"
            >Password</label>
            <div class="col-sm-10">
              <input
                id="inputPassword"
                v-model="password"
                type="password"
                :class="[!!passwordError === true ? 'form-control is-invalid': 'form-control']"
              >
              <div
                v-if="passwordError "
                :class="{'invalid-feedback text-align-left':!!passwordError===true}"
              >
                {{ passwordError }}
              </div>
            </div>
          </div>

          <div class="mb-3">
            <button
              class="btn btn-primary m-4"
              type="submit"
              @click="onClickLogin"
            >
              <span
                v-if="isLoading"
                class="spinner-border spinner-border-sm"
                role="status"
                aria-hidden="true"
              />
              {{ isLoading ? "Signing in":"Login" }}
            </button>
            <button
              class="btn btn-danger"
              type="submit"
              @click="onClickBack"
            >
              Back
            </button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { mapActions, mapState } from 'vuex'

export default {
  name: 'LoginPage',
  components: {},
  data() {
    return {
      username: '',
      password: '',
      usernameError: '',
      passwordError: '',
    }
  },
  computed: {
    ...mapState('auth', ['isLoading']),
  },
  mounted() {
  },
  methods: {
    ...mapActions('auth', [
      'login',
    ]),
    onClickBack() {
      this.$router.push('/')
    },
    onClickLogin() {
      if (this.username === '') {
        this.usernameError = 'Username is empty'
      } else if (Validate(this.username)) {
        this.usernameError = 'Username contains invalid characters'
      } else {
        this.usernameError = ''
      }

      if (this.password === '') {
        this.passwordError = 'Password is empty'
      } else if (Validate(this.password)) {
        this.passwordError = 'Password contains invalid characters'
      } else {
        this.passwordError = ''
      }

      if (!!this.usernameError || !!this.passwordError) {
        return
      }
      this.login({ username: this.username, password: this.password, router: this.$router })
    },
  },
}
function Validate(text) {
  const format = /[`^\\,.\/]/

  return format.test(text)
}
</script>

<style scoped>
.text-align-left{
  text-align: left;
}
</style>
