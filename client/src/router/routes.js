import Home from '@/views/Home'
import LoginPage from '@/views/LoginPage'
import PrivatePage from '@/views/PrivatePage'

export default [
  {
    path: '/',
    name: 'home',
    component: Home,
  },
  {
    path: '/login',
    name: 'login',
    component: LoginPage,
  }, {
    path: '/private',
    name: 'private',
    component: PrivatePage,
  },
]
