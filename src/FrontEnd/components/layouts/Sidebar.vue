<script setup lang="ts">
const links = [
  [{
    label: 'Profile',
    avatar: {
      src: 'https://avatars.githubusercontent.com/u/739984?v=4'
    },
    badgeIcon: 'material-symbols:settings-rounded'
  },{
    label: 'Project',
    badge: '1',
    to: '/project'
  }],
  [{
    label: 'Todo App',
    to: '/project/TodoApp'
  },{
    label: 'Project B',
    to: '/project/ProjectB'
  }]
];
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
