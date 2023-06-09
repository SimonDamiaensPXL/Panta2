import { Component } from '@angular/core';
import { Company } from '../../core/models/company.model';
import { CompanyService } from '../../core/services/company/company.service';
import { firstValueFrom } from 'rxjs';

@Component({
  selector: 'app-company-list',
  templateUrl: './companies-list.component.html',
})
export class CompanyListComponent {
  companies: Company[] = [];
  isLoading: boolean = true;

  constructor(private companyService: CompanyService) { }

  async ngOnInit(): Promise<void> {
    this.companies = await firstValueFrom(this.companyService.getCompanies());
    this.isLoading = false;
  }
}
