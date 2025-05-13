<template>
  <div class="about">
    <video class="background-video" autoplay muted loop playsinline>
      <source src="@/assets/background.webm" type="video/webm">
    </video>
    <div class="blur-overlay"></div>
    <div class="content">
      <h1>About Page</h1>
    </div>
  </div>
</template>

<script>
export default {
  name: 'About'
}
</script>

<style>
.about {
  position: relative;
  height: 100vh;
  width: 100vw;
  overflow: hidden;
  color: var(--text-primary-col);
  display: flex;
  justify-content: center;
  align-items: center;
}

.background-video {
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  object-fit: cover;
  z-index: 1;
}

.blur-overlay {
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  backdrop-filter: blur(16px);
  -webkit-backdrop-filter: blur(16px);
  z-index: 2;
}

.content {
  position: relative;
  z-index: 3;
  padding: 20px;
  text-align: center;
}
</style> 