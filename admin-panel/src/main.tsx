import { createRoot } from 'react-dom/client'
import './index.css'
import './i18n.ts';
import { BrowserRouter } from 'react-router-dom';
import AppRoutes from './AppRoutes.tsx'



createRoot(document.getElementById('root')!).render(
    <BrowserRouter>
        <AppRoutes />
    </BrowserRouter>
)
