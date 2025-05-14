<template>
  <div class="menu-container">
    <div class="menu-button" @click="toggleMenu" :class="{ 'open': isOpen }">
      <div class="line line-top"></div>
      <div class="line line-middle"></div>
      <div class="line line-bottom"></div>
    </div>
    <div class="overlay" :class="{ 'active': isOpen }" @click="toggleMenu"></div>
    <div class="popup-menu" :class="{ 'show': isOpen }">
      <p class="menu-item" @click="handleRedirect('Home')">Home</p>
      <p class="menu-item" @click="handleRedirect('About')">About</p>
      <p class="menu-item" @click="handleRedirect('NewStory')">New story</p>
    </div>
  </div>
</template>

<script>
import { ref } from 'vue'
import { useRouter } from 'vue-router';

export default {
  name: 'MenuButton',
  setup() {
    const router = useRouter();

    const isOpen = ref(false)
    
    const toggleMenu = () => {
      isOpen.value = !isOpen.value
      
      if (isOpen.value) {
        document.body.style.overflow = 'hidden';
      } else {
        document.body.style.overflow = '';
      }
    }

    const handleRedirect = (name) => {
      router.push({ name });
      toggleMenu();
    }
    
    return {
      isOpen,
      toggleMenu,
      handleRedirect
    }
  }
}
</script>

<style scoped>
.menu-container {
  position: relative;
}

.menu-button {
  width: 36px;
  height: 36px;
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  cursor: pointer;
  transition: opacity 0.3s ease;
  position: relative;
  z-index: 101;
}

.line {
  width: 16px;
  height: 2px;
  background-color: var(--text-primary-col);
  position: absolute;
  transition: all 0.4s cubic-bezier(0.68, -0.6, 0.32, 1.6);
  transform-origin: center;
}

.line-top {
  transform: translateY(-5px);
}

.line-bottom {
  transform: translateY(5px);
}

.menu-button.open .line-top {
  transform: translateY(0);
  opacity: 0;
  width: 0;
}

.menu-button.open .line-middle {
  transform: rotate(90deg);
  width: 16px;
}

.menu-button.open .line-bottom {
  transform: translateY(0);
  opacity: 0;
  width: 0;
}

.menu-button:hover {
  opacity: 0.7;
}

.overlay {
  position: fixed;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  background-color: rgba(0, 0, 0, 0);
  z-index: 99;
  transition: background-color 0.3s ease;
  pointer-events: none;
}

.overlay.active {
  background-color: rgba(0, 0, 0, 0.5);
  pointer-events: auto;
}

.popup-menu {
  position: fixed;
  left: -250px;
  top: 0;
  width: 250px;
  height: 100vh;
  background-color: var(--primary-col);
  z-index: 100;
  padding-top: 80px;
  transition: left 0.3s ease-in-out;
  box-shadow: 2px 0 5px rgba(0, 0, 0, 0.3);
}

.popup-menu.show {
  left: 0;
}

.menu-item {
  font-size: 13px;
  font-weight: 200;
  color: var(--text-primary-col);
  user-select: none;
  padding: 0.6rem 1.2rem;
  transition: all 0.3s ease;
}

.menu-item:hover {
  cursor: pointer;
  opacity: 0.6;
}
</style> 