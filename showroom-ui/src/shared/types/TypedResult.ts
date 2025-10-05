export class TypedResult<T> {
  public readonly isSuccess: boolean;
  public readonly message: string;
  public readonly innerStatusCode: number;
  public readonly data: T | null;

  private constructor(
    isSuccess: boolean,
    message: string,
    statusCode: number,
    data: T | null = null
  ) {
    this.isSuccess = isSuccess;
    this.message = message;
    this.innerStatusCode = statusCode;
    this.data = data;
  }

  public static success<T>(
    data: T | null = null,
    message: string = "Success",
    statusCode: number = 200
  ): TypedResult<T> {
    return new TypedResult<T>(true, message, statusCode, data);
  }

  public static error<T>(
    message: string = "An error occurred",
    statusCode: number = 400
  ): TypedResult<T> {
    return new TypedResult<T>(false, message, statusCode, null);
  }
}

export interface ApiResponse<T> {
  isSuccess: boolean;
  message: string;
  innerStatusCode: number;
  data: T | null;
}
