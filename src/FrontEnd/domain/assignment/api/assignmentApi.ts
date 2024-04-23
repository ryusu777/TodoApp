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
  return api.$get<GetAssignmentsResponse>(AssignmentApiRoute.Assignments(payload.projectId));
}

export type GetAssignmentRequest = {
  assignmentId: string;
}

export type GetAssignmentResponse = Assignment;

export function GetAssignment(payload: GetAssignmentRequest) {
  const api = useApi();
  return api.$get<GetAssignmentResponse>(AssignmentApiRoute.AssignmentDetail(payload.assignmentId));
}

export type AssigningRequest = {
  assignmentId: string;
  assigneeUsername: string;
}

export type AssigningResponse = { };

export function Assigning(payload: AssigningRequest) {
  const api = useApi();
  return api.$post(AssignmentApiRoute.Assigning(payload.assignmentId), payload);
}

export type ChangeAssignmentStatusRequest = {
  assignmentId: string;
  assignmentStatus: AssignmentStatusEnum;
};

export type ChangeAssignmentStatusResponse = { };

export function ChangeAssignmentStatus(payload: ChangeAssignmentStatusRequest) {
  const api = useApi();
  return api.$post(AssignmentApiRoute.AssignmentStatus(payload.assignmentId), payload);
}

export type CreateAssignmentRequest = {
  title: string;
  description: string;
  projectId: string;
  subdomainId?: string;
  phaseId?: string;
  deadline?: string;
  reviewer?: string;
};

export type CreateAssignmentResponse = { };

export function CreateAssignment(payload: CreateAssignmentRequest) {
  const api = useApi();
  return api.$post(AssignmentApiRoute.Assignments(payload.projectId), payload);
}

export type DeleteAssignmentRequest = {
  assignmentId: string;
};

export type DeleteAssignmentResponse = {};

export function DeleteAssignment(payload: DeleteAssignmentRequest) {
  const api = useApi();
  return api.$delete(AssignmentApiRoute.DeleteAssignment(payload.assignmentId));
}

export type RemoveAssigneeRequest = {
  assignmentId: string;
  assigneeUsername: string;
}

export type RemoveAssigneeResponse = {};

export function RemoveAssignee(payload: RemoveAssigneeRequest) {
  const api = useApi();
  return api.$post(AssignmentApiRoute.RemoveAssignee(payload.assignmentId), payload);
}

export type UpdateAssignmentRequest = {
  assignmentId: string;
  title: string;
  description: string;
  subdomainId?: string;
  phaseId?: string;
  deadline?: string;
  reviewer?: string;
};

export type UpdateAssignmentResponse = {};

export function UpdateAssignment(payload: UpdateAssignmentRequest) {
  const api = useApi();
  return api.$put(AssignmentApiRoute.AssignmentDetail(payload.assignmentId), payload);
}
