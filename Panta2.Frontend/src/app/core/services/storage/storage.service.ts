import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { UserService } from '../user/user.service';

const USER_KEY = 'auth-user';
const USER_ID = 'user-id';
const USER = 'user';

@Injectable({
  providedIn: 'root'
})
export class StorageService {
  constructor(private userService: UserService) {}
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

  public getUser(): void {
    this.user = localStorage.getItem(USER)

    if (this.user == null) {
      const id = window.sessionStorage.getItem(USER_ID);

      this.userService.getUserById(id).subscribe({
        next: data => {
          console.log(data);
          this.user = data;
          localStorage.setItem('user', this.user);
        },
        error: err => {
          console.log(err.message)
        }
      });
    }
    return JSON.parse(this.user);
  }

  public deleteUserStorage(): void {
    window.sessionStorage.removeItem(USER_KEY);
    window.sessionStorage.removeItem(USER_ID);
    localStorage.removeItem(USER);
  }

  public isLoggedIn(): boolean {
    const user = window.sessionStorage.getItem(USER_KEY);
    if (user) {
      return true;
    }

    return false;
  }
}