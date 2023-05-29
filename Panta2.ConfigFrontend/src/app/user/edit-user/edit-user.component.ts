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
  roles?: any[] = [];
  userRole: any;

  form: any = {
    username: null,
    firstname: null,
    lastname: null,
    email: null,
    password: null,
    confirmPassword: null,
    roleId: null
  };

  image?: any;
  showPopup: boolean = false;
  isDeleting: boolean = false;
  isUploading: boolean = false;
  isUploadFailed: boolean = false;
  errorMessage: string = '';
  deletingErrorMessage = '';

  constructor(private userService: UserService, private companyService: CompanyService, private route: ActivatedRoute, private router: Router) { }
  
  ngOnInit(): void {
    this.getCompanyUser();
  }

  async getCompanyUser(): Promise<void> {
    this.companyId = Number(this.route.snapshot.paramMap.get('id'));
    this.userId = Number(this.route.snapshot.paramMap.get('user-id'));

    const user = await firstValueFrom(this.userService.getUserById(this.userId));
    const company = await firstValueFrom(this.companyService.getCompanyById(this.companyId));
    
    this.roles = await firstValueFrom(this.companyService.getCompanyRoles(this.companyId));
    this.userRole = await firstValueFrom(this.userService.getUserRole(this.userId));

    this.form.username = user.userName;
    this.form.firstname = user.firstName;
    this.form.lastname = user.lastName;
    this.form.email = user.email;
    this.form.roleId = this.userRole.id;

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

  onRoleSubmit(): void {
    this.isUploading = true;

    if (this.userRole.id == this.form.roleId) {
      this.isUploading = false;
      this.isUploadFailed = true;
      this.errorMessage = "Role has not been changed. Please choose a different role.";
    } else {
      this.userService.editRole(this.userId, this.userRole.id, this.form.roleId).subscribe({
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

  onUserDelete(): void {
    this.isDeleting = true;

    this.userService.deleteUser(this.userId).subscribe({
      next: data => {
        this.isDeleting = false;
        this.showPopup = false;
        this.router.navigate([this.companyUrl]);
      },
      error: err => {
        console.log(err);
        this.deletingErrorMessage = "Deleting user went wrong! Please try again.";
        this.isUploadFailed = true;
        this.isDeleting = false;
        this.showPopup = false;
      }
    });
  }

  onShowPopup(): void {
    this.showPopup = true;
  }

  onHidePopup(): void {
    this.showPopup = false;
  }

  OnRadioSelect(id: any, event: any): void {
    if (event.target.checked === true) {
      this.form.roleId = id;
    }
  }
}
