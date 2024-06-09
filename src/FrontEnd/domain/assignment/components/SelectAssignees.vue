<script setup lang="ts">
import { type Hierarchy, GetAssignableHierarchies } from '~/domain/project/api/projectApi';

// component definitions
const props = defineProps<{
  projectId: string;
  modelValue: string[];
}>();

const emit = defineEmits(['update:modelValue']);

// utils
const apiUtils = useApiUtils();
const toast = useToast();

// component form functions
function update(value: string) {
  emit('update:modelValue', value);
}

function select(username: string) {
  if (props.modelValue.includes(username)) {
    emit('update:modelValue', props.modelValue.filter(e => e !== username));
    return;
  }

  emit('update:modelValue', [...props.modelValue, username]);
}


// component form data logic
const options = ref<Hierarchy[]>([]);
const loading = ref(false);
function fetch() {
  loading.value = true;
  apiUtils.try(() => GetAssignableHierarchies(props.projectId),
    (response) => {
      if (response.data)
        options.value = response.data?.hierarchies ?? [];
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

const label = computed(() => props.modelValue.join(","));
</script>

<template>
  <UPopover class="w-full">
    <UButton 
      :label="label || 'Select assignees'"
      trailing-icon="heroicons:chevron-down-16-solid"
      class="w-full justify-between truncate"
      color="white"
      :ui="{
        icon: {
          base: 'text-gray-500'
        }
      }"
    />

    <template #panel>
      <div class="p-2 bg-gray-800 w-full">
        <div v-for="hierarchy in options" :key="hierarchy.id" class="flex flex-col gap-y-1">
          <span class="truncate block text-sm cursor-default py-1.5">{{ hierarchy.name }}</span>

          <div class="flex flex-col gap-y-1">
            <span 
              class="block truncate text-sm px-2 hover:bg-gray-900 cursor-default rounded py-1.5 flex justify-between items-center"
              v-for="member in hierarchy.memberUsernames" 
              :key="member"
              @click="select(member)"
            >
              {{ member }}
              <UIcon
                v-if="props.modelValue.includes(member)"
                name="heroicons:check-16-solid"
                color="gray"
              />
            </span>
          </div>
        </div>
      </div>
    </template>
  </UPopover>
</template>
