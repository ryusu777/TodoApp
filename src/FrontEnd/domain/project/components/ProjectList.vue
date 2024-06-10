<script lang="ts" setup>
import { GetProjectDashboard, type ProjectItem } from '../api/projectApi';
import ProjectItemVue from './ProjectItem.vue';

const { data, refresh } = await useAsyncData(() => GetProjectDashboard());
</script>

<template>
  <div class="flex flex-wrap">
    <ProjectItemVue
      v-for="project in data?.data?.projects"
      :key="project.code"
      :project="project"
    />
  </div>
</template>
