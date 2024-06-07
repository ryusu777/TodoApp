import { useApiUtils } from "~/composables/useApiUtils";
import { GetProjectById, type Project } from "../api/projectApi";

export const useProject = defineStore('project', () => {
  const project = ref<Project>();
  const projectComputed = computed(() => project.value);
  const apiUtil = useApiUtils();
  const isFetching = ref(false);
  const $isFetching = computed(() => isFetching.value);
  const phases = computed(() => project.value?.projectPhases);
  const members = computed(() => project.value?.projectHierarchies.flatMap(e => e.memberUsernames.map(e => e)));

  async function fetch(projectId: string, server: boolean) {
    let data: Project | undefined = undefined;
    let errorDescription: String | undefined = undefined;

    isFetching.value = true;
    if (server) {
      const { data: response } = await useAsyncData(() => GetProjectById(projectId));

      if (response?.value?.data)
        data = response.value.data;

      if (response.value?.errorDescription)
        errorDescription = response.value.errorDescription;
    } else {
      await apiUtil
        .try(() => GetProjectById(projectId),
          (response: IApiResponse<Project>) => {
            data = response.data;
          }, 
          (error: string) => {
            errorDescription = error;
          }
        );
    }
    isFetching.value = false;
    
    if (errorDescription)
      return errorDescription;

    project.value = data;
  }

  return {
    project: projectComputed,
    fetch,
    isFetching: $isFetching,
    phases,
    members
  }
});
