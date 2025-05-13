<template>
  <div>
    <HeaderSpacer />
    <div class="login-container">
      <div class="login-form">
        <h1 class="title">Login</h1>
        
        <div class="form-fields">
          <InputField 
            label="Email" 
            type="email" 
            placeholder="Enter your email" 
            v-model="email"
            :validate-function="validateEmailField"
            @validation-changed="validationChanged('email', $event)"
          />
          
          <InputField 
            label="Password" 
            type="password" 
            placeholder="Enter your password" 
            v-model="password"
            :validate-function="validatePasswordField"
            @validation-changed="validationChanged('password', $event)"
          />
        </div>
        
        <div class="button-container">
          <ButtonPrimary text="Login" :disabled="!isFormValid" @click="handleLogin" />
        </div>
        
        <div class="signin-link">
          Don't have an account? <span class="signin-text" @click="navigateToSignIn">Sign up</span>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { useRouter } from 'vue-router'
import HeaderSpacer from '@/components/header/HeaderSpacer.vue'
import ButtonPrimary from '@/components/buttons/ButtonPrimary.vue'
import InputField from '@/components/InputField.vue'
import { useLoginForm } from '@/utils/formHelpers'

export default {
  name: 'Login',
  components: {
    HeaderSpacer,
    ButtonPrimary,
    InputField
  },
  setup() {
    const router = useRouter()
    
    const {
      email,
      password,
      isFormValid,
      validationChanged,
      validateEmailField,
      validatePasswordField
    } = useLoginForm()
    
    const navigateToSignIn = () => {
      router.push({ name: 'SignIn' })
    }
    
    const handleLogin = () => {
      // Here you would implement the actual login logic
      console.log('Login with', email.value, password.value)
      // For now we'll just navigate to home on success
      if (isFormValid.value) {
        router.push({ name: 'Home' })
      }
    }
    
    return {
      email,
      password,
      isFormValid,
      validationChanged,
      validateEmailField,
      validatePasswordField,
      navigateToSignIn,
      handleLogin
    }
  }
}
</script>

<style scoped>
.login-container {
  display: flex;
  justify-content: center;
  align-items: center;
  height: calc(100vh - var(--header-height));
  background-color: var(--primary-col);
}

.login-form {
  background-color: var(--secondary-col);
  border-radius: 16px;
  padding: 3rem 2.5rem;
  width: 100%;
  max-width: 450px;
  border: 1px solid var(--tetriary-light-col);
}

.title {
  font-size: 24px;
  font-weight: 300;
  color: var(--text-primary-col);
  margin-bottom: 2rem;
  text-align: center;
}

.form-fields {
  margin-bottom: 1.5rem;
}

.button-container {
  margin-top: 1.5rem;
  display: flex;
  justify-content: center;
}

.signin-link {
  margin-top: 1.5rem;
  font-size: 14px;
  font-weight: 200;
  color: var(--text-secondary-col);
  text-align: center;
}

.signin-text {
  color: var(--text-primary-col);
  text-decoration: underline;
  cursor: pointer;
  transition: all 0.3s ease;
}

.signin-text:hover {
  opacity: 0.7;
}
</style> 