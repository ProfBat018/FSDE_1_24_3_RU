/* eslint-disable @typescript-eslint/no-explicit-any */
import { tokenStorage } from "@/shared/tokenStorage";
import axios from "axios";
import { createResponseMiddleware } from "@/shared/middlewares";
import { TypedResult, type ApiResponse } from "@/shared/types";

const AUTH_API_KEY = import.meta.env.VITE_AUTH_API || "http://localhost:5298";

export const authHttp = axios.create({
  baseURL: `${AUTH_API_KEY}/api/Auth`,
  withCredentials: true,
  timeout: 10000,
});

authHttp.interceptors.request.use((config) => {
  const token = tokenStorage.get();
  if (token) {
    config.headers = config.headers ?? {};
    (config.headers as Record<string, string>)[
      "Authorization"
    ] = `Bearer ${token}`;
  }
  return config;
});

const authResponseMiddleware = createResponseMiddleware({
  onSuccess: (response) => {
    console.log("Auth API Success:", response);
  },
  onError: (response) => {
    if (response.innerStatusCode === 401) {
      tokenStorage.remove();
    }
  },
  onStatusCodeMismatch: (externalStatus, internalStatus) => {
    console.warn(
      `Auth API Status code mismatch - External: ${externalStatus}, Internal: ${internalStatus}`
    );
  },
});

export const authHttpTyped = {
  async get<T>(url: string, config?: any): Promise<TypedResult<T>> {
    try {
      const response = await authHttp.get(url, config);

      return authResponseMiddleware.processResponse<T>(
        response.data as ApiResponse<T>
      );
    } catch (error: any) {
      if (error.response) {
        return authResponseMiddleware.processResponse<T>(
          error.response.data as ApiResponse<T>
        );
      }
      return TypedResult.error<T>(error.message || "Network error", 0);
    }
  },

  async post<T>(
    url: string,
    data?: any,
    config?: any
  ): Promise<TypedResult<T>> {
    try {
      const response = await authHttp.post(url, data, config);



      return authResponseMiddleware.processResponse<T>(
        response.data as ApiResponse<T>
      );
    } catch (error: any) {
      if (error.response) {
        return authResponseMiddleware.processResponse<T>(
          error.response.data as ApiResponse<T>
        );
      }

      return TypedResult.error<T>(error.message || "Network error", 0);
    }
  },

  async put<T>(url: string, data?: any, config?: any): Promise<TypedResult<T>> {
    try {
      const response = await authHttp.put(url, data, config);
      return authResponseMiddleware.processResponse<T>(
        response.data as ApiResponse<T>
      );
    } catch (error: any) {
      if (error.response) {
        return authResponseMiddleware.processResponse<T>(
          error.response.data as ApiResponse<T>
        );
      }
      return TypedResult.error<T>(error.message || "Network error", 0);
    }
  },

  async delete<T>(url: string, config?: any): Promise<TypedResult<T>> {
    try {
      const response = await authHttp.delete(url, config);
      return authResponseMiddleware.processResponse<T>(
        response.data as ApiResponse<T>
      );
    } catch (error: any) {
      if (error.response) {
        return authResponseMiddleware.processResponse<T>(
          error.response.data as ApiResponse<T>
        );
      }
      return TypedResult.error<T>(error.message || "Network error", 0);
    }
  },
};
