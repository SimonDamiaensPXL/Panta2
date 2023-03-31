import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../core/auth/auth.service';
import { StorageService } from '../core/services/storage/storage.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.sass']
})
export class LoginComponent implements OnInit {
  form: any = {
    username: null,
    password: null,
  };
  isLoggingIn = false;
  isLoginFailed = false;
  errorMessage = '';

  constructor(private authService: AuthService, private storageService: StorageService, private router: Router) { }


  ngOnInit(): void {
    if (this.storageService.isLoggedIn()) {
      this.router.navigate(["/home"])
    }
  }

  onSubmit(): void {
    const { username, password } = this.form
    this.isLoggingIn = true;
    this.authService.login(username, password).subscribe({
      next: data => {
        this.storageService.saveToken(data.token);
        this.storageService.saveId(data.id)
        this.isLoginFailed = false;
        this.router.navigate(["/home"])
      },
      error: err => {
        if (err.status == 401) {
          this.isLoggingIn = false;
          console.log(err);
          this.errorMessage = "Username or Password is not correct."
        }
        else {
          console.log(err.message);
          this.isLoggingIn = false;
          this.errorMessage = "Something went wrong."
        }
        this.isLoginFailed = true;
      }
    });
  }
}
