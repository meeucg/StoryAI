<template>
  <div class="theme-switch-button" @click="toggleTheme" :class="{ 'light-mode': !isDarkMode }">
    <img class="moon-icon" src="@/assets/moon.svg" alt="Dark mode" />
    <img class="sun-icon" src="@/assets/sun.svg" alt="Light mode" />
  </div>
</template>

<script>
import { ref, onMounted, watch } from 'vue'

export default {
    name: 'ThemeSwitchButton',
    setup() {
        // Initialize based on stored preference or default to dark mode
        const isDarkMode = ref(true)
        
        // Function to apply theme to HTML element
        const applyTheme = (isDark) => {
            if (isDark) {
                document.documentElement.classList.remove('light-theme')
            } else {
                document.documentElement.classList.add('light-theme')
            }
            // Store theme preference in localStorage
            localStorage.setItem('isDarkMode', isDark)
        }

        // Toggle between dark and light mode
        const toggleTheme = () => {
            isDarkMode.value = !isDarkMode.value
            applyTheme(isDarkMode.value)
        }
        
        // On component mount, check localStorage for theme preference
        onMounted(() => {
            const storedPreference = localStorage.getItem('isDarkMode')
            if (storedPreference !== null) {
                isDarkMode.value = storedPreference === 'true'
                applyTheme(isDarkMode.value)
            } else {
                // Apply default dark theme
                applyTheme(true)
            }
        })

        return {
            isDarkMode,
            toggleTheme
        }
    }
}
</script>

<style scoped>
.theme-switch-button {
    width: 36px;
    height: 36px;
    border-radius: 50%;
    background-color: var(--tetriary-dark-col);
    display: flex;
    justify-content: center;
    align-items: center;
    cursor: pointer;
    position: relative;
    overflow: hidden;
    transition: background-color 0.3s ease;
}

.moon-icon, .sun-icon {
    position: absolute;
    width: 16px;
    height: 16px;
    filter: invert(100%);
    transition: transform 0.3s ease;
}

/* Invert sun icon color in light mode */
.light-mode .sun-icon {
    filter: invert(30%);
}

/* Invert moon icon color in light mode when hovered */
.light-mode:hover .moon-icon {
    filter: invert(30%);
}

.moon-icon {
    transform: translateY(0);
}

.sun-icon {
    transform: translateY(40px);
}

.theme-switch-button:hover {
    background-color: var(--tetriary-light-col);
}

.theme-switch-button:not(.light-mode):hover .moon-icon {
    transform: translateY(-40px);
}

.theme-switch-button:not(.light-mode):hover .sun-icon {
    transform: translateY(0);
}

.light-mode .moon-icon {
    transform: translateY(-40px);
}

.light-mode .sun-icon {
    transform: translateY(0);
}

.light-mode:hover .moon-icon {
    transform: translateY(0);
}

.light-mode:hover .sun-icon {
    transform: translateY(40px);
}

.control {
    position: relative;
    display: inline-flex;
    align-items: center;
    gap: 12px;
    cursor: pointer;
    background-color: var(--tetriary-dark-col);
    padding: 0.6rem 1.2rem;
    border-radius: 18px;
    transition: all 0.3s ease;
}

.control:hover {
    background-color: var(--tetriary-light-col);
}
</style> 