<script setup lang="ts">
import type { Member } from '../api/projectApi';
import { useMemberForm } from '../composable/useMemberForm';
import MemberVue from './Member.vue';
import MemberForm from './MemberForm.vue';

const props = defineProps<{
  members: Member[];
  projectId: string;
  pending: boolean;
  refresh: () => Promise<void>;
}>();

const form = useMemberForm(props.members, props.projectId);

async function onRefresh() {
  await props.refresh();

  form.refresh(props.members);
}

function update(phase: Member) {
  form.setModel(phase);
  form.showForm();
}

function add() {
  form.setModel({} as Member);
  form.showForm();
}

const editable = ref(false);

function edit() {
  editable.value = true;
}

const toast = useToast();

async function persist() {
  const result = await form.persist();

  if (result?.errorDescription) {
    toast.add({ title: 'Error', description: result?.errorDescription });
    return;
  }

  editable.value = false;
  toast.add({ title: 'Success', description: 'Successfully updated phases' });
}

function cancel() {
  form.revert();
  editable.value = false;
}

function remove(member: Member) {
  form.remove(member);
}
</script>

<template>
  <div class="py-5">
    <div class="flex flex-row gap-3"> 
      <span class="text-lg font-bold">Project Members</span>
      <div v-if="!editable">
        <UButton 
          icon="heroicons:pencil"
          size="xs"
          color="white"
          variant="ghost"
          @click="edit"
        />
        <UButton 
          icon="heroicons:arrow-path-rounded-square"
          size="xs"
          color="white"
          variant="ghost"
          @click="onRefresh"
          :loading="pending"
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
    </div>
    <div class="flex flex-row flex-wrap gap-3 mt-3">
      <div v-for="member in form.members.value" style="min-width: max-content; max-height: max-content;">
        <MemberVue
          :member="member" 
          :editable="editable"
          class="h-full" 
          @update="update(member)" 
          @delete="remove(member)"
        />
      </div>
      <div class="flex items-center">
        <UButton 
          size="xl"
          icon="heroicons:plus"
          @click="add"
          v-if="editable === true"
          color="gray"
          :ui="{
            rounded: 'rounded-xl'
          }"
        />
      </div>
    </div>
  </div>

  <UModal 
    :model-value="form.show.value" 
    @update:model-value="form.closeForm()"
    prevent-close
  >
    <MemberForm :form="form" />
  </UModal>
</template>
