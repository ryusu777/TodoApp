<script setup lang="ts">
import { createReusableTemplate } from '@vueuse/core';
import { UpdateProjectHierarchyMembers, type Hierarchy } from '../api/projectApi';

const props = defineProps<{
  hierarchy: Hierarchy;
  projectId: string;
  pending: boolean;
}>();

const editable = ref(false);

const [DefineTemplate, ReuseTemplate] = createReusableTemplate();

const memberUsernames = ref<string[]>([
  ...props.hierarchy.memberUsernames
]);

defineExpose({
  ReuseTemplate: ReuseTemplate
});

function revert() {
  memberUsernames.value = [...props.hierarchy.memberUsernames];
}

function edit() {
  editable.value = true;
}

const emit = defineEmits(['refresh']);

function refresh() {
  emit('refresh');
}

const apiUtils = useApiUtils();

const toast = useToast();

async function persist() {
  await apiUtils.try(() => UpdateProjectHierarchyMembers({
      projectId: props.projectId,
      hierarchyId: props.hierarchy.id,
      memberUsernames: memberUsernames.value
    }), () => {
      toast.add({ title: 'Success', description: 'Successfully updated hierarchy members' });
      editable.value = false;
      refresh();
    }, (errorDescription) => {
      toast.add({ title: 'Error', description: errorDescription, color: 'red' });
    }
  )
}

function cancel() {
  editable.value = false;
  revert();
}
</script>

<template>
  <DefineTemplate>
    <div v-if="!editable">
      <UButton 
        icon="heroicons:pencil"
        size="xs"
        color="white"
        variant="ghost"
        @click="edit"
      />
    </div>
    <UButton 
      size="xs"
      color="red"
      label="Cancel"
      @click="cancel"
      v-if="editable === true"
      :ui="{
        font: 'font-bold'
      }"
    />
    <UButton 
      size="xs"
      label="Save Changes"
      @click="persist"
      v-if="editable === true"
      :ui="{
        font: 'font-bold'
      }"
    />
  </DefineTemplate>
  <div>
    <MemberVue 
      v-for="member in hierarchy.memberUsernames" 
      :key="member" 
      :username="member" 
    />
  </div>
</template>
