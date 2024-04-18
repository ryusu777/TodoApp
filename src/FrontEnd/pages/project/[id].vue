<script setup lang="ts">
import { GetProjectById } from '~/domain/project/api/projectApi';
import Subdomains from '~/domain/subdomain/components/Subdomains.vue';

definePageMeta({
  name: 'Project',
  keepalive: true
})

const route = useRoute();
const router = useRouter();
const projectCode = route.params.id.toString();
const { data: response } = await useAsyncData(() => GetProjectById(projectCode));
const projectDetail = computed(() => response?.value?.data)

const breadcrumb = useBreadcrumb();
breadcrumb.setConverter('Project', computed(() => projectDetail.value?.name));

function toDetail() {
  router.push(`/project/${projectCode}/detail`);
}
</script>

<template>
  <div class="flex flex-row gap-4">
    <h1>{{ projectDetail?.name }}</h1>
    <UButton 
      color="gray"
      icon="i-heroicons-cog-6-tooth"
      size="sm"
      variant="solid"
      @click="toDetail"
    />
  </div>
  <p>{{ projectDetail?.description }}</p>
  <div class="h-full">
    <Subdomains :project-id="projectCode" />
  </div>
</template>
