import * as React from "react";
import * as Form from "@radix-ui/react-form";
import { useAuthContext } from "../../hooks/useAuthContext";

export const Login: React.FC = () => {
  const [error, setError] = React.useState<string | null>(null);
  const { login, isLoading } = useAuthContext();

  const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    setError(null);

    const form = e.currentTarget;
    const data = Object.fromEntries(new FormData(form)) as {
      email: string;
      password: string;
    };

    const result = await login(data);

    if (result.success) {
      console.log("Успешный вход");
    } else {
      setError(result.error || "Ошибка входа. Проверьте email и пароль.");
      console.error("Ошибка входа:", result.error);
    }
  };

  return (
    <Form.Root onSubmit={handleSubmit} className="space-y-4 max-w-sm">
      <Form.Field name="email">
        <div className="mb-1">
          <Form.Label className="block text-sm font-medium">Email</Form.Label>
        </div>
        <Form.Control asChild>
          <input
            type="email"
            required
            placeholder="you@example.com"
            className="w-full rounded-lg border px-3 py-2 outline-none"
          />
        </Form.Control>
        <Form.Message
          match="valueMissing"
          className="text-red-600 text-sm mt-1"
        >
          Укажите email.
        </Form.Message>
        <Form.Message
          match="typeMismatch"
          className="text-red-600 text-sm mt-1"
        >
          Неверный формат email.
        </Form.Message>
      </Form.Field>

      <Form.Field name="password">
        <div className="mb-1">
          <Form.Label className="block text-sm font-medium">Пароль</Form.Label>
        </div>
        <Form.Control asChild>
          <input
            type="password"
            required
            minLength={6}
            placeholder="••••••••"
            className="w-full rounded-lg border px-3 py-2 outline-none"
          />
        </Form.Control>
        <Form.Message
          match="valueMissing"
          className="text-red-600 text-sm mt-1"
        >
          Укажите пароль.
        </Form.Message>
        <Form.Message match="tooShort" className="text-red-600 text-sm mt-1">
          Минимум 6 символов.
        </Form.Message>
      </Form.Field>

      {error && <div className="text-red-600 text-sm">{error}</div>}

      <Form.Submit asChild>
        <button
          type="submit"
          disabled={isLoading}
          className="w-full rounded-lg border px-3 py-2 font-medium"
        >
          {isLoading ? "Вход..." : "Войти"}
        </button>
      </Form.Submit>
    </Form.Root>
  );
};
