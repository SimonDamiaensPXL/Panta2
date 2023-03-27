import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

const API_URL = 'https://localhost:7094/api/';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  constructor(private http: HttpClient) { }

  getUserById(id: any): Observable<any> {
    return this.http.get(API_URL + `users/${id}`, { responseType: 'text' });
  }

  getUserCompanyById(id: any): Observable<any> {
    return this.http.get(API_URL + `companies/logo/${id}`, { responseType: 'text' });
  }
}