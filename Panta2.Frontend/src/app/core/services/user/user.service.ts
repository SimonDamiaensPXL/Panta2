import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Service } from '../../models/service.model';
import { ApiService } from '../api/api.service';
import { EditFavorite } from '../../models/editFavorite.model';

const API_URL = 'https://localhost:7094/api'

@Injectable({
  providedIn: 'root',
})
export class UserService {
  constructor(private http: HttpClient, private apiService: ApiService) { }

  getUserById(id: any): Observable<any> {
    return this.apiService.get(`users/${id}`);
  }

  getUserCompanyById(id: any): Observable<any> {
    return this.http.get(API_URL + `/companies/logo/${id}`, { responseType: 'text' });
  }

  editFavorites(userId: number, serviceId: number, isFavorite: boolean) {
    const editFavoriteFromUserModel: EditFavorite = { 
      UserId: userId, 
      ServiceId: serviceId, 
      IsFavorite: isFavorite
    };
    const response = this.apiService.put(`/users/favorite`, editFavoriteFromUserModel);
    return {
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(response),
    };
  }

  editFavorite(userId: number, serviceId: number, isFavorite: boolean) {
    const editFavoriteFromUserModel: EditFavorite = { 
      UserId: userId, 
      ServiceId: serviceId, 
      IsFavorite: isFavorite
    }
    return this.apiService.put(`/users/favorite`,editFavoriteFromUserModel);
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