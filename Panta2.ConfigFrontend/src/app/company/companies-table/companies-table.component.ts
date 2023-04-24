import { Component } from '@angular/core';
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

  constructor(private companyService: CompanyService) { }

  async ngOnInit(): Promise<void> {
    this.companies = await firstValueFrom(this.companyService.getCompanies());
    this.isLoading = false;
  }
}
