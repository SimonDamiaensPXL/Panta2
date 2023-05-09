import { Injectable } from '@angular/core';
import { ApiService } from '../api/api.service';

@Injectable({
  providedIn: 'root'
})
export class ServiceService {

  constructor(private apiService: ApiService) { }

  getServices() {
    return this.apiService.get('/services');
  }

  getServiceById(serviceId: number) {
    return this.apiService.get(`/services/${serviceId}`);
  }

  createService(name: string, link: string, icon: string) {
    return this.apiService.post('/services', { name, icon, link });
  }

  editServiceName(id: number, name: string) {
    return this.apiService.put('/services/name', {id, name});
  }

  editServiceIcon(id: number, name: string, icon: string) {
    return this.apiService.put('/services/icon', {id, name, icon});
  }

  editServiceLink(id: number, link: string) {
    return this.apiService.put('/services/link', {id, link});
  }
}
