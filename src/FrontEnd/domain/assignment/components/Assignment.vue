<script setup lang="ts">
import { object, string } from 'yup';
import type { AssignmentStatusEnum } from '../api/assignmentApi';
import type { useAssignmentForm } from '../composables/useAssignmentForm';
import type { NumberedAssignment, useAssignmentState } from '../composables/useAssignmentState';
import IssueNumber from './IssueNumber.vue';

// component definitions
const props = defineProps<{
  assignment: NumberedAssignment;
  form: ReturnType<typeof useAssignmentForm>;
  state: ReturnType<typeof useAssignmentState>;
}>();

// utils
const apiUtils = useApiUtils();
const toast = useToast();

const isReopened = computed(() => props.assignment.status === 'New' && props.assignment.lastReview?.status === 'Approved');

// reject and requesting approval form state
const requestReviewForm = {
  state: reactive({
    reviewDescription: ''
  }),
  schema: object().shape({
    reviewDescription: string().required('Please enter description..')
  })
};

const rejectionForm = {
  state: reactive({
    rejectionNotes: ''
  }),
  schema: object().shape({
    rejectionNotes: string().required('Please enter rejection notes..')
  })
}

// api methods
async function doDelete(closeDelete: () => any) {
  const error = await props.state.delete(props.assignment.id || '');

  handleResult(error, 'Successfully deleted assignment');
}

async function workOn() {
  const result = await props.state.workOnAssignment(props.assignment.id || '');

  handleResult(result);
}

async function requestReview() {
  const result = await props.state.requestAssignmentReview(props.assignment.id || '', requestReviewForm.state.reviewDescription);

  handleResult(result);
}

async function approve() {
  const result = await props.state.approveAssignmentReview(props.assignment.id || '');

  handleResult(result);
}

async function reject() {
  const result = await props.state.rejectAssignmentReview(props.assignment.id || '', rejectionForm.state.rejectionNotes);

  handleResult(result);
}

async function reopen() {
  const result = await props.state.reopenAssignment(props.assignment.id || '');

  handleResult(result);
}

async function handleResult(error?: string, successMessage?: string) {
  if (error) {
    toast.add({
      title: 'Error',
      description: error,
      color: 'red'
    });
  } else {
    toast.add({
      title: 'Success',
      description: successMessage || 'Successfully modified assignment status'
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
        <p class="text-lg">{{ assignment.title }} <span class="text-sm text-red-400" v-if="isReopened">(Reopened)</span></p>
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
        <div>
          <p v-if="assignment.lastReview?.rejectionNotes" class="text-xs">
            Revision Notes: <br />{{ assignment.lastReview.rejectionNotes }}
          </p>
        </div>
      </div>

      <div class="space-x-2 flex">
        <UButton 
          color="red"
          icon="heroicons:backward-solid"
          label="Reopen"
          size="2xs"
          @click="reopen"
          v-if="assignment.status === 'Completed'"
        />
        <UButton 
          v-if="assignment.status === 'New' || assignment.status === 'Revised'"
          color="blue"
          icon="heroicons:play-16-solid"
          label="Start"
          size="2xs"
          @click="workOn"
        />
        <UPopover
            v-if="assignment.status === 'OnProgress'"
        >
          <UButton 
            color="yellow"
            icon="heroicons:paper-airplane-16-solid"
            label="Request Review"
            size="2xs"
          />

          <template #panel>
            <UForm 
              class="p-3 flex flex-col gap-y-2"
              :state="requestReviewForm.state"
              :schema="requestReviewForm.schema"
              @submit="requestReview"
            >
              <UFormGroup label="Description" name="reviewDescription">
                <UTextarea
                  v-model="requestReviewForm.state.reviewDescription"
                  placeholder="describe the work you've done.."
                />
              </UFormGroup>
              <UButton 
                block
                icon="heroicons:paper-airplane-16-solid"
                label="Send"
                size="2xs"
                type="submit"
              />
            </UForm>
          </template>
        </UPopover>
        <UPopover
            v-if="assignment.status === 'WaitingReview'"
        >
          <UButton 
            color="red"
            label="Reject"
            size="2xs"
          />

          <template #panel>
            <UForm 
              class="p-3 flex flex-col gap-y-2"
              :state="rejectionForm.state"
              :schema="rejectionForm.schema"
              @submit="reject"
            >
              <UFormGroup label="Rejection Notes" name="rejectionNotes">
                <UTextarea
                  v-model="rejectionForm.state.rejectionNotes"
                  placeholder="describe the revision needed.."
                />
              </UFormGroup>
              <UButton 
                block
                icon="heroicons:paper-airplane-16-solid"
                label="Send"
                size="2xs"
                type="submit"
              />
            </UForm>
          </template>
        </UPopover>
        <UButton 
          v-if="assignment.status === 'WaitingReview'"
          label="Complete"
          size="2xs"
          @click="approve"
        />
      </div>
    </div>
  </UCard>
</template>
