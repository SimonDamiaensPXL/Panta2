import { Injectable } from '@angular/core';
import { UserService } from '../user/user.service';

import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class StorageService {
  constructor(private userService: UserService, private router: Router) {}
}