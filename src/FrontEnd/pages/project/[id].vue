<script setup lang="ts">
import { useProject } from '~/domain/project/composable/useProject';
import Subdomains from '~/domain/subdomain/components/Subdomains.vue';

definePageMeta({
  name: 'Project',
  keepalive: true,
  middleware: 'authorization'
})

const route = useRoute();
const router = useRouter();
const projectCode = route.params.id.toString();
const state = useProject();

await state.fetch(projectCode, true);

const breadcrumb = useBreadcrumb();
breadcrumb.setConverter('Project', computed(() => state.project?.name));

function toDetail() {
  router.push(`/project/${projectCode}/detail`);
}
</script>

<template>
  <div class="flex flex-row gap-4">
    <h1>{{ state.project?.name }}</h1>
    <UButton 
      color="gray"
      icon="i-heroicons-cog-6-tooth"
      size="sm"
      variant="solid"
      @click="toDetail"
    />
  </div>
  <p>{{ state.project?.description }}</p>
  <div class="h-full">
    <Subdomains :project-id="projectCode" />
  </div>
</template>
