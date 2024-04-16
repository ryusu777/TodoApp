import { GetSubdomain, GetSubdomains, type Subdomain } from "../api/subdomainApi";

export function useSubdomainTabs(projectId: string) {
  type IsFetched = Subdomain & { isFetched?: boolean };
  const subdomains = ref<IsFetched[]>([]);

  const tabs = computed(() => subdomains.value.map(e => {
    return {
      label: e.title,
    }
  }));

  const selectedTab = ref(0);

  const currentSubdomain = computed(() => subdomains.value[selectedTab.value]);

  const isFetchingSubdomainDetail = ref(false);

  async function fetch() {
    const { data } = await GetSubdomains(projectId);
    
    if (data.value?.data) 
      subdomains.value = data.value.data;
    else
      return data.value?.errorDescription || "Failed to fetch subdomain list";
    
    if (selectedTab.value >= subdomains.value.length)
      selectedTab.value = 0;
  }

  async function setTab(index: number) {
    selectedTab.value = index;

    if (!subdomains.value[index].isFetched)
      return await fetchCurrentSubdomain();
  }

  async function fetchCurrentSubdomain() {
    isFetchingSubdomainDetail.value = true;
    const { data } = await GetSubdomain(subdomains
      .value[selectedTab.value].id || "");
    isFetchingSubdomainDetail.value = false;

    if (data?.value?.data) {
      subdomains.value[selectedTab.value] = data.value.data;
      subdomains.value[selectedTab.value].isFetched = true;
    }
    else
      return data.value?.errorDescription || 'Failed to fetch Subdomain Detail';
  }

  return {
    tabs,
    selectedTab,
    fetch,
    setTab,
    fetchCurrentSubdomain,
    currentSubdomain,
    isFetchingSubdomainDetail
  }
}
