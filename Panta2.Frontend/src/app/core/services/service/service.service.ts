import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { Service } from '../../models/service.model';
import { ApiService } from '../api/api.service';

@Injectable({
  providedIn: 'root'
})
export class ServiceService {

  constructor(private apiService: ApiService) { }

    getServices(id: number): Observable<Service[]> {
      return this.apiService.get(`/services/user/${id}`);
    }

    getIsFavoriteServices(id: number): Observable<Service[]> {
      return this.apiService.get(`/services/isfavorites/${id}`);
    }

    getFavoriteServices(id: number): Observable<Service[]> {
      return this.apiService.get(`/services/favorites/${id}`);
    }
}
