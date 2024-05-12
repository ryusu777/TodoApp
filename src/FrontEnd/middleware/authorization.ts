import { RefreshToken } from "~/domain/auth/api/authApi";
import { useAuth } from "~/domain/auth/composables/useAuth";

export default defineNuxtRouteMiddleware(async (to) => {
  const authStore = useAuth();

  if (authStore.isAuthenticated())
    return;

  if (!authStore.jwtToken)
    return '/login';

  if (authStore.refreshToken && !authStore.isTokenExpired(authStore.refreshToken))
    return '/login';

  const { data } = await useAsyncData(() => RefreshToken({
    jwtToken: authStore.jwtToken!,
    refreshToken: authStore.refreshToken!
  }));

  if (!data.value?.data?.access_token || !data.value.data.refresh_token)
    return '/login';

  authStore.setTokens(
    data.value?.data?.access_token,
    data.value.data.refresh_token
  );

  return;
});
