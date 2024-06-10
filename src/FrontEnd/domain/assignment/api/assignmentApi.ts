import { useAuth } from "~/domain/auth/composables/useAuth";

export const AssignmentApiRoute = {
  Assignments: (projectId: string) => `/project/${projectId}/assignment`,
  GetAssignments: (projectId: string, subdomainId?: string) => `/project/${projectId}/assignments/${subdomainId}`,
  DeleteAssignment: (assignmentId: string) => `/assignment/${assignmentId}`,
  AssignmentDetail: (assignmentId: string) => `/assignment/${assignmentId}`,
  Assigning: (assignmentId: string) => `/assignment/${assignmentId}/assign`,
  RemoveAssignee: (assignmentId: string) => `/assignment/${assignmentId}/remove-assignee`,
  AssignmentStatus: (assignmentId: string) => `/assignment/${assignmentId}/status`,
  WorkOnAssignment: (assignmentId: string) => `/assignment/${assignmentId}/work-on`,
  RequestAssignmentReview: (assignmentId: string) => `/assignment/${assignmentId}/request-review`,
  ApproveAssignmentReview: (assignmentId: string) => `/assignment/${assignmentId}/approve-review`,
  RejectAssignmentReview: (assignmentId: string) => `/assignment/${assignmentId}/reject-review`,
  ReopenAssignment: (assignmentId: string) => `/assignment/${assignmentId}/reopen`,
};

export type AssignmentStatusEnum = 'Revised' | 'New' | 'OnProgress' | 'WaitingReview' | 'Completed';

export interface Review {
  reviewer: string;
  status: 'New' | 'Approved' | 'Rejected';
  description: string;
  rejectionNotes: string;
}

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
  lastReview: Review;
}

export type GetAssignmentsRequest = {
  projectId: string;
  subdomainId: string;
}

export type GetAssignmentsResponse = Assignment[];

export function GetAssignments(payload: GetAssignmentsRequest) {
  const api = useApi();
  const auth = useAuth();
  return api
    .$get<GetAssignmentsResponse>(AssignmentApiRoute.GetAssignments(payload.projectId, payload.subdomainId), null,
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

export type WorkOnAssignmentRequest = {
  assignmentId: string;
};

export type WorkOnAssignmentResponse = {};

export function WorkOnAssignment(payload: WorkOnAssignmentRequest) {
  const api = useApi();
  const auth = useAuth();
  return api.$patch(AssignmentApiRoute.WorkOnAssignment(payload.assignmentId), payload, {
    'Authorization': 'Bearer ' + auth.jwtToken
  });
}

export type RequestAssignmentReviewRequest = {
  assignmentId: string;
  description: string;
};

export type RequestAssignmentReviewResponse = {};

export function RequestAssignmentReview(payload: RequestAssignmentReviewRequest) {
  const api = useApi();
  const auth = useAuth();
  return api.$patch(AssignmentApiRoute.RequestAssignmentReview(payload.assignmentId), payload, {
    'Authorization': 'Bearer ' + auth.jwtToken
  });
}

export type ApproveAssignmentReviewRequest = {
  assignmentId: string;
};

export type ApproveAssignmentReviewResponse = {};

export function ApproveAssignmentReview(payload: ApproveAssignmentReviewRequest) {
  const api = useApi();
  const auth = useAuth();
  return api.$patch(AssignmentApiRoute.ApproveAssignmentReview(payload.assignmentId), payload, {
    'Authorization': 'Bearer ' + auth.jwtToken
  });
}

export type RejectAssignmentReviewRequest = {
  assignmentId: string;
  rejectionNotes: string;
};

export type RejectAssignmentReviewResponse = {};

export function RejectAssignmentReview(payload: RejectAssignmentReviewRequest) {
  const api = useApi();
  const auth = useAuth();
  return api.$patch(AssignmentApiRoute.RejectAssignmentReview(payload.assignmentId), payload, {
    'Authorization': 'Bearer ' + auth.jwtToken
  });
}

export type ReopenAssignmentRequest = {
  assignmentId: string;
};

export type ReopenAssignmentResponse = {};

export function ReopenAssignment(payload: ReopenAssignmentRequest) {
  const api = useApi();
  const auth = useAuth();
  return api.$patch(AssignmentApiRoute.ReopenAssignment(payload.assignmentId), payload, {
    'Authorization': 'Bearer ' + auth.jwtToken
  });
}
