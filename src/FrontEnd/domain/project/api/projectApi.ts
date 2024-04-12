type GetProjectPagesResponse = {
  id: string;
  name: string;
};

export function GetProjectPages() {
  const api = useApi();
  return api.get<GetProjectPagesResponse[]>('/project/pages');
}
