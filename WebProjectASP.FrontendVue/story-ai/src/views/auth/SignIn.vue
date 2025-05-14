<template>
  <div>
    <HeaderSpacer />
    <div class="sign-in-container">
      <div class="sign-in-form">
        <h1 class="title">Create Account</h1>
        
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
            label="Name" 
            type="text" 
            placeholder="Enter your name" 
            v-model="name"
            :validate-function="validateNameField"
            @validation-changed="validationChanged('name', $event)"
          />
          
          <InputField 
            label="Password" 
            type="password" 
            placeholder="Enter your password" 
            v-model="password"
            :validate-function="validatePasswordField"
            @validation-changed="validationChanged('password', $event)"
          />
          
          <InputField 
            label="Repeat Password" 
            type="password" 
            placeholder="Enter your password again" 
            v-model="repeatPassword"
            :validate-function="validateRepeatPasswordField"
            :extra-validate-params="{ passwordToMatch: password }"
            @validation-changed="validationChanged('repeatPassword', $event)"
          />
        </div>
        
        <div class="button-container">
          <ButtonPrimary text="Sign Up" :disabled="!isFormValid" @click="handleSignUp" />
        </div>
        
        <div class="login-link">
          Already have an account? <span class="login-text" @click="navigateToLogin">Login</span>
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
import { useSignInForm } from '@/utils/formHelpers'

export default {
  name: 'SignIn',
  components: {
    HeaderSpacer,
    ButtonPrimary,
    InputField
  },
  setup() {
    const router = useRouter()
    
    const { 
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
    } = useSignInForm()
    
    const navigateToLogin = () => {
      router.push({ name: 'Login' })
    }
    
    const handleSignUp = () => {
      // Here you would implement the actual sign-up logic
      console.log('Sign up with', email.value, name.value, password.value)
      // For now we'll just navigate to home on success
      if (isFormValid.value) {
        router.push({ name: 'Home' })
      }
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
      validateRepeatPasswordField,
      navigateToLogin,
      handleSignUp
    }
  }
}
</script>

<style scoped>
.sign-in-container {
  display: flex;
  justify-content: center;
  align-items: center;
  height: calc(100vh - var(--header-height));
  background-color: var(--primary-col);
}

.sign-in-form {
  background-color: var(--secondary-col);
  border-radius: 16px;
  padding: 3rem 2.5rem;
  width: 100%;
  max-width: 450px;
  box-shadow: 0 4px 30px rgba(0, 0, 0, 0.1);
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

.login-link {
  margin-top: 1.5rem;
  font-size: 14px;
  font-weight: 200;
  color: var(--text-secondary-col);
  text-align: center;
}

.login-text {
  color: var(--text-primary-col);
  text-decoration: underline;
  cursor: pointer;
  transition: all 0.3s ease;
}

.login-text:hover {
  opacity: 0.7;
}
</style> 