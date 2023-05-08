import { Injectable } from '@angular/core';
import { ApiService } from '../api/api.service';
import { UserCreation } from '../../models/create-user.model';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  constructor(private apiService: ApiService) { }

  getUsers() {
    return this.apiService.get('/users');
  }

  createUser(newUser: UserCreation) {
    return this.apiService.post('/users', newUser);
  }
}