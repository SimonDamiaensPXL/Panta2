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

  getUserById(userId: number) {
    return this.apiService.get(`/users/${userId}`);
  }

  getUserRole(userId: number) {
    return this.apiService.get(`/users/role/${userId}`);
  }

  createUser(newUser: UserCreation) {
    return this.apiService.post('/users', newUser);
  }

  editUserName(id: number, userName: string) {
    return this.apiService.put('/users/username', { id, userName });
  }

  editName(id: number, firstName: string, lastName: string) {
    return this.apiService.put('/users/name', { id, firstName, lastName });
  }

  editEmail(id: number, email: string) {
    return this.apiService.put('/users/email', { id, email });
  }

  editPassword(id: number, password: string, confirmPassword: string) {
    return this.apiService.put('/users/password', { id, password, confirmPassword });
  }

  editRole(id: number, roleId: number, newRoleId: number) {
    return this.apiService.put('/users/role', { id, roleId, newRoleId });
  }

  deleteUser(id: number) {
    return this.apiService.delete(`/users/${id}`);
  }
}