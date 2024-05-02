<script lang="ts" setup>
import { object, string } from 'yup';

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

async function submit() {

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
            class="justify-center gap-x-3 mt-3"
          />

          <UButton 
            label="Sign in with Gitea"
            variant="solid"
            color="gray"
            class="justify-center gap-x-3"
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
