import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-services-overview',
  templateUrl: './services-overview.component.html',
})
export class ServicesOverviewComponent {

  constructor(private router: Router) { }

  goToAddService(): void {
    this.router.navigate(['/services/add']);
  }
}
