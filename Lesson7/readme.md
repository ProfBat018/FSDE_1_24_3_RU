# Тема урока 
- Кастомные хуки 
- Zustand 
- Zustand vs React-Redux
- Radix UI 
- Zod 
- CommonJs и ESM
- vite vs babel

## Кастомные хуки 

В будущем, когда вы будете писать уже продакшн-код, вы очень часто будете писать свои хуки. Вот пример кастомного хука в проекте Ecommerce, где мне нужно было написать хук, который будет автоматически скачитвать и переводить категории товаров. В моем случае это было так, потому что я написал PWA приложение, где при обновлении категорий со стороны администратора пользователю автоматически приходят новые категории. Также перевод этих категорий был реализован на `Back-end` и связан с `i18n`. 

```javascript

// hooks/useCategoriesSWR.ts

import useSWR from 'swr';
import { useEffect } from 'react';
import { useTranslation } from 'react-i18next';
import { buildCategoryTree, CategoryNode } from '../utils/buildCategoryTree';
import { CategoryResponseDTO } from '../types/responseDTOs';
import { fetchAllCategories } from '@/services/techCommerceApi';
import { getCategoryHubConnection } from '@/services/signalR/categoryHub';

export const useCategoriesSWR = () => {
  const { i18n } = useTranslation();
  const locale = i18n.language; // 'ru', 'uz', 'en' и т.д.

  const { data, error, mutate } = useSWR<CategoryResponseDTO[]>(
    ['categories', locale],
    () => fetchAllCategories(locale),
    { revalidateOnFocus: false }
  );

  const flatList: CategoryResponseDTO[] = data || [];
  const tree: CategoryNode[] = data ? buildCategoryTree(data) : [];

  useEffect(() => {
    const connection = getCategoryHubConnection();

    const onCategoryChanged = () => {
      mutate();
    };

    connection
      .start()
      .then(() => {
        connection.on('CategoryCreated', onCategoryChanged);
        connection.on('CategoryUpdated', onCategoryChanged);
        connection.on('CategoryDeleted', onCategoryChanged);
      })
      .catch(err => {
        console.error('Ошибка подключения к SignalR (CategoriesHub):', err);
      });

    return () => {
      connection.off('CategoryCreated', onCategoryChanged);
      connection.off('CategoryUpdated', onCategoryChanged);
      connection.off('CategoryDeleted', onCategoryChanged);
      connection.stop().catch(e => {
        console.error('Ошибка остановки SignalR (CategoriesHub):', e);
      });
    };
  }, [locale, mutate]);

  return {
    flatList,
    tree,
    isLoading: !error && !data,
    isError: error,
    mutateCategories: mutate,
  };
};

```

```js
import React, { useState, useEffect } from 'react';
import styles from './SideBar.module.css';
import { useCategoriesSWR } from '@/hooks/useCategoriesSWR';
import { CategoryNode } from '@/utils/buildCategoryTree';

const SideBar: React.FC = () => {
  // Храним Set открытых категорий (можно одновременно несколько)
  const [expandedCategories, setExpandedCategories] = useState<Set<string>>(new Set());
  const { tree: categories, isLoading, isError } = useCategoriesSWR();
  const [isClient, setIsClient] = useState(false);

  useEffect(() => {
    setIsClient(true);
  }, []);

  const toggleCategory = (categoryName: string) => {
    setExpandedCategories(prev => {
      const next = new Set(prev);
      if (next.has(categoryName)) {
        next.delete(categoryName);
      } else {
        next.add(categoryName);
      }
      return next;
    });
  };

  if (!isClient) return null;
  if (isLoading) return <div className={styles.message}>Загрузка категорий...</div>;
  if (isError) return <div className={styles.message}>Ошибка загрузки категорий</div>;

  /**
   * Рекурсивный рендер узлов:
   * - Сначала оборачиваем массив nodes в <ul className={styles.categoryList}> (или subcategoryList, если вложенный).
   * - Затем для каждого node создаём <li> с кнопкой и, если есть дети, вложенным <ul className={styles.subcategoryList}>.
   */
  const renderNodes = (nodes: CategoryNode[], isSub = false) => {
    // Если isSub=false, это корневой список: className=categoryList
    // Если isSub=true, это вложенный список: className=subcategoryList
    const ListClass = isSub ? styles.subcategoryList : styles.categoryList;

    return (
      <ul className={ListClass}>
        {nodes.map(node => {
          const isOpen = expandedCategories.has(node.categoryName);
          const hasChildren = node.children && node.children.length > 0;

          return (
            <li
              key={node.categoryName}
              className={`${styles.categoryItem} ${isOpen ? styles.open : ''}`}
            >
              <button
                className={styles.categoryButton}
                onClick={() => {
                  if (hasChildren) toggleCategory(node.categoryName);
                }}
                aria-expanded={hasChildren ? isOpen : undefined}
                aria-controls={hasChildren ? `sub-${node.categoryName}` : undefined}
                disabled={!hasChildren}
              >
                <span className={styles.buttonText}>{node.displayName}</span>
                {hasChildren && <span className={styles.chevron} aria-hidden="true" />}
              </button>

              {hasChildren && (
                <div
                  id={`sub-${node.categoryName}`}
                  className={isOpen ? styles.subcategoryOpenWrapper : styles.subcategoryWrapper}
                >
                  {renderNodes(node.children, true)}
                </div>
              )}
            </li>
          );
        })}
      </ul>
    );
  };

  return (
    <div className={`${styles.sidebar} fadeIn`}>
      <h2 className={styles.sidebarTitle}>Категории</h2>
      {categories.length === 0 ? (
        <p className={styles.empty}>Категорий нет</p>
      ) : (
        renderNodes(categories)
      )}
    </div>
  );
};

export default SideBar;
```

Правила написания кастомных хуков: 
1. **Префикс use**: Название должно начинаться с `use`, чтобы React мог правильно идентифицировать это как хук. 
2. **Логика**: Хук должен содержать логику, которая может быть переиспользована в разных компонентах. Например, получение данных, обработка событий и т.д. 
3. **Возвращаемое значение**: Хук должен возвращать данные или функции, которые будут использоваться в компонентах. Это может быть объект, массив или отдельные значения.

## Zustand

`Zustand` - это библиотека для управления состоянием в React-приложениях. Она позволяет создавать глобальное состояние, которое может быть использовано в разных компонентах без необходимости передавать его через пропсы. Zustand отличается от Redux своей простотой и минималистичным API.

### Пример использования Zustand

```javascript 
import create from 'zustand';
import { devtools } from 'zustand/middleware';

export interface CartItem {
  productId: string;
  quantity: number;
}
export interface CartState {
  items: CartItem[];
    addItem: (item: CartItem) => void;
    removeItem: (productId: string) => void;
    clearCart: () => void;
}

export const useCartStore = create<CartState>()(
  devtools(set => ({
    items: [],
    addItem: item => set(state => {
      const existingItem = state.items.find(i => i.productId === item.productId);
        if (existingItem) {
            return {
                items: state.items.map(i =>
                i.productId === item.productId
                    ? { ...i, quantity: i.quantity + item.quantity }
                    : i
                ),
            };
            }
        return { items: [...state.items, item] };
    }),
    removeItem: productId => set(state => ({
        items: state.items.filter(item => item.productId !== productId),
    })),
    clearCart: () => set({ items: [] }),
    }))
);
```

Код для использования в компоненте:

```javascript
import React from 'react';
import { useCartStore } from '@/stores/cartStore';
import styles from './Cart.module.css';

const Cart: React.FC = () => {
  const { items, addItem, removeItem, clearCart } = useCartStore();

  const handleAddItem = () => {
    const newItem = { productId: '123', quantity: 1 };
    addItem(newItem);
  };

  return (
    <div className={styles.cart}>
      <h2>Корзина</h2>
      <button onClick={handleAddItem}>Добавить товар</button>
      <ul>
        {items.map(item => (
          <li key={item.productId}>
            Товар ID: {item.productId}, Количество: {item.quantity}
            <button onClick={() => removeItem(item.productId)}>Удалить</button>
          </li>
        ))}
      </ul>
      <button onClick={clearCart}>Очистить корзину</button>
    </div>
  );
};

```

Если бы мы писали бы этот код с помощью `React-Redux`, то нам бы пришлось писать `actions`, `reducers`, `store` и т.д. В Zustand все намного проще, мы просто создаем store с помощью функции `create` и передаем туда объект с состоянием и методами для его изменения. Таким образом мы создаем меньше файлов и пишем меньше кода, но это не значит, что Zustand лучше, чем Redux. Просто в некоторых случаях он может быть более удобным и простым в использовании. В первую очередь это касается выбора ведущего программиста в команде. 

## Radix UI 

`Radix UI` - это библиотека компонентов для React, которая предоставляет готовые UI-компоненты с поддержкой доступности и стилизации. Она позволяет быстро создавать интерфейсы, не беспокоясь о низкоуровневых деталях реализации. 

Пример с кодом будет в проекте `Radix-app`

## Zod 

`Zod` - это библиотека для валидации и парсинга данных в JavaScript и TypeScript. Она позволяет создавать схемы валидации, которые можно использовать для проверки входящих данных, например, из форм или API-запросов. Zod поддерживает TypeScript, что позволяет автоматически генерировать типы на основе схем. Предположим у нас есть форма логина и регистрации, где нам нужно валидировать данные пользователя. 

```javascript
import { z } from 'zod';

const passwordRegex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{6,}$/;

export const loginSchema = z.object({
  email: z.string().email('Некорректный email'),
  password: z.string().min(6, 'Пароль должен быть не менее 6 символов').regex(passwordRegex, {
    message: 'Пароль должен содержать как минимум одну заглавную букву, одну строчную букву и одну цифру',
  }),
});


export const registerSchema = z.object({
  email: z.string().email('Некорректный email'),
  password: z.string().min(6, 'Пароль должен быть не менее 6 символов').regex(passwordRegex, {
    message: 'Пароль должен содержать как минимум одну заглавную букву, одну строчную букву и одну цифру',
  }),
  confirmPassword: z.string().min(6, 'Подтверждение пароля должно быть не менее 6 символов'),
}).refine(data => data.password === data.confirmPassword, {
  message: 'Пароли не совпадают',

```

Пример использования в компоненте:

```javascript 
import React, { useState } from 'react';
import { useForm } from 'react-hook-form';
import { zodResolver } from '@hookform/resolvers/zod';
import { loginSchema } from './schemas';

const LoginForm: React.FC = () => {
  const { login, handleSubmit, formState: { errors } } = useForm({
    resolver: zodResolver(loginSchema),
  });

  const onSubmit = (data: any) => {
    console.log('Данные формы:', data);
  };

  return (
    <form onSubmit={handleSubmit(onSubmit)}>
      <div>
        <label htmlFor="email">Email:</label>
        <input id="email" {...login('email')} />
        {errors.email && <span>{errors.email.message}</span>}
      </div>
      <div>
        <label htmlFor="password">Пароль:</label>
        <input type="password" id="password" {...login('password')} />
        {errors.password && <span>{errors.password.message}</span>}
      </div>
      <button type="submit">Войти</button>
    </form>
  );
};

export default LoginForm;
```
Можно такжен написать без `react-hook-form`, но тогда придется писать больше кода для обработки ошибок и валидации. 

```js 

import React, { useState } from 'react';
import { zodResolver } from '@hookform/resolvers/zod';
import { z } from 'zod';
import {loginSchema } from './schemas';

const LoginForm: React.FC = () => {
  const [formData, setFormData] = useState({ email: '', password: '' });
  const [errors, setErrors] = useState({ email: '', password: '' });

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setFormData({ ...formData, [e.target.name]: e.target.value });
  };

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    try {
      loginSchema.parse(formData);
      console.log('Данные формы:', formData);
      setErrors({ email: '', password: '' });
    } catch (err) {
      if (err instanceof z.ZodError) {
        const fieldErrors = err.flatten().fieldErrors;
        setErrors({
          email: fieldErrors.email ? fieldErrors.email[0] : '',
          password: fieldErrors.password ? fieldErrors.password[0] : '',
        });
      }
    }
  };

  return (
    <form onSubmit={handleSubmit}>
      <div>
        <label htmlFor="email">Email:</label>
        <input
          id="email"
          name="email"
          value={formData.email}
          onChange={handleChange}
        />
        {errors.email && <span>{errors.email}</span>}
      </div>
      <div>
        <label htmlFor="password">Пароль:</label>
        <input
          type="password"
          id="password"
          name="password"
          value={formData.password}
          onChange={handleChange}
        />
        {errors.password && <span>{errors.password}</span>}
      </div>
      <button type="submit">Войти</button>
    </form>
  );
};

```

## CommonJS и ESM 

В JavaScripy есть два основных стандарта модулей: CommonJS (CJS) и ECMAScript Modules (ESM). Данное разделение было сделано в следствии того, что в JavaScript изначально не было поддержки модулей, и разработчики использовали различные подходы для организации кода. Чаще всего в Node.js используется CommonJS, а в браузерах - ESM. То есть если вы пишете на React, то вы скорее всего используете ESM, а если пишете `Back-end` на Node.js, то скорее всего CommonJS. Вот пример модуля на CommonJS:

```javascript
// math.mjs
function add(a, b) {
  return a + b;
}

function subtract(a, b) {
  return a - b;
}
module.exports = {
  add,
  subtract,
};
```

для того, чтобы использовать этот модуль в другом файле, нужно сделать следующее:

```javascript
// app.js
const math = require('./math');
console.log(math.add(2, 3)); // 5
console.log(math.subtract(5, 2)); // 3
```

А вот пример модуля на ESM:

```javascript
// math.js
export function add(a, b) {
  return a + b;
}

export function subtract(a, b) {
  return a - b;
}
```
Для того, чтобы использовать этот модуль в другом файле, нужно сделать следующее:

```javascript
// app.js
import { add, subtract } from './math.js';
console.log(add(2, 3)); // 5
console.log(subtract(5, 2)); // 3
```

Одна из основных отличий между CommonJS и ESM заключается в том, что ESM поддерживает асинхронную загрузку модулей, а CommonJS - нет. Это позволяет использовать ESM в браузерах и в Node.js с поддержкой асинхронных операций.

Сейчас и на `Back-end` и на `Front-end` используется ESM, но в Node.js по-прежнему поддерживается CommonJS для обратной совместимости.

## Vite vs Babel

Какие вопросы нам следует задать себе перед тем как узнать разницу между Vite и Babel ?
- Как код будет компилироваться в браузере ?
- Как будет работать сборка проекта ?
- Как происзодит обратная совместимость с разными версиями js написанных библиотек ? 

При обычном создании React-приложения с помощью `create-react-app` используется Babel для транспиляции кода. Babel - это инструмент, который позволяет писать код на современном JavaScript (ES6+) и преобразовывать его в код, который будет работать в старых браузерах. Он также позволяет использовать JSX-синтаксис, который является расширением JavaScript для описания UI-компонентов.

То есть без Babel, у нас не работает JSX, а также библиотеки написанные на ES6+ не будут работать в старых браузерах. `Babel` переводит код в ES5, который поддерживается всеми браузерами. 

Тогда вопрос - Зачем нам нужен Vite и как он работает ?

`Vite` - это современный инструмент для сборки и разработки приложений на JavaScript. Он использует нативные возможности браузера для быстрой разработки и сборки приложений. Vite поддерживает ES-модули, что позволяет использовать современные возможности JavaScript без необходимости транспиляции кода. То есть он уже не транспилирует код в ES5, а использует нативные возможности браузера для работы с ES-модулями. Тем самый, процесс работы нашего кода становится быстрее и проще. Vite также поддерживает горячую перезагрузку (HMR), что позволяет быстро видеть изменения в коде без полной перезагрузки страницы.

