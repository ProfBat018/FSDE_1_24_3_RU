import { createContext } from "react";
import type { AuthUser, LoginRequest } from "@features/DTOs/auth.interfaces";

export type LoginResult =
  | { success: true }
  | { success: false; error?: string };

export interface AuthContextValue {
  user: AuthUser | null;
  loading: boolean;

  isAuthenticated: boolean;
  isLoading: boolean;

  login: (payload: LoginRequest) => Promise<LoginResult>;
  logout: () => Promise<void> | void;
  setUser: (u: AuthUser | null) => void;
}

export const AuthContext = createContext<AuthContextValue | null>(null);