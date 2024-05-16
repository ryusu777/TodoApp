<script setup lang="ts">
import type { Assignment } from '../api/assignmentApi';
import type { useAssignmentForm } from '../composables/useAssignmentForm';
import type { useAssignmentState } from '../composables/useAssignmentState';

const props = defineProps<{
  assignment: Assignment;
  form: ReturnType<typeof useAssignmentForm>;
  state: ReturnType<typeof useAssignmentState>;
}>();

const toast = useToast();

async function doDelete(closeDelete: () => any) {
  const error = await props.state.delete(props.assignment.id || '');

  if (error) {
    toast.add({
      title: 'Error',
      description: error,
      color: 'red'
    });
  } else {
    toast.add({
      title: 'Success',
      description: 'Successfully deleted assignment'
    });
    props.form.closeForm();
    closeDelete();
    await props.state.fetch(false);
  }
}
</script>

<template>
  <UCard
    :ui="{
      body: {
        padding: 'px-3 py-3 sm:px-3 sm:py-3'
      },
      footer: {
        padding: 'px-3 py-3 sm:px-3 sm:py-3'
      }
    }"
  >
    <div class="flex flex-col gap-y-1">
      <div class="flex justify-between">
        <p class="text-lg">{{ assignment.title }}</p>
        <div class="flex gap-x-2">
          <UAvatarGroup size="sm" :max="2">
            <UAvatar 
              v-for="assignee in assignment.assignees"
              :alt="assignee" 
              size="sm" 
            />
          </UAvatarGroup>
          <UPopover>
            <UButton 
              icon="heroicons:ellipsis-vertical-16-solid"
              variant="ghost"
              color="white"
            />

            <template #panel="{ close: closeMenu }">
              <div class="flex gap-x-1">
                <div class="p-1 flex flex-col gap-y-1 w-[175px]">
                  <UButton 
                    label="Edit"
                    icon="heroicons:pencil"
                    size="xs"
                    color="gray"
                    @click="form.update(assignment)"
                  />
                  <UPopover :ui="{ width: 'w-full' }">
                    <UButton
                      class="w-full"
                      label="Delete"
                      icon="heroicons:trash"
                      color="gray"
                      size="xs"
                    />
                    <template #panel="{ close: closeDelete }">
                      <div class="flex flex-col p-3 gap-y-2 text-white">
                        <span>Are you sure want to delete this?</span>
                        <div class="flex justify-end gap-x-1">
                          <UButton 
                            icon="heroicons:x-mark-16-solid"
                            label="No"
                            square
                            size="2xs"
                            class="px-2"
                            @click="closeDelete"
                          />
                          <UButton 
                            icon="heroicons:trash"
                            label="Yes"
                            square
                            color="red"
                            size="2xs"
                            class="px-2"
                            @click="doDelete(closeMenu)"
                          />
                        </div>
                      </div>
                    </template>
                  </UPopover>
                </div>
              </div>
            </template>
          </UPopover>
        </div>
      </div>
      <p class="text-sm">{{ assignment.description }}</p>
    </div>
    <div>
      <span class="text-xs text-gray-200">Reviewer</span>
      <div class="flex gap-x-2 items-center">
        <UAvatar :alt="assignment.reviewer" size="sm" />
        <p class="text-sm">{{ assignment.reviewer }}</p>
      </div>
    </div>
  </UCard>
</template>
