import { ref, computed } from 'vue'
import { validateEmail, validateName, validatePassword, validatePasswordMatch } from './validation'

/**
 * Setup for the SignIn form
 * @returns {Object} Form state and handlers
 */
export function useSignInForm() {
  const email = ref('')
  const name = ref('')
  const password = ref('')
  const repeatPassword = ref('')
  
  const validationState = ref({
    email: false,
    name: false,
    password: false,
    repeatPassword: false
  })
  
  const isFormValid = computed(() => {
    return Object.values(validationState.value).every(value => value)
  })
  
  const validationChanged = (field, isValid) => {
    validationState.value[field] = isValid
  }
  
  // Email validation function
  const validateEmailField = (value) => {
    return validateEmail(value)
  }
  
  // Name validation function
  const validateNameField = (value) => {
    return validateName(value)
  }
  
  // Password validation function
  const validatePasswordField = (value) => {
    return validatePassword(value)
  }
  
  // Repeat password validation function
  const validateRepeatPasswordField = (value, { passwordToMatch }) => {
    return validatePasswordMatch(passwordToMatch, value)
  }
  
  return {
    email,
    name,
    password,
    repeatPassword,
    isFormValid,
    validationChanged,
    validateEmailField,
    validateNameField,
    validatePasswordField,
    validateRepeatPasswordField
  }
}

/**
 * Setup for the Login form
 * @returns {Object} Form state and handlers
 */
export function useLoginForm() {
  const email = ref('')
  const password = ref('')
  
  const validationState = ref({
    email: false,
    password: false
  })
  
  const isFormValid = computed(() => {
    return Object.values(validationState.value).every(value => value)
  })
  
  const validationChanged = (field, isValid) => {
    validationState.value[field] = isValid
  }
  
  // Email validation function
  const validateEmailField = (value) => {
    return validateEmail(value)
  }
  
  // Password validation function - for login we don't need the full password complexity
  const validatePasswordField = (value) => {
    if (!value) {
      return {
        isValid: false,
        errorMessage: 'This field is required'
      }
    }
    
    return {
      isValid: true,
      errorMessage: ''
    }
  }
  
  return {
    email,
    password,
    isFormValid,
    validationChanged,
    validateEmailField,
    validatePasswordField
  }
} 