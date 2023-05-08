import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { firstValueFrom } from 'rxjs';
import { Company } from 'src/app/core/models/company.model';
import { CompanyService } from 'src/app/core/services/company/company.service';

@Component({
  selector: 'app-companies-table',
  templateUrl: './companies-table.component.html',
})
export class CompanyTableComponent {
  companies: Company[] = [];
  isLoading: boolean = true;

  constructor(private companyService: CompanyService, private router: Router) { }

  async ngOnInit(): Promise<void> {
    this.companies = await firstValueFrom(this.companyService.getCompanies());
    this.isLoading = false;
  }

  goToEditCompany(companyId: number) {
    this.router.navigate([`/company/${companyId}`]);
  }
}
