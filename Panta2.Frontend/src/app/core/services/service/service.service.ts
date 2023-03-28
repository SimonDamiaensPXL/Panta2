import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { Service } from '../../models/service.model';
import { ApiService } from '../api/api.service';

@Injectable({
  providedIn: 'root'
})
export class ServiceService {

  constructor(private apiService: ApiService) { }


}
