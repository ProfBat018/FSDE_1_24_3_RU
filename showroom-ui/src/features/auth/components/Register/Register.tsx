import * as React from "react";
import * as Form from "@radix-ui/react-form";

type RegisterFormProps = {
  onSubmit?: (data: { email: string; password: string; confirmPassword: string }) => void;
};

export const Register: React.FC<RegisterFormProps> = ({ onSubmit }) => {
  const [password, setPassword] = React.useState("");
  const confirmRef = React.useRef<HTMLInputElement | null>(null);

  const handleSubmit = (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    const form = e.currentTarget;
    const data = Object.fromEntries(new FormData(form)) as {
      email: string;
      password: string;
      confirmPassword: string;
    };
    onSubmit?.(data);
  };

  // держим customValidity для confirmPassword в актуальном состоянии
  const validateConfirm = React.useCallback(
    (value: string) => {
      const el = confirmRef.current;
      if (!el) return;
      el.setCustomValidity(value !== password ? "Пароли не совпадают" : "");
    },
    [password]
  );

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
        <Form.Message match="valueMissing" className="text-red-600 text-sm mt-1">
          Укажите email.
        </Form.Message>
        <Form.Message match="typeMismatch" className="text-red-600 text-sm mt-1">
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
            onChange={(e) => {
              setPassword(e.target.value);
              // переставим валидацию у confirm при каждом изменении
              validateConfirm(confirmRef.current?.value ?? "");
            }}
          />
        </Form.Control>
        <Form.Message match="valueMissing" className="text-red-600 text-sm mt-1">
          Укажите пароль.
        </Form.Message>
        <Form.Message match="tooShort" className="text-red-600 text-sm mt-1">
          Минимум 6 символов.
        </Form.Message>
      </Form.Field>

      <Form.Field name="confirmPassword">
        <div className="mb-1">
          <Form.Label className="block text-sm font-medium">Подтверждение пароля</Form.Label>
        </div>
        <Form.Control asChild>
          <input
            ref={confirmRef}
            type="password"
            required
            placeholder="••••••••"
            className="w-full rounded-lg border px-3 py-2 outline-none"
            onInput={(e) => validateConfirm((e.target as HTMLInputElement).value)}
          />
        </Form.Control>
        <Form.Message match="valueMissing" className="text-red-600 text-sm mt-1">
          Подтвердите пароль.
        </Form.Message>
        {/* сообщение из setCustomValidity */}
        <Form.Message className="text-red-600 text-sm mt-1" match={(value) => {
          // показываем, если кастомная ошибка установлена
          return (confirmRef.current?.validationMessage ?? "") !== "" && value !== "";
        }}>
          {confirmRef.current?.validationMessage || ""}
        </Form.Message>
      </Form.Field>

      <Form.Submit asChild>
        <button
          type="submit"
          className="w-full rounded-lg border px-3 py-2 font-medium"
        >
          Зарегистрироваться
        </button>
      </Form.Submit>
    </Form.Root>
  );
};
