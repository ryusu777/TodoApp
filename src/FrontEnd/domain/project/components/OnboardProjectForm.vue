<script setup lang="ts">
import { object, string } from 'yup';
import { CreateProject } from '../api/projectApi';

const router = useRouter();

const stageOne = {
  state: reactive({
    code: '',
    name: '',
    description: ''
  }),
  schema: object().shape({
    code: string().required('Please insert a code for your project').max(25, 'Maximum 25 characters'),
    name: string().required('Please insert a name for your project').max(30, 'Maximum 30 characters'),
    description: string().required('Please insert a description for your project').max(300, 'Maximum 300 characters')
  })
};

const apiUtils = useApiUtils();
const toast = useToast();
const isLoading = ref(false);
function submitStageOne() { 
  isLoading.value = true;
  apiUtils.try(() => CreateProject({
      code: stageOne.state.code,
      name: stageOne.state.name,
      description: stageOne.state.description,
      projectPhases: [],
      projectHierarchies: []
    }),
    () => {
      isLoading.value = false;
      router.push(`/project/${stageOne.state.code}/detail`)
    },
    (errorDescription) => {
      isLoading.value = false;
      toast.add({
        title: 'Error',
        description: errorDescription,
        color: 'red'
      })
    });
}
</script>

<template>
  <UCard>
    <h1 class="mb-3">Creating a new project</h1>
    <div>
      <UForm 
        :state="stageOne.state" 
        :schema="stageOne.schema" 
        @submit="submitStageOne"
        class="flex flex-col gap-y-3"
      >
        <UFormGroup name="code" label="Project Code">
          <UInput 
            v-model="stageOne.state.code" 
            placeholder="give your project a code.." 
          />
        </UFormGroup>
        <UFormGroup name="name" label="Project Name">
          <UInput 
            v-model="stageOne.state.name" 
            placeholder="give your project a name.." 
          />
        </UFormGroup>
        <UFormGroup name="description" label="Description">
          <UInput 
            v-model="stageOne.state.description" 
            placeholder="give your project a description.." 
          />
        </UFormGroup>
        <div class="flex justify-end">
          <UButton
            icon="heroicons:paper-airplane-16-solid" 
            size="lg"
            type="submit"
            label="Next"
            :loading="isLoading"
          />
        </div>
      </UForm>
    </div>
  </UCard>
</template>
