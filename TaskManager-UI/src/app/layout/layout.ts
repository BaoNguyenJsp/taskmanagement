import { Component, inject } from '@angular/core';
import { Sidebar } from './sidebar/sidebar';
import { Navbar } from './navbar/navbar';
import { RouterModule } from '@angular/router';
import { AuthService } from '../core/services/auth.service';
import { BehaviorSubject } from 'rxjs';
import { CommonModule } from '@angular/common';
import { LoadingService } from '../core/services/loading.service';

@Component({
  selector: 'app-layout',
  standalone: true,
  imports: [Navbar, Sidebar, RouterModule, CommonModule], // 👈 required
  templateUrl: './layout.html',
  styleUrl: './layout.scss'
})
export class Layout {
  loadingService = inject(LoadingService);
  authService = inject(AuthService);

  isAuthenticated$ = this.authService.isLoggedIn$;
}