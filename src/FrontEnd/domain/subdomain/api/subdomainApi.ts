export const SubdomainApiRoute = {
  Subdomain: (projectId: string) => `/project/${projectId}/subdomain`,
  SubdomainDetail: (subdomainId: string) => `/subdomain/${subdomainId}`,
  SubdomainKnowledgeDetail: (knowledgeId: string) => `/subdomain-knowledge/${knowledgeId}`,
  DeleteSubdomainKnowledge: (subdomainId: string, knowledgeId: string) => `/subdomain/${subdomainId}/knowledge/${knowledgeId}`
};

export interface Subdomain {
  id?: string;
  description: string;
  title: string;
  projectId: string;
  knowledges?: SubdomainKnowledge[] | null;
}

export interface SubdomainKnowledge {
  id?: string;
  title: string;
  content: string;
  subdomainId: string;
}

export type CreateSubdomainRequest = {
  title: string;
  description: string;
  projectId: string;
};

type CreateSubdomainResponse = {};

export function CreateSubdomain(request: CreateSubdomainRequest) {
  const api = useApi();
  return api.$post<CreateSubdomainResponse>(SubdomainApiRoute.Subdomain(request.projectId), request);
}

export type CreateSubdomainKnowledgeRequest = {
  title: string;
  content: string;
  subdomainId: string;
};

type CreateSubdomainKnowledgeResponse = {};

export function CreateSubdomainKnowledge(request: CreateSubdomainKnowledgeRequest) {
  const api = useApi();
  return api.$post<CreateSubdomainKnowledgeResponse>(SubdomainApiRoute.SubdomainDetail(request.subdomainId), request);
}

export type DeleteSubdomainRequest = {
  subdomainId: string;
};

type DeleteSubdomainResponse = {};

export function DeleteSubdomain(request: DeleteSubdomainRequest) {
  const api = useApi();
  return api.$delete<DeleteSubdomainResponse>(SubdomainApiRoute.SubdomainDetail(request.subdomainId));
}

export type DeleteSubdomainKnowledgeRequest = {
  subdomainId: string;
  knowledgeId: string;
};

type DeleteSubdomainKnowledgeResponse = {};

export function DeleteSubdomainKnowledge(request: DeleteSubdomainKnowledgeRequest) {
  const api = useApi();
  return api.$delete<DeleteSubdomainKnowledgeResponse>(SubdomainApiRoute.DeleteSubdomainKnowledge(request.subdomainId, request.knowledgeId));
}

export type UpdateSubdomainRequest = {
  subdomainId: string;
  title: string;
  description: string;
};

type UpdateSubdomainResponse = {};

export function UpdateSubdomain(request: UpdateSubdomainRequest) {
  const api = useApi();
  return api.$put<UpdateSubdomainResponse>(SubdomainApiRoute.SubdomainDetail(request.subdomainId), request);
}

export type UpdateSubdomainKnowledgeRequest = {
  subdomainKnowledgeId: string;
  title: string;
  content: string;
  subdomainId: string;
};

type UpdateSubdomainKnowledgeResponse = {};

export function UpdateSubdomainKnowledge(request: UpdateSubdomainKnowledgeRequest) {
  const api = useApi();
  return api
    .$put(SubdomainApiRoute
      .SubdomainKnowledgeDetail(request.subdomainKnowledgeId), 
      request);
}

export type GetSubdomainsRequest = {
  projectId: string;
}

export type GetSubdomainsResponse = Subdomain[];

export function GetSubdomains(projectId: string) {
  const api = useApi();
  return api.$get<GetSubdomainsResponse>(SubdomainApiRoute.Subdomain(projectId));
}

export type GetSubdomainRequest = {
  subdomainId: string;
}

type GetSubdomainResponse = Subdomain;

export function GetSubdomain(subdomainId: string) {
  const api = useApi();
  return api.$get<GetSubdomainResponse>(SubdomainApiRoute.SubdomainDetail(subdomainId));
}
