import { Routes } from '@angular/router';
import { Layout } from './layout/layout';
import { Login } from './features/auth/login/login';
import { authGuard } from './core/guards/auth.guard';

export const routes: Routes = [
    {
        path: '',
        component: Layout, // Layout wraps all child routes
        children: [
            {
                path: 'users',
                loadChildren: () => import('./features/users/users.module').then(m => m.UsersModule),
                canActivate: [authGuard]
            },
            {
                path: 'products',
                loadChildren: () => import('./features/products/products.module').then(m => m.ProductsModule),
                canActivate: [authGuard]
            },
            { path: 'login', component: Login },
            { path: '', redirectTo: '/login', pathMatch: 'full' }
        ]
    }
];