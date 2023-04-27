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

  createService(name: string, link: string, icon: string) {
    return this.apiService.post('/services', { name, icon, link });
  }
}
