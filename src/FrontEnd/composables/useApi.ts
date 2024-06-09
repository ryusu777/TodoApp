export interface IApiResponse<T=void> {
  errorDescription?: string;
  data?: T;
}


export const useApi = defineStore('api', () => {
  const API_URL = useRuntimeConfig().public.API_URL;
  /*
   * Sends request on client side
   */
  function $get<T>(endpoint: string, payload?: any, headers?: any) {
    return $fetch<IApiResponse<T>>(API_URL + endpoint, {
      query: payload,
      headers: headers
    });
  }

  /**
   * Sends request on server side before hydration
   */
  function get<T>(endpoint: string) {
    return useFetch<IApiResponse<T>>(API_URL + endpoint, {
      server: true
    });
  }

  /**
   * Sends request on client side
   */
  function $post<T>(endpoint: string, payload: any, headers?: any) {
    return $fetch<IApiResponse<T>>(API_URL + endpoint, {
      method: 'POST',
      body: payload,
      headers
    });
  }

  /**
   * Sends request on server side before hydration
   */
  function post<T>(endpoint: string, payload: any) {
    return useFetch<IApiResponse<T>>(API_URL + endpoint, {
      method: 'POST',
      body: payload,
    });
  }

  /**
   * Sends request on client side
   */
  function $put<T>(endpoint: string, payload: any, headers?: any) {
    return $fetch<IApiResponse<T>>(API_URL + endpoint, {
      method: 'PUT',
      body: payload,
      headers
    });
  }

  /**
   * Sends request on client side
   */
  function $delete<T>(endpoint: string, payload?: any, headers?: any) {
    return $fetch<IApiResponse<T>>(API_URL + endpoint, {
      method: 'DELETE',
      query: payload,
      headers
    });
  }

  /**
   * Sends request on server side before hydration
   */
  function deleteFun<T>(endpoint: string) {
    return useFetch<IApiResponse<T>>(API_URL + endpoint, {
      method: 'DELETE',
    });
  }

  return {
    $get,
    get,
    $post,
    post,
    $put,
    $delete,
    delete: deleteFun
  }
});
