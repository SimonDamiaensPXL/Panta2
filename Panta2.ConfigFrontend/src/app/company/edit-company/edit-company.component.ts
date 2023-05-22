import { Component, OnInit } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { ActivatedRoute, Router } from '@angular/router';
import { firstValueFrom } from 'rxjs';
import { Company } from 'src/app/core/models/company.model';
import { Service } from 'src/app/core/models/service.model';
import { User } from 'src/app/core/models/user.model';
import { CompanyService } from 'src/app/core/services/company/company.service';

@Component({
  selector: 'app-edit-company',
  templateUrl: './edit-company.component.html',
})
export class EditCompanyComponent implements OnInit {
  companyId: number = 0;
  form: any = {
    company_name: null,
    company_logo: null,
  };
  users: User[] = [];
  roles: any[] = [];
  services: Service[] = [];
  image?: any;
  isUploading: boolean = false;
  isUploadFailed: boolean = false;
  errorMessage: string = '';

  constructor(private companyService: CompanyService, private route: ActivatedRoute, private router: Router) { }
  
  async ngOnInit(): Promise<void> {
    this.companyId = Number(this.route.snapshot.paramMap.get('id'));

    this.users = await firstValueFrom(this.companyService.getCompanyUsers(this.companyId));

    this.roles = await firstValueFrom(this.companyService.getCompanyRoles(this.companyId));

    this.services = await firstValueFrom(this.companyService.getCompanyServices(this.companyId));

    await this.getCompany();
  }
  
  async getCompany(): Promise<void> {
    const company = await firstValueFrom(this.companyService.getCompanyById(this.companyId));

    this.form.company_name = company.name;
    this.image = company.logo;
  }

  onNameSubmit(): void {
    console.log("Name submitted");
    this.isUploading = true;
    this.companyService.editCompanyName(this.companyId, this.form.company_name).subscribe({
      next: data => {
        this.isUploadFailed = false;
        this.isUploading = false;
        //this.router.navigate(['/companies']);
      },
      error: err => {
        this.isUploading = false;
        console.log(err);
        this.errorMessage = "Something went wrong! Please try again.";
        this.isUploadFailed = true;
      }
    });
  }

  onLogoSubmit(): void {
    console.log("Logo submitted");

    if (this.form.company_logo == null) {
      this.errorMessage = "Provide a new logo!";
      this.isUploadFailed = true;
    }
    else{
      this.isUploading = true;
      this.companyService.editCompanyLogo(this.companyId, this.form.company_name, this.form.company_logo).subscribe({
        next: data => {
          this.isUploadFailed = false;
          this.isUploading = false;
          //this.router.navigate(['/companies']);
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

  onFileDropped(event: any) {
    if (event) {
      var reader = new FileReader();

      reader.readAsDataURL(event);

      reader.onload = (event: any) => {
        this.image = event.target.result;
        this.form.company_logo = event.target.result;
      }
    }
  }

  fileBrowseHandler(event: any) {
    if (event.target.files && event.target.files[0]) {
      var reader = new FileReader();

      reader.readAsDataURL(event.target.files[0]);

      reader.onload = (event: any) => {
        this.image = event.target.result;
        this.form.company_logo = event.target.result;
      }
    }
  }

  resetPreview(): void {
    this.image = null;
    this.form.company_logo = null;
    this.errorMessage = "";
  }

  goToAddUser(): void {
    this.router.navigate([`/company/${this.companyId}/add-user`]);
  }

  goToAddRole(): void {
    this.router.navigate([`/company/${this.companyId}/add-role`]);
  }

  goToAddService(): void {
    this.router.navigate([`/company/${this.companyId}/add-service`]);
  }
}
