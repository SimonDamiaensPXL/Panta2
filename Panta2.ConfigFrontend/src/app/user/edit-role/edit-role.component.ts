import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { firstValueFrom } from 'rxjs';
import { Role } from 'src/app/core/models/role.model';
import { RoleUpdate } from 'src/app/core/models/update-role.model';
import { CompanyService } from 'src/app/core/services/company/company.service';
import { UserService } from 'src/app/core/services/user/user.service';

@Component({
  selector: 'app-edit-role',
  templateUrl: './edit-role.component.html'
})
export class EditRoleComponent {
  roleId: number = 0;
  companyId: number = 0;
  companyName?: string;
  companyUrl?: string;
  services?: any[] = [];

  form: any = {
    name: null,
    services: null
  };

  selectedIds: any[] = [];

  image?: any;
  showPopup: boolean = false;
  isDeleting: boolean = false;
  isUploading: boolean = false;
  isUploadFailed: boolean = false;
  errorMessage: string = '';
  deletingErrorMessage: string = '';

  constructor(private companyService: CompanyService, private route: ActivatedRoute, private router: Router) { }
  
  ngOnInit(): void {
    this.getCompanyRole();
  }

  async getCompanyRole(): Promise<void> {
    this.companyId = Number(this.route.snapshot.paramMap.get('id'));
    this.roleId = Number(this.route.snapshot.paramMap.get('role-id'));

    this.services = await firstValueFrom(this.companyService.getCompanyServiceWithIsInRole(this.companyId, this.roleId));

    if (this.services) {
      this.selectedIds = this.services.filter(s => s.isInRole).map(s => s.id);
    }

    const company = await firstValueFrom(this.companyService.getCompanyById(this.companyId));
    const role = await firstValueFrom(this.companyService.getRoleById(this.roleId));

    this.form.name = role.name;
    this.companyName = company.name;
    this.companyUrl = `/company/${company.id}`
  }

  onNameSubmit(): void {
    this.isUploading = true;

    this.companyService.editRoleName(this.roleId, this.form.name).subscribe({
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

    this.companyService.editRoleServices(this.roleId, this.selectedIds).subscribe({
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

  onUserDelete(): void {
    this.isDeleting = true;

    this.companyService.deleteRole(this.roleId).subscribe({
      next: data => {
        this.isDeleting = false;
        this.showPopup = false;
        this.router.navigate([this.companyUrl]);
      },
      error: err => {
        console.log(err);
        this.deletingErrorMessage = err.error?.message || "Deleting user went wrong! Please try again.";
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

  OnCheckboxSelect(id: any, event: any): void {
    if (event.target.checked === true) {
      this.selectedIds.push(id);
    }
    if (event.target.checked === false) {
      this.selectedIds = this.selectedIds.filter((item) => item !== id);
    }
  }
}
