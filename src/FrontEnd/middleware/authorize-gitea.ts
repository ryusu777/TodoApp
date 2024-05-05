import { AuthorizeGitea } from "~/domain/auth/api/authApi";
import { useAuth } from "~/domain/auth/composables/useAuth";

export default defineNuxtRouteMiddleware(async (to) => {
  const toast = useToast();

  const authStore = useAuth();

  const authCode = to.query.code ?? "";

  const { data: authorizeResult } = await useAsyncData(() => AuthorizeGitea(authCode as string));

  if (authorizeResult.value?.errorDescription) {
    toast.add({
      title: 'Failed',
      description: authorizeResult.value?.errorDescription,
      color: 'red'
    });
    return navigateTo('/login');
  }

  else if (authorizeResult.value?.data?.authResult) {
    authStore.setTokens(
      authorizeResult.value.data?.authResult.access_token,
      authorizeResult.value.data?.authResult.refresh_token
    );

    return navigateTo('/');
  }
  else if (authorizeResult.value?.data?.onboardInformation) {
    authStore.setCurrentOnboardment(
      authorizeResult.value.data.onboardInformation.username,
      authorizeResult.value.data.onboardInformation.email,
      authorizeResult.value.data.onboardInformation.passwordChangeToken,
    );
    return navigateTo('/onboard-user');
  }

  return;
});
