<script lang="ts" setup>
import type { useRepositories } from '~/domain/project/composable/useRepositories';
import SelectRepository from '~/domain/gitea-integration/components/SelectRepository.vue';  
import { AttachRepository, type GiteaRepository } from '~/domain/gitea-integration/api/giteaIntegrationApi';
import { number, object, string } from 'yup';

const props = defineProps<{
  form: ReturnType<typeof useRepositories>
}>();

const state = reactive({
  repository: undefined as GiteaRepository | undefined
});

const schema = object({
  repository: object({
    id: number().required(),
    repoName: string().required(),
    repoOwner: string().required()
  }).test('unique-repository', 'Repository already attached / hooked', value => {
    if (props.form.repositories.value.findIndex(e => e.id == value.id) !== -1)
      return false;

    return true;
  })
});

const toast = useToast();
const apiUtils = useApiUtils();
const isSubmitting = ref(false);

async function onSubmit() {
  isSubmitting.value = true;
  await apiUtils.try(() => AttachRepository({
    projectId: props.form.projectId,
    repoOwner: state.repository?.repoOwner || '',
    repoName: state.repository?.repoName || ''
  }),
  () => {
    isSubmitting.value = false;
    toast.add({
      title: 'Success',
      description: 'Successfully attached and hooked repository'
    });
  },
  (errorDescription) => {
    isSubmitting.value = false;
    toast.add({
      title: 'Error',
      description: errorDescription,
      color: 'red'
    });
  });

  closeForm();

  await props.form.refresh();
}

function closeForm() {
  props.form.closeForm();
}
</script>

<template>
  <UForm 
    :schema="schema" 
    :state="state" 
    class="p-5 flex flex-col gap-5" 
    @submit="onSubmit">
    <UFormGroup label="Repository" name="repository">
      <SelectRepository v-model:selected="state.repository" />
    </UFormGroup>

    <div class="flex justify-end gap-3">
      <UButton 
        @click="closeForm" 
        label="Cancel" 
        icon="heroicons:x-circle-16-solid" 
        color = "red" 
      />
      <UButton 
        type="submit" 
        label="Save" 
        icon="heroicons:paper-airplane-16-solid" 
        :loading="isSubmitting"
      />
    </div>
  </UForm>
</template>
