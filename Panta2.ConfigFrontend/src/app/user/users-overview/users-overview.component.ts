import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-users-overview',
  templateUrl: './users-overview.component.html',
})
export class UsersOverviewComponent {
  constructor(private router: Router) { }

  goToAddUser(): void {
    this.router.navigate(['/users/add']);
  }
}
