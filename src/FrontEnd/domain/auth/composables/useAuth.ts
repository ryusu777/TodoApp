export const useAuth = defineStore('auth', () => {
  const jwtToken = ref('');
  const jwtComputed = computed(() => jwtToken.value);
  const refreshToken = ref('');
  const refreshComputed = computed(() => refreshToken.value);

  function setTokens(jwt: string, refresh: string) {
    jwtToken.value = jwt;
    refreshToken.value = refresh;
  }

  return {
    jwtToken: jwtComputed,
    refreshToken: refreshComputed,
    setTokens
  };
});
