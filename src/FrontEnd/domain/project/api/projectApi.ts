import { useAuth } from "~/domain/auth/composables/useAuth";

export const ProjectApiRoute = {
  Project: '/project',
  ProjectDetail: (id: string) => `/project/${id}`,
  GetProjectPages: '/project/pages',
  Members: (projectId: string) => `/project/${projectId}/members`,
  SyncMembers: (projectId: string) => `/project/${projectId}/sync-members`,
  Phases: (projectId: string) => `/project/${projectId}/phases`,
  Hierarchy: (projectId: string) => `/project/${projectId}/hierarchies`,
  HierarchyDetail: (projectId: string, hierarchyId?: string) => `/project/${projectId}/hierarchies/${hierarchyId}`,
  HierarchyMembers: (projectId: string, hierarchyId?: string) => `/project/${projectId}/hierarchies/${hierarchyId}/members`,
  GetAssignableHierarchies: (projectId: string) => `/project/${projectId}/assignable-hierarchies`,
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

export interface Hierarchy {
  id: string;
  name: string;
  superiorHierarchyId: string;
  memberUsernames: string[];
}

export interface Project {
  id: string;
  name: string;
  description: string;
  status: number;
  projectPhases: Phase[];
  projectHierarchies: Hierarchy[];
  projectMembers: string[];
  numOfNewAssignment?: number;
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
  projectPhases: {
    name: string,
    startDate: string;
    endDate: string;
    description: string;
  }[];
  projectHierarchies: {
    name: string;
    superiorHierarchyId: string;
    memberUsernames: string[];
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

export type UpdateProjectHierarchyDetailRequest = {
  projectId: string;
  hierarchyId: string;
  name: string;
  superiorHierarchyId?: string;
}

export type UpdateProjectHierarchyDetailResponse = IApiResponse;

export function UpdateProjectHierarchyDetail(request: UpdateProjectHierarchyDetailRequest) {
  const api = useApi();
  return api.$put(ProjectApiRoute.HierarchyDetail(request.projectId, request.hierarchyId), request);
}

export type UpdateProjectHierarchyMembersRequest = {
  projectId: string;
  hierarchyId: string;
  memberUsernames: string[];
}

export type UpdateProjectHierarchyMembersResponse = IApiResponse;

export function UpdateProjectHierarchyMembers(request: UpdateProjectHierarchyMembersRequest) {
  const api = useApi();
  return api.$put(ProjectApiRoute.HierarchyMembers(request.projectId, request.hierarchyId), request);
}

export type CreateProjectHierarchyRequest = {
  projectId: string;
  name: string;
  superiorHierarchyId?: string;
  memberUsernames: string[];
}

export type CreateProjectHierarchyResponse = IApiResponse;

export function CreateProjectHierarchy(request: CreateProjectHierarchyRequest) {
  const api = useApi();
  return api.$post(ProjectApiRoute.Hierarchy(request.projectId), request);
}

export type DeleteProjectHierarchyResponse = IApiResponse;

export function DeleteProjectHierarchy(projectId: string, hierarchyId: string) {
  const api = useApi();
  return api.$delete(ProjectApiRoute.HierarchyDetail(projectId, hierarchyId));
}

export type GetAllProjectMembersRequest = {
  projectId: string;
};

export type GetAllProjectMembersResponse = {
  projectId: string;
  memberUsernames: string[]
};

export function GetAllProjectMembers(request: GetAllProjectMembersRequest) {
  const api = useApi();
  return api.$get<GetAllProjectMembersResponse>(ProjectApiRoute.Members(request.projectId));
}

export type GetAssignableHierarchiesResponse = {
  projectId: string;
  hierarchies: Hierarchy[];
};

export function GetAssignableHierarchies(projectId: string) {
  const auth = useAuth();
  const api = useApi();
  return api.$get<GetAssignableHierarchiesResponse>(
    ProjectApiRoute.GetAssignableHierarchies(projectId), null, {
      'Authorization': 'Bearer ' + auth.jwtToken
    });
}

