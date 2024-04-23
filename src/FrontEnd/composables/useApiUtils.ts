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
      if ('data' in e) {
        if ('errorDescription' in e.data)
          onError(e.data.errorDescription);
        else
          onError(e.message);
      }
    }
  }

  return {
    try: tryFunction
  }
}
