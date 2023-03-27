import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { firstValueFrom } from 'rxjs';
import { User } from '../core/models/user.model';
import { StorageService } from '../core/services/storage/storage.service';
import { UserService } from '../core/services/user/user.service';

@Component({
  selector: 'app-layout-navbar',
  templateUrl: './navbar.component.html'
})
export class NavbarComponent implements OnInit {
  userName?: string;
  companyLogo?: string;

  constructor(private storageService: StorageService ,private userService: UserService, private router: Router) {}

  async ngOnInit(): Promise<void> {
    const user: User = await this.storageService.getUser();

    this.userName = user.firstName;
    this.companyLogo = await firstValueFrom(this.userService.getUserCompanyById(user.companyId));
  }
  
  logout(): void {
    this.storageService.deleteUserStorage();
  }

  goToSettings(): void {
    this.router.navigate(["/settings"]);
  }

  goToHome(): void {
    this.router.navigate(["/home"]);
  }
}