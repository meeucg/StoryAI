// API service for all backend requests

// Use the proxy configured in vue.config.js
const API_BASE_URL = '/api';

// Common fetch options
const fetchOptions = {
  headers: {
    'Accept': 'application/json',
    'Content-Type': 'application/json'
  },
  cache: 'no-cache'
};

/**
 * Start a new session
 * @returns {Promise} Promise with the response
 */
export async function startSession() {
  console.log('Calling API: Start session');
  return fetch(`${API_BASE_URL}/story/start`, {
    method: 'GET',
    ...fetchOptions
  });
}

/**
 * Generate an image based on a prompt
 * @param {string} prompt - The prompt to generate the image from
 * @param {string} model - The model to use for generation
 * @returns {Promise} Promise with the response
 */
export async function generateImage(prompt, model = "gemini") {
  console.log('Calling API: Generate image with prompt:', prompt);
  return fetch(`${API_BASE_URL}/generate/image`, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json'
      // No Accept header - allow any response type
    },
    body: JSON.stringify({
      prompt,
      model
    }),
    cache: 'no-cache'
  });
}

/**
 * Select an option and get the next state
 * @param {number} index - The index of the selected option
 * @returns {Promise} Promise with the response
 */
export async function selectOption(index) {
  console.log('Calling API: Select option', index);
  return fetch(`${API_BASE_URL}/story/next/${index}`, {
    method: 'GET',
    ...fetchOptions
  });
}

/**
 * Submit custom text action
 * @param {string} text - The custom text to submit
 * @returns {Promise} Promise with the response
 */
export async function submitCustomText(text) {
  console.log('Calling API: Submit custom text:', text);
  return fetch(`${API_BASE_URL}/story/next`, {
    method: 'POST',
    ...fetchOptions,
    body: JSON.stringify({
      action: text
    })
  });
} 