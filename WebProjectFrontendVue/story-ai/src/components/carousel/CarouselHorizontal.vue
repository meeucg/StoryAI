<template>
    <div class="carousel-body" id="cBody">
        <div class="carousel-item-wrapper"
            v-for="item in items" :key="item.id" :id="`item-${item.id}`" :style="{
                left: 'calc(' + position(item.id) + ` + ${offset}px)`,
                scale: currentElementId == item.id ? 1 : 0.7,
                opacity: currentElementId == item.id ? 1 : 0.3
            }">
            <slot :item="item"></slot>
        </div>
    </div>
</template>

<script>
import { SCROLL_LEFT_EVENT, SCROLL_RIGHT_EVENT, useHorizontalScrollGestureDetection } from '@/utils/gestureRecognition';
import { ref, computed, onMounted, onBeforeUnmount } from 'vue';

export default {
    name: 'CarouselHorizontal',
    props: [
        "items",
        "elementWidth",
        "gap",
        "elementComponentName",
        "animationTime"
    ],
    setup(props) {
        const offset = ref(0);
        const theme = localStorage.getItem('isDarkMode') ?? 'true';

        const elementWidth = props.elementWidth;
        const gap = props.gap;
        const n = Math.ceil(props.items.length / 2) - 1;
        let currentElementId = ref(+n);

        const width = computed(() => `${+elementWidth}` !== 'NaN' ? elementWidth + 'px' : elementWidth);
        const marginHorizontal = computed(() => `${+gap}` !== 'NaN' ? gap + 'px' : gap);
        const position = ref((i) => `calc((${width.value} * ${+i - 0.5}) + (${marginHorizontal.value} * ${+i}) + 50vw) 
            - (${width.value} * ${+n}) - (${marginHorizontal.value} * ${+n})`);
        const animationTime = computed(() => `${+props.animationTime / 1000}s`)

        const dTime = props.animationTime;
        const {
            startDetection: startHorizontalDetection,
            stopDetection: stopHorizontalDetection,
            resetAccumulation: resetHorizontalAccumulation
        } = useHorizontalScrollGestureDetection({
            threshold: 200,
            debounceTime: dTime
        })

        onMounted(() => {
            startHorizontalDetection();

            let currentElement = document.getElementById(`item-${currentElementId.value}`).firstElementChild;
            currentElement.style.border = `2px solid ${theme === 'true' ? 'rgba(255,255,255,0.4)' : 'rgba(0,0,0,0.4)'}`;

            let handleSwipeLeft = (e) => {
                if (currentElementId.value <= 0){
                    window.addEventListener(SCROLL_LEFT_EVENT, handleSwipeLeft, { once: true });
                    return;
                }

                offset.value += +elementWidth + +gap;
                let currentElement = document.getElementById(`item-${currentElementId.value}`).firstElementChild;
                currentElement.style.border = `0px solid ${theme === 'true' ? 'rgba(0,0,0,0)' : 'rgba(255,255,255,0)'}`;
                currentElementId.value--;
                currentElement = document.getElementById(`item-${currentElementId.value}`).firstElementChild;
                currentElement.style.border = `2px solid ${theme === 'true' ? 'rgba(255,255,255,0.4)' : 'rgba(0,0,0,0.4)'}`;

                stopHorizontalDetection();
                setTimeout(() => {
                    resetHorizontalAccumulation();
                    startHorizontalDetection();
                    window.addEventListener(SCROLL_LEFT_EVENT, handleSwipeLeft, { once: true });
                }, dTime)
            };

            let handleSwipeRight = (e) => {
                if (currentElementId.value >= props.items.length - 1){
                    window.addEventListener(SCROLL_RIGHT_EVENT, handleSwipeRight, { once: true });
                    return;
                }

                offset.value -= +elementWidth + +gap;
                let currentElement = document.getElementById(`item-${currentElementId.value}`).firstElementChild;
                currentElement.style.border = `0px solid ${theme === 'true' ? 'rgba(0,0,0,0)' : 'rgba(255,255,255,0)'}`;
                currentElementId.value++;
                currentElement = document.getElementById(`item-${currentElementId.value}`).firstElementChild;
                currentElement.style.border = `2px solid ${theme === 'true' ? 'rgba(255,255,255,0.4)' : 'rgba(0,0,0,0.4)'}`;

                stopHorizontalDetection();
                setTimeout(() => {
                    resetHorizontalAccumulation();
                    startHorizontalDetection();
                    window.addEventListener(SCROLL_RIGHT_EVENT, handleSwipeRight, { once: true });
                }, dTime)
            };

            window.addEventListener(SCROLL_LEFT_EVENT, handleSwipeLeft, { once: true });
            window.addEventListener(SCROLL_RIGHT_EVENT, handleSwipeRight, { once: true });
        })

        onBeforeUnmount(() => {
            stopHorizontalDetection();
        });

        return {
            width,
            marginHorizontal,
            offset,
            position,
            animationTime,
            currentElementId
        }
    }
}
</script>

<style>
.carousel-body {
    width: 100%;
    height: 100%;
    background-color: transparent;
    overflow: hidden;
    position: relative;
    touch-action: pan-y;
}

.carousel-item-wrapper {
    position: absolute;
    width: v-bind('width');
    height: 100%;
    transition: all v-bind('animationTime') ease;
    display: flex;
    justify-content: center;
    align-items: center;
    padding: 0px;
}

</style>