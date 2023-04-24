import { Injectable } from '@angular/core';

import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class StorageService {
  constructor(private router: Router) {}

  changeItem(item: string): void {
    localStorage.setItem("page", item);
  }

  getItem(): any {
    return localStorage.getItem("page");
  }
}