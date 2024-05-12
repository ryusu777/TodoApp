import { useAuth } from "~/domain/auth/composables/useAuth";

export const GiteaApiRoutes = {
  AttachRepository: '/integration/attach-repository',
  GiteaRepositories: '/integration/gitea-repository',
  ProjectRepositories: (projectId: string) => `/integration/repository/${projectId}`
}

export type GiteaRepository = {
  id: number;
  repoOwner: string;
  repoName: string;
};

type GetGiteaRepositoryRequest = {
  searchText?: string;
  page?: number;
  itemPerPage?: number;
}

type GetGiteaRepositoryResponse = GiteaRepository[];

export function GetGiteaRepository(payload: GetGiteaRepositoryRequest) {
  const api = useApi();
  const auth = useAuth();
  return api
    .$get<GetGiteaRepositoryResponse>(GiteaApiRoutes.GiteaRepositories, payload, {
      'Authorization': 'Bearer ' + auth.jwtToken
    });
}

type GetProjectRepositoryResponse = GetGiteaRepositoryResponse;

export function GetProjectRepository(projectId: string) {
  const api = useApi();
  const auth = useAuth();
  return api
    .$get<GetProjectRepositoryResponse>(
      GiteaApiRoutes.ProjectRepositories(projectId), 
      undefined,
      {
        'Authorization': 'Bearer ' + auth.jwtToken
      });
}

type AttachRepositoryRequest = {
  projectId: string;
  repoOwner: string;
  repoName: string;
}

export function AttachRepository(payload: AttachRepositoryRequest) {
  const api = useApi();
  const auth = useAuth();
  return api
    .$post(GiteaApiRoutes.AttachRepository, payload, {
      'Authorization': 'Bearer ' + auth.jwtToken
    });
}
