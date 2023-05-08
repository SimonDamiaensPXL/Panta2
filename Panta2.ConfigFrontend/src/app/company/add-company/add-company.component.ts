import { Component } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { Router } from '@angular/router';
import { CompanyService } from 'src/app/core/services/company/company.service';

@Component({
  selector: 'app-add-company',
  templateUrl: './add-company.component.html',
})
export class AddCompanyComponent {
  form: any = {
    company_name: null,
    company_logo: null,
  };
  image?: any;
  isUploading: boolean = false;
  isUploadFailed: boolean = false;
  errorMessage: string = '';

  constructor(private companyService: CompanyService, private sanitizer: DomSanitizer, private router: Router) { }

  onSubmit(): void {
    this.isUploading = true;
    this.companyService.createCompany(this.form.company_name, this.form.company_logo).subscribe({
      next: data => {
        this.isUploadFailed = false;
        this.router.navigate(['/companies']);
      },
      error: err => {
        this.isUploading = false;
        console.log(err);
        this.errorMessage = "Something went wrong! Please try again.";
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
}
