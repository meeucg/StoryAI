<template>
  <div class="header">
    <div class="header-left">
      <MenuButton />
      <router-link to="/">
          <div class="header-logo-container">
              <img class="header-logo" src="@/assets/logo.png" alt="logo" />
              <p class="header-title">Story AI</p>
          </div>
      </router-link>
    </div>
    <div class="header-center">
      <SearchBar />
    </div>
    <div class="header-links-container">
        <ButtonPrimary text="Sign-in" @click="navigateToSignIn" />
        <ThemeSwitchButton />
    </div>
  </div>
</template>

<script>
import ButtonPrimary from '@/components/buttons/ButtonPrimary.vue'
import ThemeSwitchButton from '@/components/buttons/ThemeSwitchButton.vue'
import MenuButton from '@/components/buttons/MenuButton.vue'
import SearchBar from '@/components/header/SearchBar.vue'
import { useRouter } from 'vue-router'

export default {
    name: 'Header',
    setup() {
        const router = useRouter()
        
        const navigateToSignIn = () => {
            router.push({ name: 'SignIn' })
        }
        
        return {
            title: 'Header',
            navigateToSignIn
        }
    },
    components: {
        ButtonPrimary,
        ThemeSwitchButton,
        MenuButton,
        SearchBar
    }
}
</script>

<style>

.header {
    position: fixed;
    top: 0;
    left: 0;
    height: var(--header-height);
    width: 100%;
    display: flex;
    justify-content: space-between;
    align-items: center;
    background-color: var(--primary-col);
    z-index: 100;
}

.header-left {
    display: flex;
    align-items: center;
    margin-left: 15px;
}

.header-center {
    position: absolute;
    left: 50%;
    transform: translateX(-50%);
}

.header-logo {
    height: calc(var(--header-height) * 0.45);
    margin-right: 10px;
}

.header-title {
    font-size: 20px;
    font-weight: 350;
    color: var(--text-primary-col);
}

.header-logo-container {
    display: flex;
    flex-direction: row;
    align-items: center;
    margin-left: 20px;
    margin-top: calc(var(--header-height) * 0.02);
    transition: all 0.3s ease;
}

.header-logo-container:hover {
    cursor: pointer;
    opacity: 0.6;
}

a,p > .router-link-active {
    text-decoration: none;
    color: inherit;
}

.header-links-container {
    position: absolute;
    display: flex;
    flex-direction: row;
    align-items: center;
    gap: 15px;
    right: 20px;
}

.header-link {
    font-size: 13px;
    font-weight: 200;
    color: var(--text-primary-col);
    user-select: none;
    padding: 0.6rem 1.2rem;
    transition: all 0.3s ease;
}

.header-link:hover {
    cursor: pointer;
    opacity: 0.6;
}

</style>