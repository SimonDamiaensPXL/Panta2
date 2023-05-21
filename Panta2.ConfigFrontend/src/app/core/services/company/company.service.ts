import { Injectable } from '@angular/core';
import { ApiService } from '../api/api.service';
import { CompanyCreation } from '../../models/create-company.model';
import { RoleCreation } from '../../models/create-role.model';

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

  getCompanyUsers(companyId: number) {
    return this.apiService.get(`/companies/users/${companyId}`);
  }

  getCompanyRoles(companyId: number) {
    return this.apiService.get(`/companies/roles/${companyId}`);
  }

  getCompanyServices(companyId: number) {
    return this.apiService.get(`/companies/services/${companyId}`);
  }

  getCompanyServiceNames(companyId: number) {
    return this.apiService.get(`/companies/servicenames/${companyId}`);
  }

  createCompany(name: string, logo: string) {
    return this.apiService.post('/companies', { name, logo });
  }

  createRole(newRole: RoleCreation, companyId: number) {
    return this.apiService.post(`/companies/roles/${companyId}`, newRole);
  }

  editCompanyName(id: number, name: string) {
    return this.apiService.put('/companies/name', {id, name});
  }

  editCompanyLogo(id: number, name: string, logo: string) {
    return this.apiService.put('/companies/logo', {id, name, logo });
  }
}
