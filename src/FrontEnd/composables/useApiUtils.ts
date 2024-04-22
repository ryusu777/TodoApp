export function useApiUtils() {

  async function tryFunction<TResponse>(
    promise: Promise<TResponse>,
    onSucess: (response: TResponse) => any,
    onError: (errorDescription: string) => any
  ) {
    try {
      return await promise;
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
