<template>
  <div class="carousel-element">
    <div class="image-container">
      <img :src="item.imageUrl" alt="Featured item" class="element-image" />
    </div>
    <div class="content-container">
      <div class="text-container">
        <h2 class="element-title">{{ item.title }}</h2>
        <p class="element-description">{{ item.description }}</p>
      </div>
      <ButtonPrimary text="Play" class="play-button" @click="navigateToStory"/>
    </div>
  </div>
</template>

<script>
import ButtonPrimary from '@/components/buttons/ButtonPrimary.vue';
import { useRouter } from 'vue-router';

export default {
  name: 'FeaturedCarouselElement',
  components: {
    ButtonPrimary
  },
  props: {
    item: {
      type: Object,
      required: true
    }
  },
  setup(props) {
    const router = useRouter();
    
    const navigateToStory = () => {
      router.push({ name: props.item.toName });
    };
    
    return {
      navigateToStory
    };
  }
}
</script>

<style scoped>
.carousel-element {
  display: flex;
  flex-direction: column;
  border-radius: 24px;
  overflow: hidden;
  transition: transform 0.3s ease;
  background-color: var(--secondary-col);
  color: var(--text-primary-col);
  width: 100%;
  aspect-ratio: 1;
  transition: all 500ms ease-in-out;
}

.image-container {
  width: calc(100% - 16px);
  height: 80%;
  margin: 8px;
  margin-bottom: 0px;
  border-radius: 16px;
  overflow: hidden;
}

.element-image {
  width: 100%;
  height: 100%;
  object-fit: cover;
  transition: transform 0.3s ease;
}

.content-container {
  display: flex;
  flex-direction: row;
  justify-content: space-between;
  align-items: last baseline;
  height: fit-content;
}

.text-container {
  display: flex;
  flex-direction: column;
  align-items: baseline;
  padding: 32px;
  padding-bottom: 0px;
  flex: 1;
}

.element-title {
  margin-top: 0;
  margin-bottom: 8px;
  font-weight: 500;
  font-size: 1.5rem;
}

.element-description {
  margin: 0;
  font-size: 0.9rem;
  opacity: 0.8;
  line-height: 1.4;
}

.carousel-element:hover {
  transform: translateY(-5px) scale(1.02);
}

.carousel-element:hover .element-image {
  transform: scale(1.05);
}

.play-button {
  margin: 32px;
}

</style> 