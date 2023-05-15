import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { firstValueFrom } from 'rxjs';
import { UserService } from 'src/app/core/services/user/user.service';

@Component({
  selector: 'app-edit-user',
  templateUrl: './edit-user.component.html'
})
export class EditUserComponent {
  userId: number = 0;
  form: any = {
    company_name: null,
    company_logo: null,
  };
  image?: any;
  isUploading: boolean = false;
  isUploadFailed: boolean = false;
  errorMessage: string = '';

  constructor(private userService: UserService, private route: ActivatedRoute, private router: Router) { }
  
  ngOnInit(): void {
    this.getUser();

  }

  async getUser(): Promise<void> {
    this.userId = Number(this.route.snapshot.paramMap.get('id'));
  }

  onNameSubmit(): void {
    console.log("Name submitted");
    this.isUploading = true;

    // this.userService.createUser(this.companyId, form.company_name).subscribe({
    //   next: data => {
    //     this.isUploadFailed = false;
    //     this.isUploading = false;
    //     //this.router.navigate(['/companies']);
    //   },
    //   error: err => {
    //     this.isUploading = false;
    //     console.log(err);
    //     this.errorMessage = "Something went wrong! Please try again.";
    //     this.isUploadFailed = true;
    //   }
    // });
  }
}
