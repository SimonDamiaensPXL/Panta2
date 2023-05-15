import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { firstValueFrom } from 'rxjs';
import { RoleCreation } from 'src/app/core/models/create-role.model';
import { Service } from 'src/app/core/models/service.model';
import { CompanyService } from 'src/app/core/services/company/company.service';
import { ServiceService } from 'src/app/core/services/service/service.service';
import { UserService } from 'src/app/core/services/user/user.service';

@Component({
  selector: 'app-add-role',
  templateUrl: './add-role.component.html'
})
export class AddRoleComponent {
  companyId: number = 0;
  form: any = {
    name: null,
    services: []
  };
  companyName?: string;
  companyUrl?: string;
  services: Service[] = [];

  selectedIds: any[] = [];
  
  isUploading: boolean = false;
  isUploadFailed: boolean = false;
  errorMessage: string = '';
  isSubmitted = false;

  constructor(private serviceService: ServiceService, private companyService: CompanyService, private userService: UserService, private route: ActivatedRoute, private router: Router) { }

  async ngOnInit(): Promise<void> {
    this.companyId = Number(this.route.snapshot.paramMap.get('id'));

    this.services = await firstValueFrom(this.serviceService.getServices());

    const company = await firstValueFrom(this.companyService.getCompanyById(this.companyId));
    this.companyName = company.name;
    this.companyUrl = `/company/${company.id}`
  }

  onSubmit(f: NgForm): void {
    this.isUploading = true;
    const newRole = f.value as RoleCreation;

    newRole.services = this.selectedIds;

    this.companyService.createRole(newRole, this.companyId).subscribe({
      next: data => {
        this.isUploadFailed = false;
        this.isUploading = false;
        this.router.navigate(['/company/' + this.companyId]);
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

  OnCheckboxSelect(id: any, event: any): void {
    if (event.target.checked === true) {
      this.selectedIds.push(id);
      console.log('Selected Ids ', this.selectedIds);
    }
  }
}
