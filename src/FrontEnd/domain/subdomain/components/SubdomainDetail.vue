<script setup lang="ts">
import type { Subdomain } from '../api/subdomainApi';

const props = defineProps<{
  subdomain: Subdomain;
  loading?: boolean;
}>();
</script>

<template>
  <div v-if="!loading">
    <h1>{{ subdomain.title }}</h1>
    <p>{{ subdomain.description }}</p>
  </div>
  <div class="space-y-2" v-else>
    <USkeleton class="h-8 w-[100px]" />
    <USkeleton class="h-4 w-[150px]" />
  </div>
</template>
