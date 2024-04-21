export const AssignmentApiRoute = {
  CreateAssignment: (projectId: string) => `/project/${projectId}/assignmnet`,
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
  phaseId: string;
  assignees: string[];
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
  subdomainId: string;
  phaseId: string;
};

export type CreateAssignmentResponse = { };

export function CreateAssignment(payload: CreateAssignmentRequest) {
  const api = useApi();
  return api.$post(AssignmentApiRoute.CreateAssignment(payload.projectId), payload);
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
};

export type UpdateAssignmentResponse = {};

export function UpdateAssignment(payload: UpdateAssignmentRequest) {
  const api = useApi();
  return api.$put(AssignmentApiRoute.AssignmentDetail(payload.assignmentId), payload);
}
