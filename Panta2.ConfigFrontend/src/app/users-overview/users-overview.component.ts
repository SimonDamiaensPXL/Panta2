import { Component } from '@angular/core';
import { User } from '../core/models/user.model';
import { UserService } from '../core/services/user/user.service';
import { firstValueFrom } from 'rxjs';

@Component({
  selector: 'app-users-overview',
  templateUrl: './users-overview.component.html',
  styleUrls: ['./users-overview.component.sass']
})
export class UsersOverviewComponent {
  users: User[] = [];
  isLoading: boolean = true;

  constructor(private userService: UserService) { }

  async ngOnInit(): Promise<void> {
    this.users = await firstValueFrom(this.userService.getUsers());
    this.isLoading = false;
  }
}
