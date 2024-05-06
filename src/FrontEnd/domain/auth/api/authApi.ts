export const AuthApiRoute = {
  SignIn: "/auth/sign-in",
  SignInWithGitea: "/auth/sign-in-gitea",
  AuthorizeGitea: "/auth/authorize-gitea",
  OnboardUser: "/auth/onboard-user"
}

type SignInRequest = {
  username: string;
  password: string;
}

type SignInResponse = {
  access_token: string;
  expires_in: number;
  token_type: string;
  refresh_token: string;
}

export function SignIn(payload: SignInRequest) {
  const api = useApi();
  return api
    .$post<SignInResponse>(AuthApiRoute.SignIn, payload);
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

type OnboardUserRequest = {
  username: string;
  email: string;
  password: string;
  changePasswordCode: string;
};

export function OnboardUser(payload: OnboardUserRequest) {
  const api = useApi();
  return api.$post(AuthApiRoute.OnboardUser, payload);
}
