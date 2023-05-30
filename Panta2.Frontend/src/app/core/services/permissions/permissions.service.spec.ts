import { TestBed } from '@angular/core/testing';
import { PermissionsService } from './permissions.service';
import { HttpClientModule } from '@angular/common/http';
import { StorageService } from '../storage/storage.service';
import { ActivatedRouteSnapshot, Router, RouterStateSnapshot } from '@angular/router';
import { of } from 'rxjs';

describe('PermissionsService', () => {
  let permissionsService: PermissionsService;
  let storageServiceMock: Partial<StorageService>;
  let routerMock: Partial<Router>;
  let next: ActivatedRouteSnapshot;
  let state: RouterStateSnapshot;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientModule]
    });
    //permissionsService = TestBed.inject(PermissionsService);

    storageServiceMock = {
      isLoggedIn: jest.fn(),
    };
    routerMock = {
      navigate: jest.fn(),
    };
    next = {} as ActivatedRouteSnapshot;
    state = {} as RouterStateSnapshot;

    permissionsService = new PermissionsService(storageServiceMock as StorageService, routerMock as Router);
  });

  it('should be created', () => {
    expect(permissionsService).toBeTruthy();
  });

  describe('canActivate', () => {
    it('should return true if the user is logged in', () => {
      storageServiceMock.isLoggedIn = jest.fn().mockReturnValue(true);

      const result = permissionsService.canActivate(next, state);

      expect(storageServiceMock.isLoggedIn).toHaveBeenCalled();
      expect(routerMock.navigate).not.toHaveBeenCalled();
      expect(result).toBe(true);
    });

    it('should navigate to login and return false if the user is not logged in', () => {
      storageServiceMock.isLoggedIn = jest.fn().mockReturnValue(false);

      const result = permissionsService.canActivate(next, state);

      expect(storageServiceMock.isLoggedIn).toHaveBeenCalled();
      expect(routerMock.navigate).toHaveBeenCalledWith(['/login']);
      expect(result).toBe(false);
    });

    it('should return true if the user is logged in asynchronously', async () => {
      storageServiceMock.isLoggedIn = jest.fn().mockReturnValue(of(true));

      const result = await permissionsService.canActivate(next, state);

      expect(storageServiceMock.isLoggedIn).toHaveBeenCalled();
      expect(routerMock.navigate).not.toHaveBeenCalled();
      expect(result).toBe(true);
    });
  });
});
