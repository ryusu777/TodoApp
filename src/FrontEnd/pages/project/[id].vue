<script setup lang="ts">
import { GetProjectById } from '~/domain/project/api/projectApi';
import Subdomains from '~/domain/subdomain/components/Subdomains.vue';

definePageMeta({
  name: 'Project'
})

const route = useRoute();
const router = useRouter();
const projectCode = route.params.id.toString();
const { data: response } = await GetProjectById(projectCode, true);
const projectDetail = response.value?.data;
const projectName = computed(() => response?.value?.data?.name);
const breadcrumb = useBreadcrumb();
breadcrumb.setConverter('Project Detail', projectName);

function toDetail() {
  router.push(`/project/${projectCode}/detail`);
}
</script>

<template>
  <div class="flex flex-row gap-4">
    <h1>{{ projectName }}</h1>
    <UButton 
      color="gray"
      icon="i-heroicons-cog-6-tooth"
      size="sm"
      variant="solid"
      @click="toDetail"
    />
  </div>
  <p>{{ projectDetail?.description }}</p>
  <div>
    <Subdomains :project-id="projectCode" />
  </div>
</template>
