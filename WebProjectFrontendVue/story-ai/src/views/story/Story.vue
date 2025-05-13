<template>
  <div>
    <HeaderSpacer />
    <transition name="fade" mode="out-in">
      <div v-if="isPageLoaded" class="story-container" key="story">
        <div class="story-image-container">
          <div class="story-image-box" id="image">
            <LoadingIcon v-if="imageLoading" size="10%" color="rgba(255,255,255,0.8)" />
          </div>
        </div>
        <div class="story-content-container">
          <div class="text" id="text">
            {{ currentText }}
          </div>
          <div class="options-container" id="options">
            <ButtonPrimary :text="option" v-for="option in currentOptions" :key="option" class="option" />
          </div>
        </div>
      </div>
      <div v-else class="loader-container" key="loader">
        <LoadingIcon />
      </div>
    </transition>
  </div>
</template>

<script>
import * as apiService from '@/utils/api';
import LoadingIcon from '@/components/icons/IconLoading.vue';
import HeaderSpacer from '@/components/header/HeaderSpacer.vue';
import ButtonPrimary from '@/components/buttons/ButtonPrimary.vue';
import { ref, onMounted } from 'vue';
import { useRouter } from 'vue-router';
import { getPalette } from '@/utils/getColors.js';

export default {
  name: 'Story',
  components: {
    LoadingIcon,
    HeaderSpacer,
    ButtonPrimary
  },
  setup(props, { emit }) {
    const isPageLoaded = ref(false);
    const imageLoading = ref(true);
    const textLoading = ref(true);
    const router = useRouter();

    const currentText = ref("");
    const currentImageUrl = ref("");
    const currentOptions = ref([
      "Option 1 asdfasfasdfasdfsadfasdfasd",
      "Option 2",
      "Option 3",
      "dasfasdfasdf",
      "asdfasdfkgk",
      "bhjklsdfgghjkl"
    ]);
    const currentColor = ref("");

    const handleStart = async () => {
      const response = await apiService.startSession();
      if (response.ok) {
        const data = await response.json();
        isPageLoaded.value = true;
        textLoading.value = false;
        currentText.value = data.description;
        currentOptions.value = data.options;
        handleImageGeneration(data.img);
      }
      else {
        // const error = await response.json();
        console.log(response.statusText);
        // console.log(error);
        emit('error', 'Failed to start story session');
        router.back();
      }
    }

    const handleImageGeneration = async (prompt) => {
      if (!prompt) return;

      imageLoading.value = true;

      try {
        const response = await apiService.generateImage(prompt);

        if (!response.ok) {
          throw new Error(`Server returned ${response.status}: ${response.statusText}`);
        }

        const blob = await response.blob();
        currentImageUrl.value = URL.createObjectURL(blob);
        let newColor = (await getPalette(currentImageUrl.value)).LightVibrant;
        currentColor.value = `rgba(${newColor.r}, ${newColor.g}, ${newColor.b}, 0.3)`;

      } catch (error) {
        console.error('Error generating image:', error);
      } finally {
        imageLoading.value = false;
      }
    }

    onMounted(() => {
      handleStart();
    });

    return {
      isPageLoaded,
      imageLoading,
      textLoading,
      currentOptions,
      currentText,
      currentImageUrl,
      currentColor
    }
  }
};
</script>

<style scoped>
.story-container {
  display: flex;
  flex-direction: row;
  align-items: center;
  justify-content: center;
  height: calc(100vh - var(--header-height));
}

.loader-container {
  display: flex;
  align-items: center;
  justify-content: center;
  height: calc(100vh - var(--header-height));
}

.story-image-container {
  width: 50%;
  height: 100%;
  /* background-color: red; */
  display: flex;
  align-items: center;
  justify-content: center;
  padding: 32px 0px 64px 64px;
  box-sizing: border-box;
}

.story-content-container {
  width: 50%;
  height: 100%;
  /* background-color: blue; */
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: baseline;
  gap: 32px;
  padding: 32px 64px 32px 64px;
  box-sizing: border-box;
}

.story-image-box {
  width: 100%;
  height: 100%;
  box-sizing: border-box;
  border-radius: 16px;
  background-color: #0f0f0f;
  border: #333 solid 2px;
  background-image: v-bind("`url(${currentImageUrl})`");
  background-size: contain;
  background-position: center;
  background-repeat: no-repeat;
  box-shadow: v-bind("`0 0 256px ${currentColor}`");
  display: flex;
  align-items: center;
  justify-content: center;
  transition: all 0.5s ease-in-out;
}

.options-container {
  display: flex;
  flex-direction: column;
  gap: 8px;
  padding-top: 16px;
  width: 100%;
}

.option {
  width: 100%;
}

.text {
  background-color: #222;
  color: rgba(255, 255, 255, 0.7);
  border-radius: 16px;
  padding: 32px;
  text-align: justify;
  font-size: 1rem;
  line-height: 1.6;
}

.fade-enter-active,
.fade-leave-active {
  transition: opacity 0.3s, transform 0.3s;
}

.fade-enter-from {
  opacity: 0;
  transform: translateY(20px);
}

.fade-leave-to {
  opacity: 0;
  transform: translateY(-20px);
}
</style>