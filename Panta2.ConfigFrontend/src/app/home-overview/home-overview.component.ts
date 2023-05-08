import { Component, OnInit } from '@angular/core';
import { StorageService } from '../core/services/storage/storage.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home-overview',
  templateUrl: './home-overview.component.html',
})
export class HomeOverviewComponent implements OnInit {

  constructor(private storageService: StorageService, private router: Router) {}
  
  async ngOnInit(): Promise<void> {
    this.storageService.changeItem("home");
  }
}