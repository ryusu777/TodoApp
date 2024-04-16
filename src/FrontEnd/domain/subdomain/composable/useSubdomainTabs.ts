import { GetSubdomains, type Subdomain } from "../api/subdomainApi";

export function useSubdomainTabs(projectId: string) {
  const subdomains = ref<Subdomain[]>([]);

  async function fetch() {
    const { data } = await GetSubdomains(projectId);
    
    if (data.value?.data)
      subdomains.value = data.value.data;
    else
      return data.value?.errorDescription || "Failed to fetch subdomain list";
    
    if (selectedTab.value >= subdomains.value.length)
      selectedTab.value = 0;
  }

  const tabs = computed(() => subdomains.value.map(e => {
    return {
      label: e.title
    }
  }));

  const selectedTab = ref(0);

  function setTab(index: number) {
    selectedTab.value = index;
  }

  return {
    tabs,
    selectedTab,
    fetch,
    setTab
  }
}
