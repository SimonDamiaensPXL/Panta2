import { Component, OnInit } from "@angular/core";
import { Company } from "../core/models/company.model";
import { firstValueFrom } from "rxjs";
import { CompanyService } from "../core/services/company/company.service";

@Component({
  selector: 'app-dashboard-page',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.sass']
})
export class DashboardComponent implements OnInit {
  companies: Company[] = [];
  isLoading: boolean = true;

  constructor(private companyService: CompanyService) { }

  async ngOnInit(): Promise<void> {

    this.companies = await firstValueFrom(this.companyService.getCompanies());
    this.isLoading = false;
  }

  onFiltered(filteredItems: any[]) {
  }
} 