import { Component } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { Router } from '@angular/router';
import { ServiceService } from 'src/app/core/services/service/service.service';

@Component({
  selector: 'app-add-service',
  templateUrl: './add-service.component.html'
})
export class AddServiceComponent {
  form: any = {
    service_name: null,
    service_link: null,
    service_icon: null
  };
  image?: any;
  isUploading: boolean = false;
  isUploadFailed: boolean = false;
  errorMessage: string = '';


  constructor(private serviceService: ServiceService ,private sanitizer: DomSanitizer, private router: Router) { }

  onSubmit(): void {
    this.isUploading = true;
    this.serviceService.createService(this.form.service_name, this.form.service_link, this.form.service_icon).subscribe({
      next: data => {
        this.isUploadFailed = false;
        this.router.navigate(['/services']);
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
    this.form.service_icon = null;
    this.errorMessage = "";
  }
}
