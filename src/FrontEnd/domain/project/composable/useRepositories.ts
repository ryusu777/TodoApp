import { object, string } from "yup";
import { AttachRepository, GetProjectRepository, type GiteaRepository } from "~/domain/gitea-integration/api/giteaIntegrationApi";

export function useRepositories(projectId: string) {
  const apiUtils = useApiUtils();
  const show = ref(false);
  const isSubmitting = ref(false);
  const isRefreshing = ref(false);

  const repositories = ref<GiteaRepository[]>([]);

  function initialize(): Promise<void> {
    return new Promise((resolve, reject) => {
      apiUtils.try(() => GetProjectRepository(projectId),
        (response) => {
          repositories.value = [...response.data!];
          resolve();
        }, 
        (errorDescription) => {
          reject(errorDescription);
        });
    })
  }

  function refresh(): Promise<void> {
    return new Promise(async (resolve, reject) => {
      isRefreshing.value = true;
      await apiUtils.try(() => GetProjectRepository(projectId),
        (response) => {
          isRefreshing.value = false;
          repositories.value = [...response.data!];
          resolve();
        }, 
        (errorDescription) => {
          isRefreshing.value = false;
          reject(errorDescription);
        });
    })
  }
  
  function closeForm() {
    show.value = false;
  }

  function showForm() {
    show.value = true;
  }

  async function attachRepository(repoOwner: string, repoName: string) {
    isSubmitting.value = true;
  }

  return {
    show,
    showForm,
    closeForm,
    repositories,
    refresh,
    attachRepository,
    isSubmitting,
    isRefreshing,
    initialize,
    projectId
  }
}