import { isToken } from "typescript";

export const useAuth = defineStore('auth', () => {
  const jwtToken = useCookie('jwt');
  const jwtComputed = computed(() => jwtToken.value);
  const refreshToken = useCookie('refresh');
  const refreshComputed = computed(() => refreshToken.value);

  function setTokens(jwt: string, refresh: string) {
    jwtToken.value = jwt;
    refreshToken.value = refresh;
  }

  function clearAuth() {
    jwtToken.value = undefined;
    refreshToken.value = undefined;
  }

  function isAuthenticated() {
    if (!jwtToken.value)
      return false;

    if (isTokenExpired(jwtToken.value))
      return false;

    return true;
  }

  function isTokenExpired(token: string) {
    const base64Url = token.split(".")[1];
    const base64 = base64Url.replace(/-/g, "+").replace(/_/g, "/");
    const jsonPayload = decodeURIComponent(
      atob(base64)
        .split("")
        .map(function (c) {
          return "%" + ("00" + c.charCodeAt(0).toString(16)).slice(-2);
        })
        .join("")
    );

    const { exp } = JSON.parse(jsonPayload);
    const expired = Date.now() >= exp * 1000;
    return expired;
  }

  return {
    jwtToken: jwtComputed,
    refreshToken: refreshComputed,
    setTokens,
    isAuthenticated,
    isTokenExpired,
    clearAuth
  };
});
