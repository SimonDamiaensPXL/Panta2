import { Injectable } from '@angular/core';
import { ApiService } from '../api/api.service';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  constructor(private apiService: ApiService) { }

  getUsers() {
    return this.apiService.get('/users');
  }
}