import { createRouter, createWebHistory } from 'vue-router'
import Home from '../views/Home.vue'
import About from '../views/About.vue'
import NewStory from '../views/story/NewStory.vue'
import Story from '../views/story/Story.vue'
import SignIn from '../views/auth/SignIn.vue'
import Login from '../views/auth/Login.vue'

const routes = [
  {
    path: '/',
    name: 'Home',
    component: Home
  },
  {
    path: '/about',
    name: 'About',
    component: About
  },
  {
    path: '/new-story',
    name: 'NewStory',
    component: NewStory
  },
  {
    path: '/story',
    name: 'Story',
    component: Story
  },
  {
    path: '/signin',
    name: 'SignIn',
    component: SignIn
  },
  {
    path: '/login',
    name: 'Login',
    component: Login
  }
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

export default router
