export interface LoginRequest {
  email: string;
  password: string;
}

export interface AuthUser {
  id: string;
  email: string;
}

export interface LoginResponse {
  accessToken: string;
  refreshToken: string;
}
