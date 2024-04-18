<script setup lang="ts">
import type { Subdomain, SubdomainKnowledge } from '../api/subdomainApi';
import SubdomainKnowledges from './SubdomainKnowledges.vue';

const props = defineProps<{
  subdomain?: Subdomain;
  loading?: boolean;
}>();

</script>

<template>
  <div v-if="!loading && subdomain" class="flex flex-col h-full">
    <h1>{{ subdomain.title }}</h1>
    <p>{{ subdomain.description }}</p>

    <SubdomainKnowledges 
      :subdomain-id="subdomain.id || ''" 
      :knowledges="subdomain.knowledges || []"
    />
  </div>
</template>
