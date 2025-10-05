/* eslint-disable @typescript-eslint/no-explicit-any */
import { TypedResult, type ApiResponse } from "../types/TypedResult";

export interface ResponseMiddlewareOptions {
  onSuccess?: (response: ApiResponse<unknown>) => void;
  onError?: (response: ApiResponse<unknown>) => void;
  onStatusCodeMismatch?: (
    externalStatus: number,
    internalStatus: number
  ) => void;
}

export class ResponseMiddleware {
  private options: ResponseMiddlewareOptions;

  constructor(options: ResponseMiddlewareOptions = {}) {
    this.options = options;
  }

  public async processResponse<T>(
    data: ApiResponse<T>
  ): Promise<TypedResult<T>> {
    try {
      console.log(data);

      if (data.isSuccess) {
        this.options.onSuccess?.(data);
        return TypedResult.success<T>(
          data.data,
          data.message,
          data.innerStatusCode
        );
      }

      this.options.onError?.(data);

      return TypedResult.error<T>(data.message, data.innerStatusCode);
    } catch (error) {
      const errorMessage =
        error instanceof Error ? error.message : "Unknown error occurred";
      return TypedResult.error<T>(
        `Failed to parse response: ${errorMessage}`,
        data.innerStatusCode
      );
    }
  }

  public createFetchHandler() {
    return async <T>(
      url: string,
      options: RequestInit = {}
    ): Promise<TypedResult<T>> => {
      try {
        const response = await fetch(url, {
          ...options,
          headers: {
            "Content-Type": "application/json",
            ...options.headers,
          },
        });

        return await this.processResponse<T>(response as any);
      } catch (error) {
        const errorMessage =
          error instanceof Error ? error.message : "Network error occurred";
        return TypedResult.error<T>(errorMessage, 0);
      }
    };
  }
}

export const defaultResponseMiddleware = new ResponseMiddleware({
  onSuccess: (response) => {
    console.log("API Success:", response);
  },
  onError: (response) => {
    console.error("API Error:", response);
  },
  onStatusCodeMismatch: (externalStatus, internalStatus) => {
    console.warn(
      `Status code mismatch - External: ${externalStatus}, Internal: ${internalStatus}`
    );
  },
});

export const createResponseMiddleware = (
  options: ResponseMiddlewareOptions
) => {
  return new ResponseMiddleware(options);
};
