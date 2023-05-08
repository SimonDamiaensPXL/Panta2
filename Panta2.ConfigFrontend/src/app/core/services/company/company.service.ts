import { Injectable } from '@angular/core';
import { ApiService } from '../api/api.service';
import { CompanyCreation } from '../../models/create-company.model';

@Injectable({
  providedIn: 'root'
})
export class CompanyService {

  constructor(private apiService: ApiService) { }

  getCompanies() {
    return this.apiService.get('/companies');
  }

  getCompanyById(companyId: number) {
    return this.apiService.get(`/companies/${companyId}`);
  }

  createCompany(name: string, logo: string) {
    return this.apiService.post('/companies', { name, logo });
  }

  editCompany(name: string, logo: string) {
    return this.apiService.put('/companies', { name, logo });
  }
}
