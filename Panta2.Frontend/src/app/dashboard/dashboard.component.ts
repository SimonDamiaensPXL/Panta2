import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { StorageService } from "../core/services/storage/storage.service";
import { UserService } from "../core/services/user/user.service";
import { User } from "../core/models/user.model";
import { firstValueFrom } from 'rxjs';

const USER_ID = 'user-id';
const USER = 'user';

@Component({
  selector: 'app-dashboard-page',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.sass']
})
export class DashboardComponent implements OnInit  {
  content?: any;
  userName?: string;
  isLoading: boolean = true;

  constructor(private storageService: StorageService, private userService: UserService, private router: Router) { }
  async ngOnInit(): Promise<void> {
    const user: User = await this.storageService.getUser();
    this.userName = user.firstName;

    this.isLoading = false;
  }

  logout(): void {
    this.storageService.deleteUserStorage();
    this.router.navigate(["/login"])
  }
} 