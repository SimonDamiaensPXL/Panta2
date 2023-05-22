import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { firstValueFrom } from 'rxjs';
import { CompanyServiceCreation } from 'src/app/core/models/create-company-service-model';
import { RoleCreation } from 'src/app/core/models/create-role.model';
import { CompanyService } from 'src/app/core/services/company/company.service';
import { UserService } from 'src/app/core/services/user/user.service';

@Component({
  selector: 'app-add-company-service',
  templateUrl: './add-company-service.component.html'
})
export class AddCompanyServiceComponent {
  companyId: number = 0;
  form: any = {
    services: []
  };
  companyName?: string;
  companyUrl?: string;
  services: any[] = [];

  selectedIds: any[] = [];
  
  isUploading: boolean = false;
  isUploadFailed: boolean = false;
  errorMessage: string = '';
  isSubmitted = false;

  constructor(private companyService: CompanyService, private route: ActivatedRoute, private router: Router) { }

  async ngOnInit(): Promise<void> {
    this.companyId = Number(this.route.snapshot.paramMap.get('id'));

    this.services = await firstValueFrom(this.companyService.getNotInCompanyServiceNames(this.companyId));

    const company = await firstValueFrom(this.companyService.getCompanyById(this.companyId));
    this.companyName = company.name;
    this.companyUrl = `/company/${company.id}`
  }

  onSubmit(f: NgForm): void {
    this.isUploading = true;
    const newCompanyServices = f.value as CompanyServiceCreation;

    newCompanyServices.services = this.selectedIds;
    newCompanyServices.companyId = this.companyId;

    this.companyService.addCompanyServices(newCompanyServices).subscribe({
      next: data => {
        this.isUploadFailed = false;
        this.isUploading = false;
        this.router.navigate(['/company/' + this.companyId]);
      },
      error: err => {
        this.isUploading = false;
        this.errorMessage = err.error?.message || "Something went wrong! Please try again.";
        console.log(this.errorMessage);
        this.isUploadFailed = true;
      }
    });
  }

  OnCheckboxSelect(id: any, event: any): void {
    if (event.target.checked === true) {
      this.selectedIds.push(id);
    }
    if (event.target.checked === false) {
      this.selectedIds = this.selectedIds.filter((item) => item !== id);
      console.log('Selected Ids ', this.selectedIds);
    }
  }
}
