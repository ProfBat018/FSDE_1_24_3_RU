import {
  useCallback,
  useEffect,
  useMemo,
  useState,
  type ReactNode,
} from "react";
import type { LoginRequest } from "@features/auth/DTOs/auth.interfaces";
import type { LoginResult } from "./AuthContext";
import { AuthContext } from "./AuthContext";
import {
  loginTyped,
  logout as logoutApi,
} from "@features/auth/services/auth.service";
import { tokenStorage, userStorage } from "@/shared/tokenStorage";

export default function AuthProvider({ children }: { children: ReactNode }) {
  const [user, setUser] = useState<string | null>(null);
  const [loading, setLoading] = useState(false);

  useEffect(() => {
    const accessToken = tokenStorage.get();
    const refreshToken = localStorage.getItem("refresh_token");

    // Проверяем наличие обоих токенов
    if (!accessToken || !refreshToken) {
      // Если нет токенов, очищаем все данные
      tokenStorage.clear();
      userStorage.clear();
      setUser(null);
      return;
    }

    // Если токены есть, восстанавливаем пользователя
    const storedUser = userStorage.get<string>();
    if (storedUser) {
      setUser(storedUser);
    }
  }, []);

  const login = useCallback(
    async (payload: LoginRequest): Promise<LoginResult> => {
      setLoading(true);
      try {
        const result = await loginTyped(payload);

        if (result.isSuccess && result.data) {
          setUser(payload.email);
          tokenStorage.set(result.data);
          userStorage.set(payload.email);
          return { success: true };
        } else {
          return { success: false, error: result.message };
        }
      } catch (e: unknown) {
        const msg = e instanceof Error ? e.message : "Authentication error";
        return { success: false, error: msg };
      } finally {
        setLoading(false);
      }
    },
    []
  );

  const logout = useCallback(async (): Promise<void> => {
    try {
      await Promise.resolve(logoutApi());
    } finally {
      tokenStorage.clear();
      userStorage.clear();
      setUser(null);
    }
  }, []);

  const value = useMemo(
    () => ({
      user,
      loading,
      // ✅ вычисляемые поля для удобства в компонентах (AppNew.tsx уже их использует)
      isAuthenticated: !!user,
      isLoading: loading,

      login,
      logout,
      setUser,
    }),
    [user, loading, login, logout]
  );

  return <AuthContext.Provider value={value}>{children}</AuthContext.Provider>;
}
