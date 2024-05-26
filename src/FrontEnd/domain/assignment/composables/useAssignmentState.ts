import { ChangeAssignmentStatus, DeleteAssignment, GetAssignments, type Assignment, type AssignmentStatusEnum, type GetAssignmentsResponse } from "../api/assignmentApi";
import { GetAssignmentsIssueNumber, type GetAssignmentsIssueNumberResponse } from "../api/giteaIssueApi";

export type NumberedAssignment = Assignment & {
  issueNumber?: number;
  issueUrl?: string;
};

export function useAssignmentState(projectId: string, subdomainId: string) {
  const assignments = ref<NumberedAssignment[]>([]);
  const assignmentsComputed = computed(() => assignments.value);
  const isFetchingIssueNumber = ref(false);
  const isFetchingIssueNumberComputed = computed(() => isFetchingIssueNumber.value);
  const isFetching = ref(false);
  const isFetchingComputed = computed(() => isFetching.value);

  const apiUtil = useApiUtils();

  async function doDelete(assignmentId: string) {
    let errorResult: string | undefined;
    await apiUtil
      .try(
        () => DeleteAssignment({ assignmentId }),
        (response) => {},
        (error) => errorResult = error
      )

    return errorResult;
  }
  
  async function fetch(server: boolean) {
    let data: GetAssignmentsResponse | null = null;
    let errorDescription: string | null = null;

    if (server) {
      const { data: response } = await useAsyncData(() => GetAssignments({ projectId, subdomainId }));

      if (response.value?.data)
        data = response.value.data;

      if (response.value?.errorDescription)
        errorDescription = response.value.errorDescription;
    } else {
      await apiUtil
        .try(() => GetAssignments({ projectId, subdomainId }),
          (response: IApiResponse<GetAssignmentsResponse>) => {
            data = response.data!;
          }, 
          (error: string) => {
            errorDescription = error;
          });
    }

    if (data)
      assignments.value = data;

    if (errorDescription)
      return errorDescription;

    await fetchIssueNumber();
  }

  async function fetchIssueNumber() {
    return new Promise<void>((resolve, reject) => {
      apiUtil
        .try(() => GetAssignmentsIssueNumber({ 
            assignmentIds: assignments.value.map(e => e.id!)
          }),
          (response) => {
            const assignmentNumbers = response.data!;
            assignmentNumbers.forEach((e) => {
              const foundAssignment = assignments.value.find(a => a.id === e.assignmentId);
              if (foundAssignment) {
                foundAssignment.issueNumber = e.issueNumber;
                foundAssignment.issueUrl = e.issueUrl;
              }
            });
            resolve();
          }, 
          (error: string) => {
            reject(error);
          });
    });
  }

  async function setAssignmentStatus(assignmentId: string, assignmentStatus: AssignmentStatusEnum) {
    let errorResult: string | undefined;
    await apiUtil
      .try(
        () => ChangeAssignmentStatus({ assignmentId, assignmentStatus }),
        (response) => {},
        (error) => errorResult = error
      )

    await fetch(false);

    return errorResult;
  }

  return {
    fetch,
    assignments: assignmentsComputed,
    delete: doDelete,
    setAssignmentStatus,
    isFetchingIssueNumber: isFetchingIssueNumberComputed,
    isFetching: isFetchingComputed
  }
}

