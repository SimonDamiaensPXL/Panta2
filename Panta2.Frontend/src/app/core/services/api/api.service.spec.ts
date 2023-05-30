import { TestBed } from '@angular/core/testing';
import { ApiService } from './api.service';
import { HttpClient, HttpClientModule, HttpParams } from '@angular/common/http';
import { Observable, of, throwError } from 'rxjs';

describe('ApiService', () => {
  let apiService: ApiService;
  let httpMock: Partial<HttpClient>;

  beforeEach(() => {
    httpMock = {
      get: jest.fn(),
      put: jest.fn(),
      post: jest.fn(),
      delete: jest.fn(),
    };

    TestBed.configureTestingModule({
      imports: [HttpClientModule],
      providers: [{ provide: HttpClient, useValue: httpMock }],
    });

    apiService = TestBed.inject(ApiService);
  });

  it('should be created', () => {
    expect(apiService).toBeTruthy();
  });

  describe('get', () => {
    it('should make a GET request to the specified path with the provided params', () => {
      const path = '/users';
      const params = new HttpParams().set('page', '1');

      (httpMock.get as jest.Mock).mockReturnValue(of({}));

      apiService.get(path, params);

      expect(httpMock.get).toHaveBeenCalledWith(
        expect.stringContaining('/users'),
        expect.objectContaining({ params })
      );
    });

    it('should return an Observable', () => {
      const path = '/users';

      (httpMock.get as jest.Mock).mockReturnValue(of({}));

      const result = apiService.get(path);

      expect(result).toBeInstanceOf(Observable);
    });
  });

  describe('put', () => {
    it('should make a PUT request to the specified path with the provided body', () => {
      const path = '/users/123';
      const body = { firstName: 'John', lastName: 'Doe' };

      (httpMock.put as jest.Mock).mockReturnValue(of({}));

      apiService.put(path, body);

      expect(httpMock.put).toHaveBeenCalledWith(
        expect.stringContaining('/users/123'),
        expect.objectContaining(body)
      );
    });

    it('should return an Observable', () => {
      const path = '/users/123';
      const body = { firstName: 'John', lastName: 'Doe' };

      (httpMock.put as jest.Mock).mockReturnValue(of({}));

      const result = apiService.put(path, body);

      expect(result).toBeInstanceOf(Observable);
    });
  });

  describe('post', () => {
    it('should make a POST request to the specified path with the provided body', () => {
      const path = '/users';
      const body = { firstName: 'John', lastName: 'Doe' };

      (httpMock.post as jest.Mock).mockReturnValue(of({}));

      apiService.post(path, body);

      expect(httpMock.post).toHaveBeenCalledWith(
        expect.stringContaining('/users'),
        expect.stringContaining(JSON.stringify(body))
      );
    });

    it('should return an Observable', () => {
      const path = '/users';
      const body = { firstName: 'John', lastName: 'Doe' };

      (httpMock.post as jest.Mock).mockReturnValue(of({}));


      const result = apiService.post(path, body);

      expect(result).toBeInstanceOf(Observable);
    });
  });

  describe('delete', () => {
    it('should make a DELETE request to the specified path', () => {
      const path = '/users/123';

      (httpMock.delete as jest.Mock).mockReturnValue(of({}));

      apiService.delete(path);

      expect(httpMock.delete).toHaveBeenCalledWith(
        expect.stringContaining('/users/123')
      );
    });

    it('should return an Observable', () => {
      const path = '/users/123';

      (httpMock.delete as jest.Mock).mockReturnValue(of({}));


      const result = apiService.delete(path);

      expect(result).toBeInstanceOf(Observable);
    });
  });
});
