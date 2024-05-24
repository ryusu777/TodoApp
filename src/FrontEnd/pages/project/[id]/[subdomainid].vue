<script setup lang="ts">
import { GetSubdomain } from '~/domain/subdomain/api/subdomainApi';
import SubdomainKnowledges from '~/domain/subdomain/components/SubdomainKnowledges.vue';
import Assignments from '~/domain/assignment/components/Assignments.vue';
import type { ReuseTemplateComponent } from '@vueuse/core';

definePageMeta({
  disableBreadcrumb: true,
  middleware: 'authorization'
});

const route = useRoute();

const subdomainId = route.params.subdomainid.toString();
const projectId = route.params.id.toString();
const { data, refresh } = await useAsyncData(subdomainId, () => GetSubdomain(subdomainId));

const subdomain = computed(() => data.value?.data);

const tabItems = [{
  label: 'Assignments'
}, {
  label: 'Knowledges'
}];

const selectedTab = ref(0);

const assignmentRef = ref<InstanceType<typeof Assignments>>();
const knowledgeRef = ref<InstanceType<typeof SubdomainKnowledges>>();

const AssignmentDefinedTemplate = computed(() => 
  assignmentRef.value?.ReuseTemplate as ReuseTemplateComponent<Record<string, any>, Record<"default", undefined>>);
const KnowledgeDefinedTemplate = computed(() => 
  knowledgeRef.value?.ReuseTemplate as ReuseTemplateComponent<Record<string, any>, Record<"default", undefined>>);
</script>

<template>
  <div class="flex flex-col h-full">
    <div class="flex gap-x-3">
      <div class="max-w-[300px]">
        <UTabs
          :items="tabItems"
          v-model="selectedTab"
        />
      </div>
      <div
        v-if="selectedTab === 0 && AssignmentDefinedTemplate"
      >
        <AssignmentDefinedTemplate 
        />
      </div>
      <div
        v-if="selectedTab === 1 && KnowledgeDefinedTemplate"
      >
        <KnowledgeDefinedTemplate
        />
      </div>
    </div>
    <div 
      v-show="selectedTab === 0"
    >
      <Assignments
        :subdomain-id="subdomainId"
        :project-id="projectId"
        ref="assignmentRef"
      />
    </div>
    <div
      v-show="selectedTab === 1"
    >
      <SubdomainKnowledges
        :knowledges="subdomain?.knowledges || []"
        :subdomainId="subdomainId"
        @refresh="refresh"
        ref="knowledgeRef"
      />
    </div>
  </div>
</template>
