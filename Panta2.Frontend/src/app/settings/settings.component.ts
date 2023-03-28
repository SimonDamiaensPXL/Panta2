import { Component, OnInit } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { Service } from '../core/models/service.model';
import { User } from '../core/models/user.model';
import { ServiceService } from '../core/services/service/service.service';
import { StorageService } from '../core/services/storage/storage.service';
import { UserService } from '../core/services/user/user.service';

@Component({
  selector: 'app-settings',
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.sass']
})
export class SettingsComponent implements OnInit {
  services: Service[] = [];
  isLoading: boolean = true;

  constructor(private storageService: StorageService, private serviceService: ServiceService, private userService: UserService) {}

  async ngOnInit(): Promise<void> {
    try {
      const user: User = await this.storageService.getUser();
      this.services = await firstValueFrom(this.userService.getIsFavoriteServices(user.id));
      this.isLoading = false;
    } catch(error) {
      console.error(error);
    }
  }

  async editFavorite(serviceId: number, isFavorite: boolean): Promise<void> {
    const user: User = await this.storageService.getUser();

    this.userService.editFavorite(user.id, serviceId, isFavorite);
    // this.userService.editFavorite(user.id, serviceId, isFavorite).subscribe({
    //   next() { 
    //     window.location.reload();
    //   },
    //   error(msg) {
    //     console.error(msg);
    //   }
    // })
  }
}
