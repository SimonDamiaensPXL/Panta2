import { Component, OnInit } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { ActivatedRoute, Router } from '@angular/router';
import { firstValueFrom } from 'rxjs';
import { CompanyService } from 'src/app/core/services/company/company.service';

@Component({
  selector: 'app-edit-company',
  templateUrl: './edit-company.component.html',
})
export class EditCompanyComponent implements OnInit {
  form: any = {
    company_name: null,
    company_logo: null,
  };
  image?: any;
  isUploading: boolean = false;
  isUploadFailed: boolean = false;
  errorMessage: string = '';

  constructor(private companyService: CompanyService, private route: ActivatedRoute, private router: Router) { }
  
  ngOnInit(): void {
    this.getCompany();
  }

  async getCompany(): Promise<void> {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    
    const company = await firstValueFrom(this.companyService.getCompanyById(id));

    this.form.company_name = company.name;
    this.image = company.logo;
  }

  onSubmit(): void {
    this.isUploading = true;
    this.companyService.editCompany(this.form.company_name, this.form.company_logo).subscribe({
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
