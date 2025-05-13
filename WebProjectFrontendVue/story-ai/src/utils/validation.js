/**
 * Validation utility functions for form inputs
 */

/**
 * Validates an email address
 * @param {string} email - The email to validate
 * @returns {Object} - Validation result with isValid and errorMessage
 */
export function validateEmail(email) {
  if (!email) {
    return {
      isValid: false,
      errorMessage: 'This field is required'
    }
  }
  
  const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/
  if (!emailRegex.test(email)) {
    return {
      isValid: false,
      errorMessage: 'Please enter a valid email address'
    }
  }
  
  return {
    isValid: true,
    errorMessage: ''
  }
}

/**
 * Validates a name field
 * @param {string} name - The name to validate
 * @returns {Object} - Validation result with isValid and errorMessage
 */
export function validateName(name) {
  if (!name) {
    return {
      isValid: false,
      errorMessage: 'This field is required'
    }
  }
  
  if (name.trim() === '') {
    return {
      isValid: false,
      errorMessage: 'Name cannot be empty'
    }
  }
  
  return {
    isValid: true,
    errorMessage: ''
  }
}

/**
 * Validates a password
 * @param {string} password - The password to validate
 * @returns {Object} - Validation result with isValid and errorMessage
 */
export function validatePassword(password) {
  if (!password) {
    return {
      isValid: false,
      errorMessage: 'This field is required'
    }
  }
  
  const passwordRegex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$/
  if (!passwordRegex.test(password)) {
    return {
      isValid: false,
      errorMessage: 'Password must be at least 8 characters with at least one uppercase letter, one lowercase letter, one number, and one special character'
    }
  }
  
  return {
    isValid: true,
    errorMessage: ''
  }
}

/**
 * Validates that passwords match
 * @param {string} password - The original password
 * @param {string} repeatPassword - The repeated password to validate
 * @returns {Object} - Validation result with isValid and errorMessage
 */
export function validatePasswordMatch(password, repeatPassword) {
  if (!repeatPassword) {
    return {
      isValid: false,
      errorMessage: 'This field is required'
    }
  }
  
  if (password !== repeatPassword) {
    return {
      isValid: false,
      errorMessage: 'Passwords do not match'
    }
  }
  
  return {
    isValid: true,
    errorMessage: ''
  }
}

/**
 * Get validation function by type
 * @param {string} type - The validation type
 * @returns {Function} - The validation function
 */
export function getValidationFunctionByType(type) {
  switch (type) {
    case 'email':
      return validateEmail
    case 'name':
      return validateName
    case 'password':
      return validatePassword
    default:
      return () => ({ isValid: true, errorMessage: '' })
  }
} 