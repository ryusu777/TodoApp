<script setup lang="ts">
import type { Subdomain, SubdomainKnowledge } from '../api/subdomainApi';
import SubdomainKnowledges from './SubdomainKnowledges.vue';

const props = defineProps<{
  subdomain: Subdomain;
  loading?: boolean;
}>();

</script>

<template>
  <div v-if="!loading" class="flex flex-col h-full">
    <h1>{{ subdomain.title }}</h1>
    <p>{{ subdomain.description }}</p>

    <SubdomainKnowledges 
      :subdomain-id="props.subdomain.id || ''" 
      :knowledges="props.subdomain.knowledges || []"
    />
  </div>
</template>
