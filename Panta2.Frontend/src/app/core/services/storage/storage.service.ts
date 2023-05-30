import { Injectable } from '@angular/core';
import { UserService } from '../user/user.service';
import { User } from '../../models/user.model';
import { firstValueFrom } from 'rxjs';
import { Router } from '@angular/router';

const USER_KEY = 'auth-user';
const USER_ID = 'user-id';
const USER = 'user';

@Injectable({
  providedIn: 'root'
})
export class StorageService {
  constructor(private userService: UserService, private router: Router) {}
  user?: any;

  clean(): void {
    window.sessionStorage.clear();
  }

  public saveToken(token: any): void {
    window.sessionStorage.removeItem(USER_KEY);
    window.sessionStorage.setItem(USER_KEY, JSON.stringify(token));
  }

  public saveId(id: any): void {
    window.sessionStorage.removeItem(USER_ID);
    window.sessionStorage.setItem(USER_ID, JSON.stringify(id));
  }

  public async getUser(): Promise<any> {
    const userString = window.sessionStorage.getItem('USER');
    const user: User = userString ? JSON.parse(userString) : null;

    if (user == null) {
      const id = window.sessionStorage.getItem(USER_ID);
      try {
        const userData = await firstValueFrom(this.userService.getUserById(id));
        const user = userData;
        window.sessionStorage.setItem(USER, JSON.stringify(user));
        return user;
      } catch (err) {
        console.error(err);
        return null;
      }
    } else {
      return user;
    }
  }

  public deleteUserStorage(): void {
    window.sessionStorage.removeItem(USER_KEY);
    window.sessionStorage.removeItem(USER_ID);
    window.sessionStorage.removeItem(USER);
    this.router.navigate(['/login']);
  }

  public isLoggedIn(): boolean {
    const user = window.sessionStorage.getItem(USER_KEY);
    if (user) {
      return true;
    }

    return false;
  }
}