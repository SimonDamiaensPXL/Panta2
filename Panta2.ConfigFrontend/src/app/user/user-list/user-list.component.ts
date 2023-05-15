import { Component, Input } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { User } from '../../core/models/user.model';
import { UserService } from '../../core/services/user/user.service';
import { CompanyService } from 'src/app/core/services/company/company.service';

@Component({
  selector: 'app-users-list',
  templateUrl: './user-list.component.html',
})

export class UsersListComponent {
  @Input() companyId: number = 0;

  users: User[] = [];
  isLoading: boolean = true;

  constructor(private companyService: CompanyService, private userService: UserService) { }

  async ngOnInit(): Promise<void> {
    if (this.companyId == 0) {
      this.users = await firstValueFrom(this.userService.getUsers());
    } else {
      console.log(this.companyId);
      this.users = await firstValueFrom(this.companyService.getCompanyUsers(this.companyId));
    }

    console.log(this.users);

    this.isLoading = false;
  }
}
