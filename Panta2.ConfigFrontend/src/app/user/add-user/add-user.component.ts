import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { firstValueFrom } from 'rxjs';
import { UserCreation } from 'src/app/core/models/create-user.model';
import { CompanyService } from 'src/app/core/services/company/company.service';
import { UserService } from 'src/app/core/services/user/user.service';

@Component({
  selector: 'app-add-user',
  templateUrl: './add-user.component.html',
})
export class AddUserComponent implements OnInit {
  companyId: number = 0;
  form: any = {
    name: null,
  };
  companyName?: string;
  companyUrl?: string;
  roles: any[] = [];
  isUploading: boolean = false;
  isUploadFailed: boolean = false;
  errorMessage: string = '';
  isSubmitted = false;

  constructor(private companyService: CompanyService, private userService: UserService, private route: ActivatedRoute, private router: Router) { }

  async ngOnInit(): Promise<void> {
    this.companyId = Number(this.route.snapshot.paramMap.get('id'));

    this.roles = await firstValueFrom(this.companyService.getCompanyRoles(this.companyId));

    const company = await firstValueFrom(this.companyService.getCompanyById(this.companyId));
    this.companyName = company.name;
    this.companyUrl = `/company/${company.id}`
  }

  onSubmit(f: NgForm): void {
    this.isUploading = true;

    const newUser = f.value as UserCreation;
    newUser.companyId = this.companyId;

    this.userService.createUser(newUser).subscribe({
      next: data => {
        this.isUploadFailed = false;
        this.isUploading = false;
        this.router.navigate(['/company/' + newUser.companyId]);
      },
      error: err => {
        this.isUploading = false;
        console.log(err);
        this.errorMessage = err?.error || "Something went wrong! Please try again.";
        this.isUploadFailed = true;
      }
    });
  }
}
