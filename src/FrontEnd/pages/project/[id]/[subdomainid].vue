<script setup lang="ts">
import { GetSubdomain } from '~/domain/subdomain/api/subdomainApi';
import SubdomainKnowledges from '~/domain/subdomain/components/SubdomainKnowledges.vue';

definePageMeta({
  disableBreadcrumb: true
});

const route = useRoute();

const subdomainId = route.params.subdomainid.toString();
const { data, pending, refresh } = await useAsyncData(subdomainId, () => GetSubdomain(subdomainId));

const subdomain = computed(() => data.value?.data);
</script>

<template>
  <div class="flex flex-col h-full">
    <SubdomainKnowledges
      :knowledges="subdomain?.knowledges || []"
      :subdomainId="subdomainId"
      @refresh="refresh"
    />
  </div>
</template>
