<script lang="ts" setup>
import { GetGiteaRepository, type GiteaRepository } from '../api/giteaIntegrationApi';

const loading = ref(false);

const apiUtils = useApiUtils();
const toast = useToast();
const selected = ref<GiteaRepository>();

function search(q: string): Promise<GiteaRepository[]> {
  loading.value = true;
  return new Promise((resolve, reject) => {
    apiUtils.try(() => GetGiteaRepository({ searchText: q }),
      (response) => {
        loading.value = false;
        resolve(response.data!);
      },
      (errorDescription) => {
        loading.value = false;
        toast.add({
          title: 'Error',
          description: errorDescription,
          color: 'red'
        });
        reject();
      }
    );
  });
}

</script>

<template>
  <USelectMenu 
    :searchable="search"
    :loading="loading"
    searchable-placeholder="Search a repo.."
    v-model="selected"
    trailing
    :debounce="300"
  >
    <template #label>
      <span class="truncate" v-if="selected">
        {{ selected.repoOwner }}/{{ selected.repoName }}
      </span>
      <span v-else>
        Select repository
      </span>
    </template>
    <template #option="{ option: repository }">
      <span class="truncate">{{ repository.repoOwner }}/{{ repository.repoName }}</span>
    </template>
  </USelectMenu>
</template>
