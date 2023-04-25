import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-companies-overview',
  templateUrl: './companies-overview.component.html',
})
export class CompaniesOverviewComponent {

  constructor(private router: Router) { }

  goToAddCompany(): void {
    this.router.navigate(['/companies/add']);
  }
 }
