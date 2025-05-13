<template>
  <transition name="slide-down">
    <div v-if="errorMessage" class="error-container">
      <div class="error-message">
        <span class="error-text">{{ errorMessage }}</span>
        <button class="error-close" @click="closeError">×</button>
      </div>
    </div>
  </transition>
</template>

<script>
import { ref } from 'vue'

export default {
  name: 'ErrorHandler',
  
  setup(props, { emit }) {
    const errorMessage = ref(null)

    const handleError = (message) => {
      errorMessage.value = message
      // Auto-close after 5 seconds
      setTimeout(() => {
        errorMessage.value = null
      }, 5000)
    }

    const closeError = () => {
      errorMessage.value = null
    }

    return {
      errorMessage,
      handleError,
      closeError
    }
  }
}
</script>

<style scoped>
.error-container {
  position: fixed;
  top: 20px;
  left: 50%;
  transform: translateX(-50%);
  z-index: 9999;
  width: 80%;
  max-width: 500px;
}

.error-message {
  background-color: #2c0a0e;
  color: #ff8a94;
  padding: 12px 18px;
  border-radius: 16px;
  display: flex;
  justify-content: space-between;
  box-shadow: 0 4px 20px rgba(0, 0, 0, 0.25);
  align-items: center;
  border: 1px solid #662026;
  font-weight: 200;
}

/* Light theme error styles - keeping same colors since errors are semantic */
html.light-theme .error-message {
  box-shadow: 0 4px 20px rgba(0, 0, 0, 0.15);
}

.error-text {
  flex-grow: 1;
  text-align: left;
  font-size: 14px;
}

.error-close {
  background: none;
  border: none;
  color: #ff8a94;
  font-size: 22px;
  cursor: pointer;
  padding: 0 5px;
  transition: all 0.3s ease;
}

.error-close:hover {
  opacity: 0.6;
}

/* Slide-down animation */
.slide-down-enter-active,
.slide-down-leave-active {
  transition: all 0.4s ease;
}

.slide-down-enter-from {
  transform: translate(-50%, -100%);
  opacity: 0;
}

.slide-down-leave-to {
  transform: translate(-50%, -100%);
  opacity: 0;
}
</style> 