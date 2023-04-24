import { Component } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { User } from '../../core/models/user.model';
import { UserService } from '../../core/services/user/user.service';

@Component({
  selector: 'app-users-list',
  templateUrl: './user-list.component.html',
})
export class UsersListComponent {
  users: User[] = [];
  isLoading: boolean = true;

  constructor(private userService: UserService) { }

  async ngOnInit(): Promise<void> {
    this.users = await firstValueFrom(this.userService.getUsers());
    this.isLoading = false;
  }
}
