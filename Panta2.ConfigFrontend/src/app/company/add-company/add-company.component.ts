import { Component } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';

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

  constructor(private sanitizer: DomSanitizer) {}

  onSubmit(): void {
    console.log("Adding company:"); 
  }

  onFileDropped(event: any) {
    if (event) {
      var reader = new FileReader();

      reader.readAsDataURL(event); // read file as data url

      reader.onload = (event: any) => { // called once readAsDataURL is completed
        this.image = event.target.result;
      }
    }    
  }

  fileBrowseHandler(event: any) {
    //let file: File = event.target.files[0];
    if (event.target.files && event.target.files[0]) {
      var reader = new FileReader();

      reader.readAsDataURL(event.target.files[0]); // read file as data url

      reader.onload = (event: any) => { // called once readAsDataURL is completed
        this.image = event.target.result;
        console.log(this.image);
      }
    }    
  }
  
  resetPreview(): void {
    window.location.reload();
  }
}
