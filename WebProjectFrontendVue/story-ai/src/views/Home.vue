<template>
  <div>
  <div class="introduction-view" ref="introView">
    <video class="background-video" autoplay muted loop playsinline>
      <source src="@/assets/background.webm" type="video/webm">
    </video>
    <div class="blur-overlay"></div>
    <div class="home-text-container">
      <a class="home-title" v-html="titleText"></a>
      <a class="home-subtitle" v-html="subtitleText"></a>
    </div>
    <button class="new-story-button" id="newStoryButton">
      New story
      <img src="@/assets/plus.svg" alt="+" class="plus-icon" />
    </button>
  </div>

  <div class="featured-view" ref="featuredView">
    <HeaderSpacer/>
    <div class="carousel-wrapper">
      <CarouselHorizontal 
        :items="featuredItems"
        elementWidth=600
        gap=-50
        additionalElementsCount=2
        elementComponentName=""
        animationTime=800>
        <template #default="{ item }">
            <FeaturedCarouselElement :item="item"/>
        </template>
      </CarouselHorizontal>
    </div>
  </div>
  
  <div class="about-view" ref="aboutView">
    <HeaderSpacer/>
    <div class="about-content">
      <h2>About Story AI</h2>
      <p>Story AI is an interactive platform that helps you create captivating stories with the assistance of artificial intelligence. Our goal is to make storytelling more accessible and enjoyable for everyone.</p>
      <LoadingIcon/>
    </div>
  </div>
</div>
</template>

<script>
import { onMounted, onBeforeUnmount, ref } from 'vue'
import { useSimpleTyping } from '@/utils/typingAnimation'
import { 
  useVerticalScrollGestureDetection, 
  SCROLL_UP_EVENT, 
  SCROLL_DOWN_EVENT,
} from '@/utils/gestureRecognition'
import FeaturedCarouselElement from '@/components/carousel/FeaturedCarouselElement.vue'
import CarouselHorizontal from '@/components/carousel/CarouselHorizontal.vue'
import LoadingIcon from '@/components/icons/IconLoading.vue'
import HeaderSpacer from '@/components/header/HeaderSpacer.vue'
export default {
  name: 'Home',
  components: {
    FeaturedCarouselElement,
    CarouselHorizontal,
    LoadingIcon,
    HeaderSpacer
  },
  setup() {
    const fullTitle = 'Story <span style="font-weight: 700;">AI</span>'
    const fullSubtitle = 'Create your own interactive story on the fly, with AI.'
    const featuredView = ref(null)
    const introView = ref(null)
    const aboutView = ref(null)
    
    const featuredItems = [
      { id: 0, imageUrl: '/featured-img/3.png', title: 'Featured Story 1', description: 'An exciting adventure awaits...', toName: "Story" },
      { id: 1, imageUrl: '/featured-img/1.png', title: 'Featured Story 2', description: 'Discover new worlds and characters...', toName: "Story" },
      { id: 2, imageUrl: '/featured-img/2.png', title: 'Featured Story 3', description: 'Embark on an epic journey...', toName: "Story" },
      { id: 3, imageUrl: '/featured-img/3.png', title: 'Featured Story 4', description: 'Create your own destiny...', toName: "Story" },
      { id: 4, imageUrl: '/featured-img/1.png', title: 'Featured Story 5', description: 'Uncover ancient mysteries...', toName: "Story" },
      { id: 5, imageUrl: '/featured-img/2.png', title: 'Featured Story 6', description: 'Dive into a new narrative...', toName: "Story" },
    ]
    
    const isAnimating = ref(false)
    
    const { displayedText: titleText, startTyping: startTitleTyping, stopTyping: stopTitleTyping } = 
      useSimpleTyping(fullTitle, 150, true)
    
    const { displayedText: subtitleText, startTyping: startSubtitleTyping, stopTyping: stopSubtitleTyping } = 
      useSimpleTyping(fullSubtitle, 35, true)
    
    const { 
      startDetection: startVerticalDetection, 
      stopDetection: stopVerticalDetection, 
      resetAccumulation: resetVerticalAccumulation 
    } = useVerticalScrollGestureDetection({
      threshold: 200,
      debounceTime: 300
    })
    
    const getCurrentSection = () => {
      const scrollY = window.scrollY
      const introHeight = introView.value ? introView.value.offsetHeight : 0
      const featuredHeight = featuredView.value ? featuredView.value.offsetHeight : 0
      
      if (scrollY < introHeight / 2) {
        return 'intro'
      } else if (scrollY < introHeight + featuredHeight / 2) {
        return 'featured'
      } else {
        return 'about'
      }
    }
    
    const scrollToTarget = (target) => {
      if (isAnimating.value) return false
      
      isAnimating.value = true
      stopVerticalDetection()
      resetVerticalAccumulation()
      
      window.scrollTo({
        top: target,
        behavior: 'smooth'
      })
      
      setTimeout(() => {
        isAnimating.value = false
        startVerticalDetection()
      }, 1000)
      
      return true
    }
    
    const handleScrollUp = () => {
      if (isAnimating.value) return
      
      const currentSection = getCurrentSection()
      
      if (currentSection === 'featured') {
        const newStoryButton = document.getElementById('newStoryButton');
        newStoryButton.style.bottom = '40px'
        scrollToTarget(0)
      } else if (currentSection === 'about') {
        if (featuredView.value) {
          scrollToTarget(introView.value.offsetHeight)
        }
      }
    }
    
    const handleScrollDown = () => {
      if (isAnimating.value) return
      
      const currentSection = getCurrentSection()
      
      if (currentSection === 'intro') {
        if (featuredView.value) {
          const newStoryButton = document.getElementById('newStoryButton');
          newStoryButton.style.bottom = '-60px'
          scrollToTarget(featuredView.value.offsetTop)
        }
      } else if (currentSection === 'featured') {
        if (aboutView.value) {
          scrollToTarget(aboutView.value.offsetTop)
        }
      }
    }
    
    const handleScroll = () => {
      if (!isAnimating.value) return
      
      clearTimeout(window.scrollEndTimer)
      window.scrollEndTimer = setTimeout(() => {
        isAnimating.value = false
      }, 150)
    }
    
    onMounted(() => {
      startTitleTyping(() => {
        setTimeout(() => {
          startSubtitleTyping()
        }, 500)
      })
      
      startVerticalDetection()
      
      window.addEventListener(SCROLL_UP_EVENT, handleScrollUp)
      window.addEventListener(SCROLL_DOWN_EVENT, handleScrollDown)
      window.addEventListener('scroll', handleScroll, { passive: true })
    })
    
    onBeforeUnmount(() => {
      stopTitleTyping()
      stopSubtitleTyping()
      
      stopVerticalDetection()
      window.removeEventListener(SCROLL_UP_EVENT, handleScrollUp)
      window.removeEventListener(SCROLL_DOWN_EVENT, handleScrollDown)
      window.removeEventListener('scroll', handleScroll)
      clearTimeout(window.scrollEndTimer)
    })
    
    return {
      titleText,
      subtitleText,
      featuredItems,
      featuredView,
      introView,
      aboutView
    }
  }
}
</script>

<style>
.header-spacer {
  height: var(--header-height);
}

.introduction-view {
  position: relative;
  overflow: hidden;
  color: white;
  height: 100vh;
  width: 100vw;
  background-image: url('@/assets/background.png');
  background-size: cover;
  background-position: center;
  background-repeat: no-repeat;
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  scroll-margin-top: 0;
}

.background-video {
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  object-fit: cover;
  z-index: -1;
}

@keyframes blur-overlay {
  from {
    filter: blur(16px) brightness(1.2) saturate(1.2) hue-rotate(0deg);
  }
  to {
    filter: blur(16px) brightness(1.2) saturate(1.2) hue-rotate(360deg);
  }
}

.blur-overlay {
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  backdrop-filter: blur(16px);
  -webkit-backdrop-filter: blur(16px);
  filter: blur(16px) brightness(1.2) saturate(1.2) hue-rotate(180deg);
  animation: blur-overlay 15s ease-in-out infinite;
  z-index: 2;
}

.home-text-container {
  position: relative;
  z-index: 3;
  display: flex;
  flex-direction: column;
  align-items: center;
  text-align: left;
  justify-content: center;
  text-shadow: 0 0 100px rgba(224, 205, 121, 0.6);
  user-select: none;
  transition: all 0.3s ease;
  width: fit-content;
}

.home-text-container:hover {
  text-shadow: 0 0 100px rgba(224, 205, 121, 1);
}

.home-title {
  font-size: 6.5rem;
  font-weight: 200;
}

.home-subtitle {
  max-width: 50%;
  opacity: 0.7;
  margin-top: 1rem;
  font-size: 1.2rem;
  font-weight: 200;
  user-select: none;
}

@keyframes blink {
  0%, 100% { opacity: 0.5; }
  50% { opacity: 0; }
}

.new-story-button {
  position: fixed;
  bottom: 40px;
  left: 50%;
  transform: translateX(-50%);
  display: flex;
  align-items: center;
  justify-content: center;
  gap: 10px;
  background-color: transparent;
  color: white;
  font-size: 1rem;
  font-weight: 300;
  padding: 12px 24px;
  border: 2px solid white;
  border-radius: 100px;
  cursor: pointer;
  z-index: 10;
  transition: all 0.3s ease;
}

.new-story-button .plus-icon {
  width: 16px;
  height: 16px;
  filter: invert(100%);
  color: white;
}

.new-story-button:hover {
  background-color: rgba(255, 255, 255, 0.1);
  transform: translateX(-50%) scale(1.05);
}

.featured-view {
  height: 100vh;
  width: 100vw;
  position: relative;
  z-index: 3;
  display: flex;
  flex-direction: column;
  align-items: center;
  scroll-margin-top: 0;
}

.carousel-wrapper {
  flex: 1;
  display: flex;
  justify-content: center;
  align-items: center;
  width: 100%;
  height: 70%;
}

.about-view {
  height: 100vh;
  width: 100vw;
  position: relative;
  z-index: 3;
  display: flex;
  flex-direction: column;
  align-items: center;
  color: var(--text-primary-col);
  background-color: var(--primary-col);
  scroll-margin-top: 0;
}

.about-content {
  max-width: 800px;
  padding: 2rem;
  text-align: center;
}

.about-content h2 {
  font-size: 2.5rem;
  font-weight: 300;
  margin-bottom: 1.5rem;
}

.about-content p {
  font-size: 1.2rem;
  line-height: 1.6;
  opacity: 0.8;
}

html {
  scroll-behavior: smooth;
}
</style> 