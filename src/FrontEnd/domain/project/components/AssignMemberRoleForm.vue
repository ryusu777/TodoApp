<script setup lang="ts">
import { object, string } from 'yup';
import { UpdateProjectHierarchyMembers } from '../api/projectApi';
import { useProject } from '../composable/useProject';

const props = defineProps<{
  username: string;
}>();

const project = useProject();

const state = reactive({
  hierarchyId: undefined as string | undefined,
});

const schema = object({
  hierarchyId: string().required(),
});

const apiUtils = useApiUtils();
const toast = useToast();
const emit = defineEmits(['assigned']);

async function onSubmit() {
  const hierarchy = project.hierarchies?.find(h => h.id === state.hierarchyId);
  if (!hierarchy) {
    return;
  }

  hierarchy.memberUsernames.push(props.username);
  apiUtils.try(() => UpdateProjectHierarchyMembers(({
      projectId: project.project!.id,
      hierarchyId: hierarchy.id,
      memberUsernames: hierarchy.memberUsernames
    })),
    () => {
      toast.add({
        title: 'Success',
        description: 'Member role has been assigned',
      });
      emit('assigned');
    },
    (errorDescription) => {
      toast.add({
        title: 'Error',
        description: errorDescription,
        color: 'red'
      });
    }
  )
}
</script>

<template>
  <UForm :state="state" :schema="schema" @submit="onSubmit" class="w-full flex justify-end items-center space-between gap-x-2">
    <div class="flex items-center">
      <USelectMenu
        placeholder="Select role.."
        class="w-max"
        v-model="state.hierarchyId"
        :options="project.hierarchies"
        value-attribute="id"
        option-attribute="name"
      />
    </div>

    <div>
      <UButton 
        type="submit"
        label="Save"
        icon="heroicons:paper-airplane-16-solid"
      />
    </div>
  </UForm>
</template>
