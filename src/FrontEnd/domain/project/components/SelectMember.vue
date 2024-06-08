<script setup lang="ts">
import { GetAllProjectMembers } from '../api/projectApi';


const props = defineProps<{
  projectId: string;
  modelValue: string;
}>();

const emit = defineEmits(['update:modelValue']);

function update(value: string) {
  emit('update:modelValue', value);
}

const loading = ref(false);
const options = ref<string[]>([]);

const apiUtils = useApiUtils();
const toast = useToast();

function fetch() {
  loading.value = true;
  apiUtils.try(() => GetAllProjectMembers({ projectId: props.projectId }),
    (response) => {
      if (response.data)
        options.value = response.data?.memberUsernames;
      loading.value = false;
    },
    (errorDescription) => {
      toast.add({
        title: 'Error',
        description: errorDescription,
        color: 'red'
      });
      loading.value = false;
    }
  );
}

onMounted(() => {
  fetch();
});
</script>

<template>
  <USelectMenu
    :model-value="modelValue"
    @update:model-value="update"
    :options="options"
    label="Select a member"
    :loading="loading"
  >
  </USelectMenu>
</template>
