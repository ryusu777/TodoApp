import { GetSubdomain, GetSubdomains, type GetSubdomainsResponse, type Subdomain } from "../api/subdomainApi";

export function useSubdomainTabs(projectId: string, currentSubdomainId: string) {
  type IsFetched = Subdomain & { isFetched?: boolean };
  const subdomains = ref<IsFetched[]>([]);
  const disable = ref(false);

  const tabs = computed(() => subdomains.value.map(e => {
    return {
      label: e.title,
      to: `/project/${projectId}/${e.id}`,
      disabled: disable.value,
      subdomain: {
        id: e.id,
        description: e.description,
        title: e.title
      }
    };
  }));

  const selectedTab = ref(0);

  const currentSubdomain = computed(() => subdomains.value[selectedTab.value]);

  const isFetchingSubdomainDetail = ref(false);

  async function fetch(initial?: boolean) {
    let data: GetSubdomainsResponse | null = null;
    let errorDescription: string | null = null;

    if (initial) {
      const { data: response } = await useAsyncData(() => GetSubdomains(projectId));

      if (response?.value?.data)
        data = response.value.data;

      if (response.value?.errorDescription)
        errorDescription = response.value.errorDescription;
    } else {
      const { data: response, errorDescription: errorResponse } = await GetSubdomains(projectId);

      if (response)
        data = response;

      if (errorResponse)
        errorDescription = errorResponse;
    }

    
    if (data) 
      subdomains.value = data;
    else
      return errorDescription || "Failed to fetch subdomain list";

    if (currentSubdomainId) {
      selectedTab.value = subdomains.value.findIndex(e => e.id === currentSubdomainId);
    }
    
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
    const { data, errorDescription } = await GetSubdomain(subdomains
      .value[selectedTab.value].id || "");
    isFetchingSubdomainDetail.value = false;

    if (data) {
      subdomains.value[selectedTab.value] = data;
      subdomains.value[selectedTab.value].isFetched = true;
    }
    else
      return errorDescription || 'Failed to fetch Subdomain Detail';
  }

  function disableTabs() {
    disable.value = true;
  }

  function enableTabs() {
    disable.value = false;
  }

  return {
    tabs,
    selectedTab,
    fetch,
    setTab,
    fetchCurrentSubdomain,
    currentSubdomain,
    isFetchingSubdomainDetail,
    disableTabs,
    enableTabs
  }
}
