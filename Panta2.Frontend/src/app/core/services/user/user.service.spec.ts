import { TestBed } from '@angular/core/testing';
import { UserService } from './user.service';
import { HttpClientModule } from '@angular/common/http';
import { ApiService } from '../api/api.service';
import { Observable, of } from 'rxjs';
import { Service } from '../../models/service.model';
import { expect } from '@jest/globals';

describe('UserService', () => {
  let userService: UserService;
  let apiServiceMock: Partial<ApiService>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientModule]
    });
    userService = TestBed.inject(UserService);

    apiServiceMock = {
      put: jest.fn(),
      get: jest.fn(),
    }

    userService = new UserService(apiServiceMock as ApiService);
  });

  it('should be created', () => {
    expect(userService).toBeTruthy();
  });

  describe('changeName', () => {
    it('should call the apiService put method with the correct endpoint and data', () => {
      const firstName = 'John';
      const userId = 1;

      userService.changeName(firstName, userId);

      expect(apiServiceMock.put).toHaveBeenCalledWith('/users/name', { FirstName: firstName, UserId: userId });
    });
  });

  describe('getUserById', () => {
    it('should call the apiService get method with the correct endpoint and return the result', () => {
      const userId = 1;
      const mockResponse = { id: userId, name: 'John Doe' };
      (apiServiceMock.get as jest.Mock).mockReturnValue(of(mockResponse));

      const result = userService.getUserById(userId);

      expect(apiServiceMock.get).toHaveBeenCalledWith(`/users/${userId}`);
      expect(result).toBeInstanceOf(Observable);
      result.subscribe((response) => {
        expect(response).toEqual(mockResponse);
      });
    });
  });

  describe('getUserCompanyById', () => {
    it('should call the apiService get method with the correct endpoint and return the result', () => {
      const companyId = 1;
      const mockResponse = { id: companyId, name: 'Company'};
      (apiServiceMock.get as jest.Mock).mockReturnValue(of(mockResponse));

      const result = userService.getUserCompanyById(companyId);

      expect(apiServiceMock.get).toHaveBeenCalledWith(`/companies/logo/${companyId}`);
      expect(result).toBeInstanceOf(Observable);
      result.subscribe((response) => {
        expect(response).toEqual(mockResponse);
      });
    });
  });

  describe('editFavorite', () => {
    it('should call the apiService put method with the correct endpoint and data', () => {
      const userId = 1;
      const serviceId = 1;
      const isFavorite = true;

      userService.editFavorite(userId, serviceId, isFavorite);

      expect(apiServiceMock.put).toHaveBeenCalledWith('/users/favorite', { userId, serviceId, isFavorite });
    });
  });

  describe('getServices', () => {
    it('should call the apiService get method with the correct endpoint and return the services', () => {
      const userId = 1;
      const mockResponse: Service[] = [
        { serviceId: 1, name: 'Service 1', icon: 'icon 1', link: 'link 1', isFavorite: true },
        { serviceId: 2, name: 'Service 2', icon: 'icon 2', link: 'link 2', isFavorite: false }
      ];
      (apiServiceMock.get as jest.Mock).mockReturnValue(of(mockResponse));

      const result = userService.getServices(userId);

      expect(apiServiceMock.get).toHaveBeenCalledWith(`/users/services/${userId}`);
      expect(result).toBeInstanceOf(Observable);
      result.subscribe((response) => {
        expect(response).toEqual(mockResponse);
      });
    });
  });

  describe('getFavoriteServices', () => {
    it('should call the apiService get method with the correct endpoint and return the services', () => {
      const userId = 1;
      const mockResponse: Service[] = [
        { serviceId: 1, name: 'Service 1', icon: 'icon 1', link: 'link 1', isFavorite: true },
        { serviceId: 2, name: 'Service 2', icon: 'icon 2', link: 'link 2', isFavorite: false }
      ];
      (apiServiceMock.get as jest.Mock).mockReturnValue(of(mockResponse));

      const result = userService.getFavoriteServices(userId);

      expect(apiServiceMock.get).toHaveBeenCalledWith(`/users/favorites/${userId}`);
      expect(result).toBeInstanceOf(Observable);
      result.subscribe((response) => {
        expect(response).toEqual(mockResponse);
      });
    });
  });

  describe('getIsFavoriteServices', () => {
    it('should call the apiService get method with the correct endpoint and return the services', () => {
      const userId = 1;
      const mockResponse: Service[] = [
        { serviceId: 1, name: 'Service 1', icon: 'icon 1', link: 'link 1', isFavorite: true },
        { serviceId: 2, name: 'Service 2', icon: 'icon 2', link: 'link 2', isFavorite: false }
      ];
      (apiServiceMock.get as jest.Mock).mockReturnValue(of(mockResponse));

      const result = userService.getIsFavoriteServices(userId);

      expect(apiServiceMock.get).toHaveBeenCalledWith(`/users/isfavorites/${userId}`);
      expect(result).toBeInstanceOf(Observable);
      result.subscribe((response) => {
        expect(response).toEqual(mockResponse);
      });
    });
  });
});
