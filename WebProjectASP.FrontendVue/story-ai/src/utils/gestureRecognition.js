export const SCROLL_UP_EVENT = 'scroll-gesture-up';
export const SCROLL_DOWN_EVENT = 'scroll-gesture-down';
export const SCROLL_LEFT_EVENT = 'scroll-gesture-left';
export const SCROLL_RIGHT_EVENT = 'scroll-gesture-right';

const DEFAULT_OPTIONS = {
  threshold: 200,          
  resetAfterTrigger: true, 
  debounceTime: 200        
};

export const useVerticalScrollGestureDetection = (options = {}) => {
  const config = { ...DEFAULT_OPTIONS, ...options };
  
  let accumulatedDeltaY = 0;
  let lastEventTime = 0;
  let isActive = false;
  
  const handleWheel = (event) => {
    if (!isActive) return;
    
    const { deltaY } = event;
    
    accumulatedDeltaY += deltaY;
    
    const now = Date.now();
    
    if (now - lastEventTime > config.debounceTime) {
      if (Math.abs(accumulatedDeltaY) >= config.threshold) {
        const eventName = accumulatedDeltaY > 0 ? SCROLL_DOWN_EVENT : SCROLL_UP_EVENT;
        
        const customEvent = new CustomEvent(eventName, {
          detail: {
            distance: accumulatedDeltaY,
            timestamp: now
          }
        });
        
        window.dispatchEvent(customEvent);
        
        lastEventTime = now;
        
        if (config.resetAfterTrigger) {
          accumulatedDeltaY = 0;
        }
      }
    }
  };
  
  const startDetection = () => {
    if (!isActive) {
      window.addEventListener('wheel', handleWheel, { passive: true });
      isActive = true;
    }
  };
  
  const stopDetection = () => {
    if (isActive) {
      window.removeEventListener('wheel', handleWheel);
      isActive = false;
      accumulatedDeltaY = 0;
    }
  };
  
  const resetAccumulation = () => {
    accumulatedDeltaY = 0;
  };
  
  return {
    startDetection,
    stopDetection,
    resetAccumulation
  };
};

export const useHorizontalScrollGestureDetection = (options = {}) => {
  const config = { ...DEFAULT_OPTIONS, ...options };
  
  let accumulatedDeltaX = 0;
  let lastEventTime = 0;
  let isActive = false;
  
  const handleWheel = (event) => {
    if (!isActive) return;
    
    const { deltaX } = event;
    
    accumulatedDeltaX += deltaX;
    
    const now = Date.now();
    
    if (now - lastEventTime > config.debounceTime) {
      if (Math.abs(accumulatedDeltaX) >= config.threshold) {
        const eventName = accumulatedDeltaX > 0 ? SCROLL_RIGHT_EVENT : SCROLL_LEFT_EVENT;
        
        const customEvent = new CustomEvent(eventName, {
          detail: {
            distance: accumulatedDeltaX,
            timestamp: now
          }
        });
        
        window.dispatchEvent(customEvent);
        
        lastEventTime = now;
        
        if (config.resetAfterTrigger) {
          accumulatedDeltaX = 0;
        }
      }
    }
  };
  
  const startDetection = () => {
    if (!isActive) {
      window.addEventListener('wheel', handleWheel, { passive: true });
      isActive = true;
    }
  };
  
  const stopDetection = () => {
    if (isActive) {
      window.removeEventListener('wheel', handleWheel);
      isActive = false;
      accumulatedDeltaX = 0;
    }
  };
  
  const resetAccumulation = () => {
    accumulatedDeltaX = 0;
  };
  
  return {
    startDetection,
    stopDetection,
    resetAccumulation
  };
};

export const useScrollGestureDetection = (options = {}) => {
  const vertical = useVerticalScrollGestureDetection(options);
  const horizontal = useHorizontalScrollGestureDetection(options);
  
  const startDetection = () => {
    vertical.startDetection();
    horizontal.startDetection();
  };
  
  const stopDetection = () => {
    vertical.stopDetection();
    horizontal.stopDetection();
  };
  
  const resetAccumulation = () => {
    vertical.resetAccumulation();
    horizontal.resetAccumulation();
  };
  
  return {
    startDetection,
    stopDetection,
    resetAccumulation
  };
}; 