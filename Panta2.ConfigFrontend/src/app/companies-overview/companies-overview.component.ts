import { Component } from '@angular/core';
import { Company } from '../core/models/company.model';
import { firstValueFrom } from 'rxjs';
import { CompanyService } from '../core/services/company/company.service';

@Component({
  selector: 'app-companies-overview',
  templateUrl: './companies-overview.component.html',
  styleUrls: ['./companies-overview.component.sass']
})
export class CompaniesOverviewComponent {
  companies: Company[] = [];
  isLoading: boolean = true;

  constructor(private companyService: CompanyService) { }

  async ngOnInit(): Promise<void> {
    this.companies = await firstValueFrom(this.companyService.getCompanies());
  }
}
