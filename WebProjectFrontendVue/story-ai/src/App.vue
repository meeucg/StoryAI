<template>
  <ErrorHandler ref="errorHandler" />
  <Header />
  <router-view v-slot="{ Component }">
    <transition name="page" mode="out-in">
      <component :is="Component" @error="handleError" />
    </transition>
  </router-view>
</template>

<script>
import { ref } from 'vue'
import Header from '@/components/header/Header.vue'
import ErrorHandler from '@/components/ErrorHandler.vue'

export default {
  components: { 
    Header,
    ErrorHandler
  },
  setup() {
    const errorHandler = ref(null)
    
    const handleError = (message) => {
      if (errorHandler.value) {
        errorHandler.value.handleError(message)
      }
    }
    
    return {
      errorHandler,
      handleError
    }
  }
}
</script>

<style>
#app {
  font-family: 'Google Sans', Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: var(--text-primary-col);
}

/* Add global transition for theme changes */
*, *::before, *::after {
  transition: background-color 0.3s ease, color 0.3s ease, border-color 0.3s ease, box-shadow 0.3s ease;
}

.page-enter-active,
.page-leave-active {
  transition: opacity 0.3s, transform 0.3s;
}

.page-enter-from {
  opacity: 0;
  transform: translateY(20px);
}

.page-leave-to {
  opacity: 0;
  transform: translateY(-20px);
}
</style>
