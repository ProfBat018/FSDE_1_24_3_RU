import type { LoginResponse } from '@/features/auth/DTOs/auth.interfaces';
import { Login } from '../features/auth/components/Login/Login';
const ACCESS_TOKEN_KEY = "access_token";
const REFRESH_TOKEN_KEY = "refresh_token";

const USER_KEY = "auth_user";

export const tokenStorage = {
  get(): string | null {
    try {
      return localStorage.getItem(ACCESS_TOKEN_KEY);
    } catch {
      return null;
    }
  },
  set(data: LoginResponse) {
    try {
      console.log(data.accessToken);

      localStorage.setItem(ACCESS_TOKEN_KEY, data.accessToken);
      localStorage.setItem(REFRESH_TOKEN_KEY, data.refreshToken);
    } catch {
      /* ignore */
    }
  },
  clear() {
    try {
      localStorage.removeItem(ACCESS_TOKEN_KEY);
      localStorage.removeItem(REFRESH_TOKEN_KEY);
    } catch {
      /* ignore */
    }
  },
  remove() {
    try {
      localStorage.removeItem(ACCESS_TOKEN_KEY);
      localStorage.removeItem(REFRESH_TOKEN_KEY);
    } catch {
      /* ignore */
    }
  },
};

export const userStorage = {
  get<T = unknown>(): T | null {
    try {
      const raw = localStorage.getItem(USER_KEY);
      return raw ? (JSON.parse(raw) as T) : null;
    } catch {
      return null;
    }
  },
  set(value: unknown) {
    try {
      localStorage.setItem(USER_KEY, JSON.stringify(value));
    } catch {
      /* ignore */
    }
  },
  clear() {
    try {
      localStorage.removeItem(USER_KEY);
    } catch {
      /* ignore */
    }
  },
};
