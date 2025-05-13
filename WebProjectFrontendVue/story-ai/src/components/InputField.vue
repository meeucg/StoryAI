<template>
  <div class="input-field-container">
    <label v-if="label" class="input-label">{{ label }}</label>
    <div class="input-wrapper">
      <input 
        :type="showPassword ? 'text' : type" 
        :placeholder="placeholder" 
        class="input-field" 
        :class="{ 'invalid': showError }"
        v-model="inputValue"
        @input="onInput"
        @blur="validateOnBlur"
      />
      <div 
        v-if="type === 'password'" 
        class="password-toggle"
        @click="togglePasswordVisibility"
      >
        <svg v-if="showPassword" class="eye-icon" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
          <path d="M17.94 17.94A10.07 10.07 0 0 1 12 20c-7 0-11-8-11-8a18.45 18.45 0 0 1 5.06-5.94M9.9 4.24A9.12 9.12 0 0 1 12 4c7 0 11 8 11 8a18.5 18.5 0 0 1-2.16 3.19m-6.72-1.07a3 3 0 1 1-4.24-4.24"></path>
          <line x1="1" y1="1" x2="23" y2="23"></line>
        </svg>
        <svg v-else class="eye-icon" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
          <path d="M1 12s4-8 11-8 11 8 11 8-4 8-11 8-11-8-11-8z"></path>
          <circle cx="12" cy="12" r="3"></circle>
        </svg>
      </div>
    </div>
    <div v-if="showError" class="error-message">{{ errorMessage }}</div>
  </div>
</template>

<script>
import { ref, computed, watch } from 'vue'

export default {
  name: 'InputField',
  props: {
    label: {
      type: String,
      default: ''
    },
    type: {
      type: String,
      default: 'text'
    },
    placeholder: {
      type: String,
      default: ''
    },
    modelValue: {
      type: String,
      default: ''
    },
    validateFunction: {
      type: Function,
      default: null
    },
    extraValidateParams: {
      type: Object,
      default: () => ({})
    }
  },
  emits: ['update:modelValue', 'validation-changed'],
  setup(props, { emit }) {
    const inputValue = ref(props.modelValue)
    const isValid = ref(true)
    const errorMessage = ref('')
    const isDirty = ref(false)
    const showError = computed(() => !isValid.value && isDirty.value)
    const showPassword = ref(false)
    
    // Update the input value when the prop changes
    watch(() => props.modelValue, (newValue) => {
      inputValue.value = newValue
    })
    
    // Watch for changes in extra validation parameters
    watch(() => props.extraValidateParams, () => {
      validate()
    }, { deep: true })
    
    const onInput = () => {
      emit('update:modelValue', inputValue.value)
      validate()
    }
    
    const validateOnBlur = () => {
      isDirty.value = true
      validate()
    }
    
    const validate = () => {
      // Default state is valid
      isValid.value = true
      errorMessage.value = ''
      
      // If no validation function, return
      if (!props.validateFunction) {
        emit('validation-changed', true)
        return
      }
      
      // Call the validation function with input value and any extra params
      const validationResult = props.validateFunction(
        inputValue.value,
        props.extraValidateParams
      )
      
      isValid.value = validationResult.isValid
      errorMessage.value = validationResult.errorMessage
      
      // Emit validation result to parent
      emit('validation-changed', isValid.value)
    }
    
    const togglePasswordVisibility = () => {
      showPassword.value = !showPassword.value
    }
    
    return {
      inputValue,
      isValid,
      errorMessage,
      showError,
      onInput,
      validate,
      validateOnBlur,
      showPassword,
      togglePasswordVisibility
    }
  }
}
</script>

<style scoped>
.input-field-container {
  margin-bottom: 1.2rem;
  width: 100%;
}

.input-label {
  display: block;
  text-align: left;
  margin-bottom: 0.6rem;
  font-size: 14px;
  font-weight: 200;
  color: var(--text-primary-col);
}

.input-wrapper {
  position: relative;
  display: flex;
  align-items: center;
}

.input-field {
  font-size: 14px;
  font-weight: 200;
  color: var(--text-primary-col);
  background-color: var(--tetriary-dark-col);
  padding: 0.7rem 1.2rem;
  padding-right: 2.5rem;
  border-radius: 16px;
  border: 1px solid var(--tetriary-light-col);
  width: 100%;
  transition: all 0.3s ease;
}

.input-field::placeholder {
  color: var(--text-secondary-col);
}

.input-field:focus {
  outline: none;
  background-color: var(--tetriary-light-col);
  border-color: #444;
}

.input-field:hover {
  background-color: #2a2a2a;
}

.input-field.invalid {
  border-color: #662026;
}

.error-message {
  color: #ff8a94;
  font-size: 12px;
  margin-top: 0.3rem;
  text-align: left;
  font-weight: 200;
}

.password-toggle {
  position: absolute;
  right: 12px;
  cursor: pointer;
  display: flex;
  align-items: center;
  justify-content: center;
  padding: 5px;
}

.eye-icon {
  width: 20px;
  height: 20px;
  stroke: var(--text-secondary-col);
  transition: stroke 0.3s ease;
}

.password-toggle:hover .eye-icon {
  stroke: var(--text-primary-col);
}
</style> 