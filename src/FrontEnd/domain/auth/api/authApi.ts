export const AuthApiRoute = {
  SignInWithGitea: "/auth/sign-in-gitea",
  AuthorizeGitea: "/auth/authorize-gitea"
}

type SignInWithGiteaResponse = string;

export function SignInWithGitea() {
  const api = useApi();
  return api.$get<SignInWithGiteaResponse>(AuthApiRoute.SignInWithGitea);
}

type AuthorizeGiteaRequest = {
  authorizationCode: string;
};

type AuthorizeGiteaResponse = {
  authResult: {
    access_token: string;
    expires_in: number;
    token_type: string;
    refresh_token: string;
  };
  onboardInformation: {
    passwordChangeToken: string;
    username: string;
    email: string;
  }
};

export function AuthorizeGitea(authorizationCode: string) {
  const api = useApi();
  return api
    .$post<AuthorizeGiteaResponse>(AuthApiRoute.AuthorizeGitea, { authorizationCode });
}
