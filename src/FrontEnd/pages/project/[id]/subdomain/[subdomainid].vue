<script setup lang="ts">
import { GetSubdomain } from '~/domain/subdomain/api/subdomainApi';
import SubdomainDetail from '~/domain/subdomain/components/SubdomainDetail.vue';

const route = useRoute();

const subdomainId = route.params.subdomainid.toString();
const { data, pending, refresh } = await useAsyncData(() => GetSubdomain(subdomainId));

const subdomain = computed(() => data.value?.data);
</script>

<template>
  <SubdomainDetail 
    :loading="pending" 
    :subdomain="subdomain" 
  />
</template>
