export function useApiUtils() {
  async function tryFunction<TResponse>(
    callback: () => Promise<TResponse>,
    onSuccess: (response: TResponse) => any,
    onError: (errorDescription: string) => any
  ) {
    try {
      const response = await callback();
      onSuccess(response);
    } catch(e: any) {
      if (e.data?.errorDescription) 
        onError(e.data.errorDescription);
      else
        onError("Internal server error, please contact admin");
    }
  }

  return {
    try: tryFunction
  }
}
