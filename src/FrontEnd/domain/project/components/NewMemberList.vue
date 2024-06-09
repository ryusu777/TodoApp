<script setup lang="ts">
import AssignMemberRoleForm from './AssignMemberRoleForm.vue';

defineProps<{
  unassignedMembers: string[];
}>();

const emit = defineEmits(['close', 'refresh']);

function close() {
  emit('close');
}
</script>

<template>
  <div class="p-5">
    <div class="flex justify-between">
      <p v-if="unassignedMembers.length > 0" class="text-md">
        Here's the list of new assignees that comes from Gitea, please give them role..
      </p>
      <p v-else class="text-md">No new assignees, you can close this dialog..</p>

      <div>
        <UButton
          size="xl"
          icon="heroicons:x-mark-16-solid"
          color="red"
          variant="ghost"
          @click="close"
        />
      </div>
    </div>
    <div class="flex flex-col gap-y-1 mt-2">
      <UCard
        v-for="member in unassignedMembers" 
        :key="member"
        :ui="{
          body: {
            base: 'flex justify-between items-center',
            padding: 'px-1 py-2 sm:p-2'
          }
        }"
      >
        <div>
          <p>{{ member }}</p>
        </div>
        <div class="flex-1">
          <AssignMemberRoleForm 
            :username="member" 
            @assigned="emit('refresh')"
          />
        </div>
      </UCard>
    </div>
  </div>
</template>
