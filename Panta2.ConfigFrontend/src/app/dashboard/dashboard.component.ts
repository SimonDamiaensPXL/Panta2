import { Component, OnInit } from "@angular/core";
import { Company } from "../core/models/company.model";
import { CompanyService } from "../core/services/company/company.service";

@Component({
  selector: 'app-dashboard-page',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.sass']
})
export class DashboardComponent implements OnInit {
  companies: Company[] = [];
  isLoading: boolean = true;
  pageNumber: number = 1;

  constructor(private companyService: CompanyService) { }

  async ngOnInit(): Promise<void> {
    this.isLoading = false;
  }

  onChangePage(pageNumber: any) {
    this.pageNumber = pageNumber;
  }

  onFiltered(filteredItems: any[]) {
  }
} 