<script lang="ts" setup>
import { object, string } from 'yup';
import { SignIn, SignInWithGitea } from '~/domain/auth/api/authApi';
import { useAuth } from '~/domain/auth/composables/useAuth';

definePageMeta({
  layout: 'login'
});

const schema = object({
  username: string().required('Username is required'),
  password: string().required('Password is required'),
});

const state = reactive({
  username: '',
  password: ''
});

const isSigningIn = ref(false);
const isSigningInWithGitea = ref(false);

async function signInWithGitea() {
  isSigningInWithGitea.value = true;
  const response = await SignInWithGitea();
  isSigningInWithGitea.value = false;

  if (response.data)
    window.location.replace(response.data);
}

const toast = useToast();
const authStore = useAuth();
const router = useRouter();
const apiUtils = useApiUtils();

async function submit() {
  isSigningIn.value = true;
  apiUtils.try(() => SignIn({
      username: state.username,
      password: state.password
    }),
    async (loginResponse) => {
      isSigningIn.value = false;
      authStore.setTokens(loginResponse.data?.access_token!, loginResponse.data?.refresh_token!);
      await router.replace('/')
    },
    (errorDescription) => {
      isSigningIn.value = false;
      toast.add({
        title: 'Error',
        description: errorDescription,
        color: 'red'
      });
    }
  )
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
              v-model="state.username" 
              placeholder="obvious-warden..." 
            />
          </UFormGroup>
          <UFormGroup label="Password" name="password">
            <UInput 
              icon="heroicons:key-16-solid"
              v-model="state.password" 
              placeholder="****.." 
              type="password" 
            />
          </UFormGroup>

          <UButton 
            label="Login"
            type="submit"
            class="justify-center gap-x-3 mt-3"
            :loading="isSigningIn"
          />

          <UButton 
            label="Sign in with Gitea"
            variant="solid"
            color="gray"
            class="justify-center gap-x-3"
            @click="signInWithGitea"
            :loading="isSigningInWithGitea"
          >
            <template #leading>
              <img src="/public/gitea-logo.svg" class="h-[24px]" />
            </template>
          </UButton>
        </div>
      </UForm>
    </UCard>
  </div>
</template>
