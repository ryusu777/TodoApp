export const useAuth = defineStore('auth', () => {
  const jwtToken = useCookie('jwt');
  const jwtComputed = computed(() => jwtToken.value);
  const refreshToken = useCookie('refresh');
  const refreshComputed = computed(() => refreshToken.value);

  function setTokens(jwt: string, refresh: string) {
    jwtToken.value = jwt;
    refreshToken.value = refresh;
  }

  function getUsername() {
    if (!jwtToken.value)
      return null;

    const base64Url = jwtToken.value.split(".")[1];
    const base64 = base64Url.replace(/-/g, "+").replace(/_/g, "/");
    const jsonPayload = decodeURIComponent(
      atob(base64)
        .split("")
        .map(function (c) {
          return "%" + ("00" + c.charCodeAt(0).toString(16)).slice(-2);
        })
        .join("")
    );

    const { sub } = JSON.parse(jsonPayload);
    return sub;
  }

  function clearAuth() {
    jwtToken.value = null;
    refreshToken.value = null;
  }

  function isAuthenticated() {
    if (!jwtToken.value)
      return false;

    if (isTokenExpired(jwtToken.value))
      return false;

    return true;
  }

  function isTokenExpired(token: string | undefined) {
    if (!token)
      return false;
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
    clearAuth,
    getUsername
  };
});
