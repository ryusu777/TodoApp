<script setup lang="ts">
import type { AssignmentStatusEnum } from '../api/assignmentApi';
import type { useAssignmentForm } from '../composables/useAssignmentForm';
import type { NumberedAssignment, useAssignmentState } from '../composables/useAssignmentState';
import IssueNumber from './IssueNumber.vue';

const props = defineProps<{
  assignment: NumberedAssignment;
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

async function changeStatus(status: AssignmentStatusEnum) {
  if (!props.assignment.id)
    return;

  const error = await props.state.setAssignmentStatus(props.assignment.id, status);

  if (error) {
    toast.add({
      title: 'Error',
      description: error,
      color: 'red'
    });
  } else {
    toast.add({
      title: 'Success',
      description: 'Successfully changed assignment'
    });
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
      <div class="text-sm" v-if="assignment.issueNumber">
        <IssueNumber :assignment="assignment" />
      </div>
    </div>
    <div class="flex flex-between items-end w-full">
      <div v-if="assignment.reviewer" class="flex-1">
        <span class="text-xs text-gray-200">Reviewer</span>
        <div class="flex gap-x-2 items-center">
          <UAvatar :alt="assignment.reviewer" size="sm" />
          <p class="text-sm">{{ assignment.reviewer }}</p>
        </div>
      </div>

      <div class="space-x-2">
        <UTooltip text="Reopen" :popper="{ placement: 'top' }" v-if="assignment.status !== 'New'">
          <UButton 
            color="red"
            icon="heroicons:backward-solid"
            square
            size="2xs"
            @click="changeStatus('New')"
          />
        </UTooltip>
        <UTooltip text="Work on" :popper="{ placement: 'top' }" v-if="assignment.status !== 'OnProgress'">
          <UButton 
            color="blue"
            icon="heroicons:play-16-solid"
            square
            size="2xs"
            @click="changeStatus('OnProgress')"
          />
        </UTooltip>
        <UTooltip text="Request Review" :popper="{ placement: 'top' }" v-if="assignment.status !== 'WaitingReview'">
          <UButton 
            color="yellow"
            icon="heroicons:paper-airplane-16-solid"
            square
            size="2xs"
            @click="changeStatus('WaitingReview')"
          />
        </UTooltip>
        <UTooltip text="Complete" :popper="{ placement: 'top' }" v-if="assignment.status !== 'Completed'">
          <UButton 
            icon="heroicons:check-badge-16-solid"
            square
            size="2xs"
            @click="changeStatus('Completed')"
          />
        </UTooltip>
      </div>
    </div>
  </UCard>
</template>
