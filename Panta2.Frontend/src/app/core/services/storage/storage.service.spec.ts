import { TestBed } from '@angular/core/testing';
import { StorageService } from './storage.service';
import { HttpClientModule } from '@angular/common/http';
import { Router } from '@angular/router';
import { UserService } from '../user/user.service';
import { of } from 'rxjs';
import { User } from '../../models/user.model';

describe('StorageService', () => {
  let storageService: StorageService;
  let userServiceMock: Partial<UserService>;
  let routerMock: Partial<Router>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientModule]
    });
    storageService = TestBed.inject(StorageService);

    userServiceMock = {
      getUserById: jest.fn(),
    };
    routerMock = {
      navigate: jest.fn(),
    };

    storageService = new StorageService(userServiceMock as UserService, routerMock as Router);
  });

  afterEach(() => {
    window.sessionStorage.clear();
  });

  it('should be created', () => {
    expect(storageService).toBeTruthy();
  });

  describe('clean', () => {
    it('should clear the session storage', () => {
      window.sessionStorage.setItem('key', 'value');

      storageService.clean();

      expect(window.sessionStorage.length).toBe(0);
    });
  });

  describe('getUser', () => {
    const mockUser: User = { id: 1, firstName: 'John', lastName: 'Doe', companyId: 1 };

    it('should return the user from session storage if it exists', async () => {
      window.sessionStorage.setItem('USER', JSON.stringify(mockUser));

      const result = await storageService.getUser();

      expect(userServiceMock.getUserById).not.toHaveBeenCalled();
      expect(result).toEqual(mockUser);
    });

    it('should fetch the user from the user service and store it in session storage if it does not exist', async () => {
      const id = '123';
      userServiceMock.getUserById = jest.fn().mockReturnValue(of(mockUser));
      window.sessionStorage.setItem('user-id', id);

      const result = await storageService.getUser();

      expect(userServiceMock.getUserById).toHaveBeenCalledWith(id);
      expect(window.sessionStorage.getItem('user')).toBe(JSON.stringify(mockUser));
      expect(result).toEqual(mockUser);
    });

    it('should handle errors when fetching the user', async () => {
      const id = '123';
      const errorMessage = 'Error fetching user';
      userServiceMock.getUserById = jest.fn().mockRejectedValue(errorMessage);
      window.sessionStorage.setItem('user-id', id);
      console.error = jest.fn();

      const result = await storageService.getUser();

      expect(userServiceMock.getUserById).toHaveBeenCalledWith(id);
      expect(window.sessionStorage.getItem('user')).toBeNull();
      expect(result).toBeNull();
    });
  });

  describe('deleteUserStorage', () => {
    it('should remove user-related items from session storage and navigate to login', () => {
      window.sessionStorage.setItem('auth-user', 'token');
      window.sessionStorage.setItem('user-id', '123');
      window.sessionStorage.setItem('USER', JSON.stringify({ id: 1, name: 'John Doe' }));

      storageService.deleteUserStorage();

      expect(window.sessionStorage.getItem('auth-user')).toBeNull();
      expect(window.sessionStorage.getItem('user-id')).toBeNull();
      expect(window.sessionStorage.getItem('user')).toBeNull();
      expect(routerMock.navigate).toHaveBeenCalledWith(['/login']);
    });
  });

  describe('isLoggedIn', () => {
    it('should return true if the user is logged in', () => {
      window.sessionStorage.setItem('auth-user', 'token');

      const result = storageService.isLoggedIn();

      expect(result).toBe(true);
    });

    it('should return false if the user is not logged in', () => {
      const result = storageService.isLoggedIn();

      expect(result).toBe(false);
    });
  });
});
