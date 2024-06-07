import type { Hierarchy } from "../api/projectApi";

export function useHierarchyState(initialState: Hierarchy[]) {
  const hierarchies = ref<Hierarchy[]>(initialState);
  const hierarchiesComputed = computed(() => hierarchies.value);
  const isFetching = ref(false);
  const $isFetching = computed(() => isFetching.value);
  const members = computed(() => hierarchies.value.flatMap(e => e.memberUsernames.map(e => e)));

  function addMember(hierarchyId: string, username: string) {
    const hierarchy = hierarchies.value.find(e => e.id === hierarchyId);
    if (!hierarchy) return;
    hierarchy.memberUsernames.push(username);
  }

  function removeMember(hierarchyId: string, username: string) {
    const hierarchy = hierarchies.value.find(e => e.id === hierarchyId);
    if (!hierarchy) return;
    hierarchy.memberUsernames = hierarchy.memberUsernames.filter(e => e !== username);
  }

  function persistMemberChange() {
  }

  return {
    hierarchies: hierarchiesComputed,
    fetch,
    isFetching: $isFetching,
    members
  }
}
