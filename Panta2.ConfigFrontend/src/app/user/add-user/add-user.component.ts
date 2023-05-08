import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { firstValueFrom } from 'rxjs';
import { Company } from 'src/app/core/models/company.model';
import { UserCreation } from 'src/app/core/models/create-user.model';
import { CompanyService } from 'src/app/core/services/company/company.service';
import { UserService } from 'src/app/core/services/user/user.service';

@Component({
  selector: 'app-add-user',
  templateUrl: './add-user.component.html',
})
export class AddUserComponent implements OnInit {
  form: any = {
    username: null,
    firstname: null,
    lastname: null
  };
  companies: Company[] = [];
  isUploading: boolean = false;
  isUploadFailed: boolean = false;
  errorMessage: string = '';
  isSubmitted = false;


  constructor(private companyService: CompanyService, private userService: UserService, private router: Router) { }
  async ngOnInit(): Promise<void> {
    this.companies = await firstValueFrom(this.companyService.getCompanies());
  }

  onSubmit(f: NgForm): void {
    this.isUploading = true;

    const newUser = f.value as UserCreation;

    console.log(newUser);
    console.log(newUser.companyId);

    this.userService.createUser(newUser).subscribe({
      next: data => {
        this.isUploadFailed = false;
        this.router.navigate(['/users']);
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
