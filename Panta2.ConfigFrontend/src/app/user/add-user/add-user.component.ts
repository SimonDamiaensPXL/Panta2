import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from 'src/app/core/services/user/user.service';

@Component({
  selector: 'app-add-user',
  templateUrl: './add-user.component.html',
})
export class AddUserComponent {
  form: any = {
    username: null,
    firstname: null,
    lastname: null
  };
  isUploading: boolean = false;
  isUploadFailed: boolean = false;
  errorMessage: string = '';

  constructor(private userService: UserService, private router: Router) { }

  onSubmit(): void {
    this.isUploading = true;
    this.userService.createUser(this.form.username, this.form.firstname, this.form.lastname).subscribe({
      next: data => {
        this.isUploadFailed = false;
        this.router.navigate(['/services']);
      },
      error: err => {
        this.isUploading = false;
        console.log(err);
        this.errorMessage = "Something went wrong! Please try again.";
        console.log(this.errorMessage);
        this.isUploadFailed = true;
      }
    });
  }
}
