import { ref } from 'vue';

export const parseText = (text) => {
  const fragments = [];
  let startIndex = 0;
  
  const spanRegex = /<span\s+([^>]*)>(.*?)<\/span>/g;
  let match;
  
  while ((match = spanRegex.exec(text)) !== null) {
    if (match.index > startIndex) {
      fragments.push({
        type: 'text',
        content: text.substring(startIndex, match.index)
      });
    }
    
    const spanAttrs = match[1];
    const spanContent = match[2];
    fragments.push({
      type: 'span',
      attrs: spanAttrs,
      content: spanContent
    });
    
    startIndex = match.index + match[0].length;
  }
  
  if (startIndex < text.length) {
    fragments.push({
      type: 'text',
      content: text.substring(startIndex)
    });
  }
  
  return fragments;
};

export const useSimpleTyping = (text, speed = 50, hideCursorOnComplete = true) => {
  const displayedText = ref('');
  let typingInterval = null;
  
  const startTyping = (callback) => {
    const fragments = parseText(text);
    let fragmentIndex = 0;
    let charIndex = 0;
    let currentText = '';
    
    displayedText.value = '<span class="cursor">_</span>';
    
    const updateText = () => {
      displayedText.value = currentText + '<span class="cursor">_</span>';
    };
    
    const getNextChar = () => {
      if (fragmentIndex >= fragments.length) {
        return null;
      }
      
      const fragment = fragments[fragmentIndex];
      
      if (fragment.type === 'text') {
        if (charIndex < fragment.content.length) {
          return fragment.content[charIndex++];
        } else {
          fragmentIndex++;
          charIndex = 0;
          return getNextChar();
        }
      } else if (fragment.type === 'span') {
        if (charIndex === 0) {
          charIndex++;
          return `<span ${fragment.attrs}>`;
        } 
        else if (charIndex <= fragment.content.length) {
          return fragment.content[charIndex++ - 1];
        }
        else if (charIndex === fragment.content.length + 1) {
          fragmentIndex++;
          charIndex = 0;
          return '</span>';
        }
      }
      
      return getNextChar();
    };
    typingInterval = setInterval(() => {
      const nextChar = getNextChar();
      
      if (nextChar !== null) {
        currentText += nextChar;
        updateText();
      } else {
        clearInterval(typingInterval);
        
        if (hideCursorOnComplete) {
          displayedText.value = currentText;
        }
        
        if (callback) callback();
      }
    }, speed);
    
    return typingInterval;
  };
  
  const stopTyping = () => {
    if (typingInterval) {
      clearInterval(typingInterval);
      typingInterval = null;
    }
  };
  
  return {
    displayedText,
    startTyping,
    stopTyping
  };
};

export const typingAnimationStyles = `
.cursor {
  opacity: 0.5;
  animation: blink 1s step-end infinite;
}

@keyframes blink {
  0%, 100% { opacity: 0.5; }
  50% { opacity: 0; }
}
`; 