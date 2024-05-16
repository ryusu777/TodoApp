<script lang="ts" setup>
import { GetGiteaRepository, GetProjectRepository, type GiteaRepository } from '../api/giteaIntegrationApi';

const loading = ref(false);

const props = defineProps<{
  selected?: GiteaRepository,
  projectId: string
}>();

const emit = defineEmits(['update:selected']);

function update(val: GiteaRepository) {
  emit('update:selected', val);
}

const apiUtils = useApiUtils();
const toast = useToast();

function fetch(): Promise<GiteaRepository[]> {
  loading.value = true;
  return new Promise((resolve, reject) => {
    apiUtils.try(() => GetProjectRepository(props.projectId),
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

const options = ref<GiteaRepository[]>([]);

onMounted(async () => {
  options.value = await fetch();
});
</script>

<template>
  <USelectMenu 
    :loading="loading"
    :model-value="selected"
    @update:model-value="update"
    :options="options"
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
