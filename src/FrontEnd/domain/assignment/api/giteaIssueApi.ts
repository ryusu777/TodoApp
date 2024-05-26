import { useAuth } from "~/domain/auth/composables/useAuth";

export const GiteaIssueApi = {
  AssignmentIssueNumber: `/integration/issue-number`,
};

type GetAssignmentsIssueNumberRequest = {
  assignmentIds: string[];
};

export type GetAssignmentsIssueNumberResponse  = {
  assignmentId: string;
  issueNumber: number;
  issueUrl: string;
};

export function GetAssignmentsIssueNumber(payload: GetAssignmentsIssueNumberRequest) {
  const api = useApi();
  const auth = useAuth();
  return api
    .$post<GetAssignmentsIssueNumberResponse[]>(GiteaIssueApi.AssignmentIssueNumber, 
      payload,
      {
        'Authorization': 'Bearer ' + auth.jwtToken
      });
}

