import { Ref, ref } from 'vue';

export type ApiRequest<T> = () => Promise<T>; // Update to return data

export interface UseableApi<T> {
  response: Ref<T | undefined>;
  request: ApiRequest<T>; // Ensure the request returns data
}

let apiUrl = '';

export function setApiUrl(url: string) {
  apiUrl = url;
}

export default function useApi<T>(
  url: RequestInfo,
  options?: RequestInit
): UseableApi<T> {
  const response: Ref<T | undefined> = ref();

  const request: ApiRequest<T> = async () => {
    try {
      const fetchResponse = await fetch(`${apiUrl}/api/${url}`, options);
      if (!fetchResponse.ok) {
        throw new Error(`Network response was not ok, status: ${fetchResponse.status}`);
      }
      const data: T = await fetchResponse.json(); // Parse JSON data
      response.value = data; // Update the ref with the new data
      return data; // Return the data for immediate use as well
    } catch (error) {
      console.error('Fetch error:', error);
      throw error; // Rethrow after logging to handle it in the consuming function
    }
  };

  return { response, request };
}
