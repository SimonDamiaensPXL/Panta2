import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { StorageService } from '../storage/storage.service';
@Injectable({
  providedIn: 'root'
})
export class PermissionsService implements CanActivate {

  constructor(private storageService: StorageService, private router: Router) { }

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    if ((this.storageService.isLoggedIn())) {
      return true;
    }
    this.router.navigate(['/login']);
    return false;
  }
}