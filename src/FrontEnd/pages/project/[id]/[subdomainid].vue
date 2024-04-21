<script setup lang="ts">
import { GetSubdomain } from '~/domain/subdomain/api/subdomainApi';
import SubdomainKnowledges from '~/domain/subdomain/components/SubdomainKnowledges.vue';
import Assignments from '~/domain/assignment/components/Assignments.vue';

definePageMeta({
  disableBreadcrumb: true
});

const route = useRoute();

const subdomainId = route.params.subdomainid.toString();
const { data, refresh } = await useAsyncData(subdomainId, () => GetSubdomain(subdomainId));

const subdomain = computed(() => data.value?.data);

const tabItems = [{
  label: 'Assignments'
}, {
  label: 'Knowledges'
}];

const selectedTab = ref(0);
</script>

<template>
  <div class="flex flex-col h-full">
    <div class="max-w-[300px]">
      <UTabs 
        :items="tabItems"
        v-model="selectedTab"
      />
    </div>
    <Assignments 
      v-if="selectedTab === 0"
    />
    <SubdomainKnowledges
      v-if="selectedTab === 1"
      :knowledges="subdomain?.knowledges || []"
      :subdomainId="subdomainId"
      @refresh="refresh"
    />
  </div>
</template>
