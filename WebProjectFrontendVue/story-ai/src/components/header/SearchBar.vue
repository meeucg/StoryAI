<template>
  <div class="search-bar">
    <input type="text" placeholder="Search..." class="search-input" />
    <img src="@/assets/search.svg" alt="Search" class="search-icon" />
  </div>
</template>

<script>
export default {
  name: 'SearchBar'
}
</script>

<style scoped>
.search-bar {
  position: relative;
  display: flex;
  align-items: center;
}

.search-input {
  font-size: 13px;
  font-weight: 200;
  color: var(--text-primary-col);
  background-color: var(--tetriary-dark-col);
  padding: 0.6rem 1.2rem;
  border-radius: 100px;
  border: none;
  width: 200px;
  transition: all 0.3s ease;
}

.search-input::placeholder {
  color: var(--text-secondary-col);
}

.search-input:focus {
  outline: none;
  background-color: var(--tetriary-light-col);
  width: 250px;
}

.search-input:hover {
  background-color: var(--tetriary-light-col);
}

.search-icon {
  position: absolute;
  right: 12px;
  width: 16px;
  height: 16px;
  filter: invert(100%);
  opacity: 0.7;
  pointer-events: none;
}

/* Adjust search icon color for light theme */
html.light-theme .search-icon {
  filter: invert(30%);
}
</style> 