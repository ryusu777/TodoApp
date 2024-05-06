<script lang="ts" setup>
import { object, string } from 'yup';
import { OnboardUser, SignIn } from '~/domain/auth/api/authApi';
import { useAuth } from '~/domain/auth/composables/useAuth';

definePageMeta({
  layout: false
});

const route = useRoute();

const username = ref(route.query.username as string);
const email = ref(route.query.email as string);
const passwordChangeToken = route.query.passwordToken;

const state = reactive({
  password: '',
  confirmPassword: ''
});

const authStore = useAuth();

const schema = object({
  password: string().required('Password is required'),
  confirmPassword: string()
    .required('Please confirm your password')
    .test({
      name: 'is-same',
      test(value, ctx) {
        if (value !== state.confirmPassword)
          return ctx.createError({ message: 'Password is different!'})
        return true;
      }
    }),
});

const toast = useToast();
const router = useRouter();

async function submit() {
  const response = await OnboardUser({
    username: username.value,
    password: state.password,
    email: email.value,
    changePasswordCode: passwordChangeToken as string
  });

  if (response?.errorDescription) {
    await router.push('/login');
    toast.add({
      title: 'Error',
      description: response.errorDescription,
      color: 'red'
    });
    return;
  }

  const loginResponse = await SignIn({
    username: username.value,
    password: state.password
  });

  if (loginResponse.errorDescription) {
    await router.push('/login');
    toast.add({
      title: 'Error',
      description: loginResponse.errorDescription,
      color: 'red'
    });
    return;
  }

  authStore.setTokens(loginResponse.data?.access_token!, loginResponse.data?.refresh_token!);
  await router.replace('/');
}
</script>

<template>
  <div class="w-full h-full flex justify-center items-center">
    <UCard
      :ui="{
        body: {
          padding: 'px-2 py-2 sm:p-2'
        }
      }"
    >
      <UForm 
        :schema="schema" 
        :state="state" 
        class="p-5 flex flex-col gap-5" 
        @submit="submit"
      >
        <div class="flex flex-col gap-y-2">
          <UFormGroup label="Username" name="username">
            <UInput 
              icon="heroicons:user"
              :model-value="username"
              placeholder="example-user..." 
              disabled
            />
          </UFormGroup>
          <UFormGroup label="Email" name="email">
            <UInput 
              icon="heroicons:envelope"
              :model-value="email" 
              placeholder="example@example.com..." 
              disabled
            />
          </UFormGroup>
          <UFormGroup label="Password" name="password">
            <UInput 
              icon="heroicons:key-16-solid"
              v-model="state.password" 
              placeholder="enter password..." 
              type="password" 
            />
          </UFormGroup>

          <UFormGroup label="Confirm Password" name="confirmPassword">
            <UInput 
              icon="heroicons:key-16-solid"
              v-model="state.confirmPassword" 
              placeholder="enter password confirmation..." 
              type="password" 
            />
          </UFormGroup>

          <UButton 
            label="Sign Up"
            class="justify-center gap-x-3 mt-3"
            type="submit"
          />
        </div>
      </UForm>
    </UCard>
  </div>
</template>
