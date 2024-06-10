<script setup lang="ts">
import { useAuth } from '~/domain/auth/composables/useAuth';
import { GetProjectPages } from '~/domain/project/api/projectApi';

const projectPages = await GetProjectPages();

const auth = useAuth();

const username = computed(() => auth.getUsername());

const pagesLink = computed(() => projectPages.data.value?.data?.map(e => {
    return {
      label: e.name,
      to: '/project/' + e.id
    }
  }));

const links = computed(() => [
  [{
    label: username.value,
    avatar: {
      src: 'https://avatars.githubusercontent.com/u/739984?v=4'
    },
    disabled: true
  },{
    label: 'Project',
    badge: '1',
    to: '/project'
  }],
  [...pagesLink.value || []]
]);
</script>

<template>
  <UVerticalNavigation 
    :links="links" class="p-5" 
    :ui="{
      padding: 'px-3 py-2',
      width: 'min-w-[200px]',
      label: 'truncate text-bold tracking-wide'
    }"
  >
    <template #badge="{ link }">
      <div class="flex flex-row justify-end grow">
        <UIcon :name="link.badgeIcon" dynamic v-if="link.badgeIcon" />
        <UBadge 
          color="gray"
          variant="solid"
          size="xs"
          v-else-if="link.badge">
          {{ link.badge }}
        </UBadge>
      </div>
    </template>
  </UVerticalNavigation>
</template>
