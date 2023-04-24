import { Injectable } from '@angular/core';

import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class StorageService {
  item: string = "home";

  constructor(private router: Router) {}

  changeItem(item: string): void {
    this.item = item;
  }

  getItem(): any {
    return this.item;
  }
}