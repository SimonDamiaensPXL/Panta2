import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { firstValueFrom } from 'rxjs';
import { ServiceService } from 'src/app/core/services/service/service.service';

@Component({
  selector: 'app-edit-service',
  templateUrl: './edit-service.component.html'
})
export class EditServiceComponent {
  serviceId: number = 0;

  form: any = {
    service_name: null,
    service_link: null,
    service_icon: null
  };
  
  image?: any;
  showPopup: boolean = false;
  isDeleting: boolean = false;
  isUploading: boolean = false;
  isUploadFailed: boolean = false;
  errorMessage: string = '';
  deletingErrorMessage: string = '';

  constructor(private serviceService: ServiceService, private route: ActivatedRoute, private router: Router) { }

  ngOnInit(): void {
    this.getService();

  }

  async getService(): Promise<void> {
    this.serviceId = Number(this.route.snapshot.paramMap.get('id'));

    const service = await firstValueFrom(this.serviceService.getServiceById(this.serviceId));

    this.form.service_name = service.name;
    this.form.service_link = service.link;
    this.image = service.icon;
  }

  onNameSubmit(): void {
    this.isUploading = true;
    this.serviceService.editServiceName(this.serviceId, this.form.service_name).subscribe({
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

  onLinkSubmit(): void {
    this.isUploading = true;
    this.serviceService.editServiceLink(this.serviceId, this.form.service_link).subscribe({
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
      this.serviceService.editServiceIcon(this.serviceId, this.form.service_name, this.form.service_icon).subscribe({
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

  
  onCompanyServiceDelete(): void {
    this.isDeleting = true;

    this.serviceService.deleteService(this.serviceId).subscribe({
      next: data => {
        this.isDeleting = false;
        this.showPopup = false;
        this.router.navigate(['/services/']);
      },
      error: err => {
        console.log(err);
        this.deletingErrorMessage = err.error?.message || "Deleting service went wrong! Please try again.";
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
}
