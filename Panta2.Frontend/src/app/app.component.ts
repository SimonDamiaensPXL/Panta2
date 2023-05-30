import { Component, OnInit } from '@angular/core';
import { StorageService } from './core/services/storage/storage.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
})
export class AppComponent implements OnInit {
  isLoggedIn = false;

  constructor(private storageService: StorageService) { }

  ngOnInit(): void {
    this.isLoggedIn = this.storageService.isLoggedIn();
  }
}
