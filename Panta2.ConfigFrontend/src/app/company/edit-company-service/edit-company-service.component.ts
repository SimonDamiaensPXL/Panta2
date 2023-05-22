import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { firstValueFrom } from 'rxjs';
import { CompanyService } from 'src/app/core/services/company/company.service';

@Component({
  selector: 'app-edit-service',
  templateUrl: './edit-company-service.component.html'
})
export class EditCompanyServiceComponent {
  serviceId: number = 0;
  companyId: number = 0;
  companyName?: string;
  companyUrl?: string;

  form: any = {
    service_name: null,
    service_icon: null
  };

  image?: any;
  isUploading: boolean = false;
  isUploadFailed: boolean = false;
  errorMessage: string = '';

  constructor(private companyService: CompanyService, private route: ActivatedRoute, private router: Router) { }

  async ngOnInit(): Promise<void> {
    this.getCompanyService();
  }

  async getCompanyService(): Promise<void> {
    this.companyId = Number(this.route.snapshot.paramMap.get('id'));
    this.serviceId = Number(this.route.snapshot.paramMap.get('service-id'));

    const service = await firstValueFrom(this.companyService.getCompanyService(this.companyId, this.serviceId));
    const company = await firstValueFrom(this.companyService.getCompanyById(this.companyId));

    this.companyName = company.name;
    this.companyUrl = `/company/${company.id}`
    this.form.service_name = service.name;
    this.form.service_link = service.link;
    this.image = service.icon;
  }

  onNameSubmit(): void {
    this.isUploading = true;
    this.companyService.editServiceName(this.companyId, this.serviceId, this.form.service_name).subscribe({
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

  onIconSubmit(): void {
    if (this.form.service_icon == null) {
      this.errorMessage = "Provide a new icon!";
      this.isUploadFailed = true;
    }
    else {
      this.isUploading = true;
      this.companyService.editServiceIcon(this.companyId, this.serviceId, this.form.service_name, this.form.service_icon).subscribe({
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

  onFileDropped(event: any) {
    if (event) {
      var reader = new FileReader();

      reader.readAsDataURL(event);

      reader.onload = (event: any) => {
        this.image = event.target.result;
        this.form.service_icon = event.target.result;
      }
    }
  }

  fileBrowseHandler(event: any) {
    if (event.target.files && event.target.files[0]) {
      var reader = new FileReader();

      reader.readAsDataURL(event.target.files[0]);

      reader.onload = (event: any) => {
        this.image = event.target.result;
        this.form.service_icon = event.target.result;
      }
    }
  }

  resetPreview(): void {
    this.image = null;
    this.form.service_logo = null;
    this.errorMessage = "";
  }
}
