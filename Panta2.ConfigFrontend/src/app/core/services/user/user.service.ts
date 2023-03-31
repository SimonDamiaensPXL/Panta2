import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Service } from '../../models/service.model';
import { ApiService } from '../api/api.service';

const API_URL = 'https://localhost:7094/api'

@Injectable({
  providedIn: 'root',
})
export class UserService {
  constructor(private http: HttpClient, private apiService: ApiService) { }

  changeName(FirstName: string, UserId: number) {
    return this.apiService.put(`/users/name`, {FirstName, UserId});
  }

  getUserById(id: any): Observable<any> {
    return this.apiService.get(`/users/${id}`);
  }

  getUserCompanyById(id: any): Observable<any> {
    return this.http.get(API_URL + `/companies/logo/${id}`, { responseType: 'text' });
  }

  editFavorite(userId: number, serviceId: number, isFavorite: boolean): Observable<any> {
    return this.apiService.put(`/users/favorite`, {userId, serviceId, isFavorite});
  }

  getServices(id: number): Observable<Service[]> {
    return this.apiService.get(`/users/services/${id}`);
  }

  getFavoriteServices(id: number): Observable<Service[]> {
    return this.apiService.get(`/users/favorites/${id}`);
  }

  getIsFavoriteServices(id: number): Observable<Service[]> {
    return this.apiService.get(`/users/isfavorites/${id}`);
  }
}