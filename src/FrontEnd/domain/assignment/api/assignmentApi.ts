import { useAuth } from "~/domain/auth/composables/useAuth";

export const AssignmentApiRoute = {
  Assignments: (projectId: string) => `/project/${projectId}/assignment`,
  DeleteAssignment: (assignmentId: string) => `/assignment/${assignmentId}`,
  AssignmentDetail: (assignmentId: string) => `/assignment/${assignmentId}`,
  Assigning: (assignmentId: string) => `/assignment/${assignmentId}/assign`,
  RemoveAssignee: (assignmentId: string) => `/assignment/${assignmentId}/remove-assignee`,
  AssignmentStatus: (assignmentId: string) => `/assignment/${assignmentId}/status`
};

export type AssignmentStatusEnum = 'New' | 'OnProgress' | 'WaitingReview' | 'Completed';

export interface Assignment {
  id?: string;
  title: string;
  description?: string;
  projectId: string;
  status: AssignmentStatusEnum;
  assignees: string[];
  phaseId?: string;
  subdomainId?: string;
  deadline?: string;
  reviewer?: string;
}

export type GetAssignmentsRequest = {
  projectId: string;
}

export type GetAssignmentsResponse = Assignment[];

export function GetAssignments(payload: GetAssignmentsRequest) {
  const api = useApi();
  const auth = useAuth();
  return api
    .$get<GetAssignmentsResponse>(AssignmentApiRoute.Assignments(payload.projectId), null,
    {
      'Authorization': 'Bearer ' + auth.jwtToken
    });
}

export type GetAssignmentRequest = {
  assignmentId: string;
}

export type GetAssignmentResponse = Assignment;

export function GetAssignment(payload: GetAssignmentRequest) {
  const api = useApi();
  const auth = useAuth();
  return api.$get<GetAssignmentResponse>(AssignmentApiRoute.AssignmentDetail(payload.assignmentId), null,
    {
      'Authorization': 'Bearer ' + auth.jwtToken
    });
}

export type AssigningRequest = {
  assignmentId: string;
  assigneeUsername: string;
}

export type AssigningResponse = { };

export function Assigning(payload: AssigningRequest) {
  const api = useApi();
  const auth = useAuth();
  return api.$post(AssignmentApiRoute.Assigning(payload.assignmentId), payload, {
    'Authorization': 'Bearer ' + auth.jwtToken
  });
}

export type ChangeAssignmentStatusRequest = {
  assignmentId: string;
  assignmentStatus: AssignmentStatusEnum;
};

export type ChangeAssignmentStatusResponse = { };

export function ChangeAssignmentStatus(payload: ChangeAssignmentStatusRequest) {
  const api = useApi();
  const auth = useAuth();
  return api.$post(AssignmentApiRoute.AssignmentStatus(payload.assignmentId), payload, {
    'Authorization': 'Bearer ' + auth.jwtToken
  });
}

export type CreateAssignmentRequest = {
  title: string;
  description: string;
  projectId: string;
  giteaRepositoryId: number;
  assignees: string[];
  subdomainId?: string;
  phaseId?: string;
  deadline?: string;
  reviewer?: string;
};

export type CreateAssignmentResponse = { };

export function CreateAssignment(payload: CreateAssignmentRequest) {
  const api = useApi();
  const auth = useAuth();
  return api.$post(AssignmentApiRoute.Assignments(payload.projectId), payload, {
    'Authorization': 'Bearer ' + auth.jwtToken
  });
}

export type DeleteAssignmentRequest = {
  assignmentId: string;
};

export type DeleteAssignmentResponse = {};

export function DeleteAssignment(payload: DeleteAssignmentRequest) {
  const api = useApi();
  const auth = useAuth();
  return api.$delete(AssignmentApiRoute.DeleteAssignment(payload.assignmentId), null, {
    'Authorization': 'Bearer ' + auth.jwtToken
  });
}

export type RemoveAssigneeRequest = {
  assignmentId: string;
  assigneeUsername: string;
}

export type RemoveAssigneeResponse = {};

export function RemoveAssignee(payload: RemoveAssigneeRequest) {
  const api = useApi();
  const auth = useAuth();
  return api.$post(AssignmentApiRoute.RemoveAssignee(payload.assignmentId), payload, {
    'Authorization': 'Bearer ' + auth.jwtToken
  });
}

export type UpdateAssignmentRequest = {
  assignmentId: string;
  title: string;
  description: string;
  assignees: string[];
  subdomainId?: string;
  phaseId?: string;
  deadline?: string;
  reviewer?: string;
};

export type UpdateAssignmentResponse = {};

export function UpdateAssignment(payload: UpdateAssignmentRequest) {
  const api = useApi();
  const auth = useAuth();
  return api.$put(AssignmentApiRoute.AssignmentDetail(payload.assignmentId), payload, {
    'Authorization': 'Bearer ' + auth.jwtToken
  });
}
