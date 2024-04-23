import { DeleteAssignment, GetAssignments, type Assignment, type GetAssignmentsResponse } from "../api/assignmentApi";

export function useAssignmentState(projectId: string) {
  const assignments = ref<Assignment[]>([]);
  const assignmentsComputed = computed(() => assignments.value);

  const apiUtil = useApiUtils();

  async function doDelete(assignmentId: string) {
    let errorResult: string | undefined;
    await apiUtil
      .try(
        () => DeleteAssignment({ assignmentId }),
        (response) => {},
        (error) => errorResult = error
      )

    await fetch(false);

    return errorResult;
  }
  
  async function fetch(server: boolean) {
    let data: GetAssignmentsResponse | null = null;
    let errorDescription: string | null = null;

    if (server) {
      const { data: response } = await useAsyncData(() => GetAssignments({ projectId }));

      if (response.value?.data)
        data = response.value.data;

      if (response.value?.errorDescription)
        errorDescription = response.value.errorDescription;
    } else {
      await apiUtil
        .try(() => GetAssignments({ projectId }),
          (response: IApiResponse<GetAssignmentsResponse>) => {
            data = response.data!;
          }, 
          (error: string) => {
            errorDescription = error;
          }
        );
    }

    if (data)
      assignments.value = data;

    if (errorDescription)
      return errorDescription;
  }

  return {
    fetch,
    assignments: assignmentsComputed,
    delete: doDelete
  }
}

