<script setup lang="ts">
import { UpdateProjectHierarchyMembers, type Hierarchy } from '../api/projectApi';
import Member from './Member.vue';
import HierarchyMemberForm from './HierarchyMemberForm.vue';

// component definition
const emit = defineEmits(['refresh']);

const props = defineProps<{
  hierarchy: Hierarchy;
  projectId: string;
  pending: boolean;
}>();

// utils
const apiUtils = useApiUtils();
const toast = useToast();

// member functions
const editable = ref(false);
const memberUsernames = ref<string[]>([
  ...props.hierarchy.memberUsernames
]);

function revert() {
  memberUsernames.value = [...props.hierarchy.memberUsernames];
}

function add(username: string) {
  memberUsernames.value.push(username);
}

function edit() {
  editable.value = true;
}

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

// api functions
function refresh() {
  emit('refresh');
}

// member form modal functions
const showForm = ref(false);

function onSubmit(username: string) {
  add(username);
  showForm.value = false;
}

function onShowForm() {
  showForm.value = true;
}
</script>

<template>
  <div class="flex flex-col" v-bind="$attrs">
    <div class="flex gap-x-2 my-2">
      <p class="text-lg">{{ hierarchy.name }} Members</p>
      <UButton 
        v-if="!editable" 
        @click="edit" 
        color="gray" 
        icon="heroicons:pencil"
        size="xs"
        variant="ghost"
      />
      <UButton 
        v-if="editable" 
        @click="persist" 
        color="green" 
        size="xs"
        label="Save Changes"
      />
      <UButton 
        v-if="editable"
        @click="cancel"
        color="red"
        size="xs"
        label="Cancel"
      />
    </div>
    <div class="flex gap-x-2 items-center">
      <p class="text-gray-500" v-if="memberUsernames.length === 0">No members...</p>
      <Member
        v-for="member in memberUsernames" 
        :key="member" 
        :username="member" 
      />
      <UButton 
        v-if="editable" 
        @click="onShowForm"
        color="gray" 
        icon="heroicons:plus"
        size="md"
      />
    </div>
  </div>
  <UModal
    v-model="showForm"
  >
    <HierarchyMemberForm 
      :projectId="props.projectId" 
      @submit="onSubmit" 
      @close="showForm = false"
    />
  </UModal>
</template>
