<script lang="ts" setup>
import { useAuth } from '~/domain/auth/composables/useAuth';

const auth = useAuth();

const router = useRouter();

function logout() {
  auth.clearAuth();
  router.push('/login');
}
</script>

<template>
  <div class="py-4 px-8 flex flex-row align-center justify-between">
    <h1 class="text-xl font-bold tracking-wider">Todo App</h1>

    <div>
      <UButton 
        color="red"
        square
        icon="heroicons:arrow-right-start-on-rectangle-16-solid"
        @click="logout"
      />
    </div>
  </div>
</template>
