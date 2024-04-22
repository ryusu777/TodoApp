import { GetAssignments, type Assignment, type GetAssignmentsResponse } from "../api/assignmentApi";

export function useAssignmentState(projectId: string) {
  const assignments = ref<Assignment[]>([]);
  
  async function fetch(initial: boolean) {
    let data: GetAssignmentsResponse | null = null;
    let errorDescription: string | null = null;

    if (initial) {
      const { data: response } = await useAsyncData(() => GetAssignments({ projectId }));

      if (response.value?.data)
        data = response.value.data;

      if (response.value?.errorDescription)
        errorDescription = response.value.errorDescription;
    } else {
      const { data: response, errorDescription: errorResponse } = await GetAssignments({ projectId });

      if (response)
        data = response;

      if (errorResponse)
        errorDescription = errorResponse;
    }
  }
}

