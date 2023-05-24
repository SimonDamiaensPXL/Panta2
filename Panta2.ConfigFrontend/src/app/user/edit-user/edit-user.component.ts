import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { firstValueFrom } from 'rxjs';
import { CompanyService } from 'src/app/core/services/company/company.service';
import { UserService } from 'src/app/core/services/user/user.service';

@Component({
  selector: 'app-edit-user',
  templateUrl: './edit-user.component.html'
})
export class EditUserComponent {
  userId: number = 0;
  companyId: number = 0;
  companyName?: string;
  companyUrl?: string;

  form: any = {
    username: null,
    firstname: null,
    lastname: null,
    email: null,
    password: null,
    confirmPassword: null,
  };

  image?: any;
  isUploading: boolean = false;
  isUploadFailed: boolean = false;
  errorMessage: string = '';

  constructor(private userService: UserService, private companyService: CompanyService, private route: ActivatedRoute, private router: Router) { }
  
  ngOnInit(): void {
    this.getCompanyUser();

  }

  async getCompanyUser(): Promise<void> {
    this.companyId = Number(this.route.snapshot.paramMap.get('id'));
    this.userId = Number(this.route.snapshot.paramMap.get('user-id'));

    const user = await firstValueFrom(this.userService.getUserById(this.userId));
    const company = await firstValueFrom(this.companyService.getCompanyById(this.companyId));

    this.form.username = user.userName;
    this.form.firstname = user.firstName;
    this.form.lastname = user.lastName;
    this.form.email = user.email;

    this.companyName = company.name;
    this.companyUrl = `/company/${company.id}`
  }

  onUsernameSubmit(): void {
    this.isUploading = true;
    this.userService.editUserName(this.userId, this.form.username).subscribe({
      next: data => {
        this.isUploadFailed = false;
        this.isUploading = false;
      },
      error: err => {
        this.isUploading = false;
        console.log(err);
        this.errorMessage = "Something went wrong! Please try again.";
        this.isUploadFailed = true;
      }
    });
  }

  onNameSubmit(): void {
    this.isUploading = true;
    this.userService.editName(this.userId, this.form.firstname, this.form.lastname).subscribe({
      next: data => {
        this.isUploadFailed = false;
        this.isUploading = false;
      },
      error: err => {
        this.isUploading = false;
        console.log(err);
        this.errorMessage = "Something went wrong! Please try again.";
        this.isUploadFailed = true;
      }
    });
  }

  onEmailSubmit(): void {
    this.isUploading = true;
    this.userService.editEmail(this.userId, this.form.email).subscribe({
      next: data => {
        this.isUploadFailed = false;
        this.isUploading = false;
      },
      error: err => {
        this.isUploading = false;
        console.log(err);
        this.errorMessage = "Something went wrong! Please try again.";
        this.isUploadFailed = true;
      }
    });
  }

  onPasswordSubmit(): void {
    this.isUploading = true;
    this.userService.editPassword(this.userId, this.form.password, this.form.confirmPassword).subscribe({
      next: data => {
        this.isUploadFailed = false;
        this.isUploading = false;
      },
      error: err => {
        this.isUploading = false;
        console.log(err);
        this.errorMessage = "Something went wrong! Please try again.";
        this.isUploadFailed = true;
      }
    });
  }
}
