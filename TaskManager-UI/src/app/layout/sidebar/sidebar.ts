import { Component, inject, signal } from '@angular/core';
import { AuthService } from '../../core/services/auth.service';
import { NavigationEnd, Router, RouterModule } from '@angular/router';
import { filter } from 'rxjs';

@Component({
  selector: 'app-sidebar',
  standalone: true,
  imports: [RouterModule],
  templateUrl: './sidebar.html',
  styleUrl: './sidebar.scss'
})
export class Sidebar {
  private router = inject(Router);
  private authService = inject(AuthService);
  currentUrl = signal(this.router.url);
  isAuthenticated$ = this.authService.isLoggedIn$;

  constructor() {
    this.router.events
      .pipe(filter(event => event instanceof NavigationEnd))
      .subscribe((event: NavigationEnd) => {
        this.currentUrl.set(event.urlAfterRedirects);
      });
  }

  onLogout() {
    this.authService.logout();
    this.router.navigate(['/login']);
  }
}
