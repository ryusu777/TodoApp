export interface IApiResponse<T=void> {
  errorDescription?: string;
  data?: T;
}

const API_URL = "http://localhost:5168/api";

export const useApi = () => {
  /**
   * Sends request on client side
   */
  function $get<T>(endpoint: string) {
    return useFetch<IApiResponse<T>>(API_URL + endpoint, {
      server: false
    });
  }

  /**
   * Sends request on server side before hydration
   */
  function get<T>(endpoint: string) {
    return useFetch<IApiResponse<T>>(API_URL + endpoint);
  }

  /**
   * Sends request on client side
   */
  function $post<T>(endpoint: string) {
    return useFetch<IApiResponse<T>>(API_URL + endpoint, {
      server: false,
      method: 'POST'
    });
  }

  /**
   * Sends request on server side before hydration
   */
  function post<T>(endpoint: string) {
    return useFetch<IApiResponse<T>>(API_URL + endpoint, {
      method: 'POST'
    });
  }

  /**
   * Sends request on client side
   */
  function $put<T>(endpoint: string) {
    return useFetch<IApiResponse<T>>(API_URL + endpoint, {
      server: false,
      method: 'PUT'
    });
  }

  /**
   * Sends request on server side before hydration
   */
  function put<T>(endpoint: string) {
    return useFetch<IApiResponse<T>>(API_URL + endpoint, {
      method: 'PUT'
    });
  }

  /**
   * Sends request on client side
   */
  function $delete<T>(endpoint: string) {
    return useFetch<IApiResponse<T>>(API_URL + endpoint, {
      server: false,
      method: 'DELETE'
    });
  }

  /**
   * Sends request on server side before hydration
   */
  function deleteFun<T>(endpoint: string) {
    return useFetch<IApiResponse<T>>(API_URL + endpoint, {
      method: 'DELETE'
    });
  }

  return {
    $get,
    get,
    $post,
    post,
    $put,
    put,
    $delete,
    delete: deleteFun
  }
}
