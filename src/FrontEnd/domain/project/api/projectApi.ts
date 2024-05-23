import { useAuth } from "~/domain/auth/composables/useAuth";

export const ProjectApiRoute = {
  Project: '/project',
  ProjectDetail: (id: string) => `/project/${id}`,
  GetProjectPages: '/project/pages',
  Members: (projectId: string) => `/project/${projectId}/members`,
  SyncMembers: (projectId: string) => `/project/${projectId}/sync-members`,
  Phases: (projectId: string) => `/project/${projectId}/phases`
};

export interface Phase {
  id?: string;
  name: string,
  startDate: string;
  endDate: string;
  description: string;
}

export interface Member {
  username: string;
}

export interface Project {
  id: string;
  name: string;
  description: string;
  status: number;
  projectMembers: string[];
  projectPhases: Phase[];
}


type GetProjectPagesResponse = {
  id: string;
  name: string;
};

export function GetProjectPages() {
  const api = useApi();
  return api.get<GetProjectPagesResponse[]>(ProjectApiRoute.GetProjectPages);
}

export type CreateProjectRequest = {
  code: string;
  name: string;
  description: string;
  projectMembers: string[];
  projectPhases: {
    name: string,
    startDate: string;
    endDate: string;
    description: string;
  }[];
}

type CreateProjectResponse = {};

export function CreateProject(request: CreateProjectRequest) {
  const api = useApi();
  return api.$post<CreateProjectResponse>(ProjectApiRoute.Project, request);
}

type DeleteProjectResponse = {};

export function DeleteProject(projectId: string) {
  const api = useApi();
  return api.$delete<DeleteProjectResponse>(ProjectApiRoute.ProjectDetail(projectId));
}

export type GetProjectByIdResponse = Project;

export function GetProjectById(projectId: string) {
  const api = useApi();
  return api.$get<GetProjectByIdResponse>(ProjectApiRoute.ProjectDetail(projectId));
}

export type UpdateProjectRequest = {
  projectId: string;
  name: string;
  description: string;
  status: number;
};

type UpdateProjectResponse = IApiResponse;

export function UpdateProject(request: UpdateProjectRequest) {
  const api = useApi();
  return api.$put(ProjectApiRoute.ProjectDetail(request.projectId), request);
}

export type UpdateProjectMembersRequest = {
  projectId: string;
  memberUsernames: string[];
}

type UpdateProjectMembersResponse = IApiResponse;

export function UpdateProjectMembers(request: UpdateProjectMembersRequest) {
  const api = useApi();
  return api.$put(ProjectApiRoute.Members(request.projectId), request);
}

export type SyncProjectMembersRequest = {
  projectId: string;
}

type SyncProjectMembersResponse = IApiResponse;

export function SyncProjectMembers(request: SyncProjectMembersRequest) {
  const api = useApi();
  const auth = useAuth();
  return api.$post(ProjectApiRoute.SyncMembers(request.projectId), request,
    {
      'Authorization': 'Bearer ' + auth.jwtToken
    });
}

export type UpdateProjectPhasesRequest = {
  projectId: string;
  phases: Phase[];
}

type UpdateProjectPhasesResponse = IApiResponse;

export function UpdateProjectPhases(request: UpdateProjectPhasesRequest) {
  const api = useApi();
  return api.$put(ProjectApiRoute.Phases(request.projectId), request);
}
