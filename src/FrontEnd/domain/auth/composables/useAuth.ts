export const useAuth = defineStore('auth', () => {
  const jwtToken = ref('');
  const jwtComputed = computed(() => jwtToken.value);
  const refreshToken = ref('');
  const refreshComputed = computed(() => refreshToken.value);

  function setTokens(jwt: string, refresh: string) {
    jwtToken.value = jwt;
    refreshToken.value = refresh;
  }

  // onboarding context
  const username = ref('');
  const email = ref('');
  const passwordChangeToken = ref('');

  function setCurrentOnboardment(
    currentUsername: string, 
    currentEmail: string, 
    currentPasswordChangeToken: string
  ) {
    username.value = currentUsername;
    email.value = currentEmail;
    passwordChangeToken.value = currentPasswordChangeToken;
  }

  return {
    jwtToken: jwtComputed,
    refreshToken: refreshComputed,
    setTokens,
    setCurrentOnboardment
  };
});
