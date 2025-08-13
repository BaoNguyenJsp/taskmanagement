import { Component } from '@angular/core';
import { AuthService } from '../../core/services/auth.service';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { BehaviorSubject } from 'rxjs';

@Component({
    selector: 'app-navbar',
    standalone: true,
    imports: [CommonModule],
    templateUrl: './navbar.html',
    styleUrl: './navbar.scss'
})
export class Navbar {
    isAuthenticated$= new BehaviorSubject<boolean>(false);;

    constructor(private authService: AuthService, private router: Router) {
        this.isAuthenticated$ = this.authService.isLoggedIn$
    }

    onLogout() {
        this.authService.logout();
        this.router.navigate(['/login']);
    }
}
