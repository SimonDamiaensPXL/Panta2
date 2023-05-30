import { TestBed } from '@angular/core/testing';
import { HttpClient, HttpClientModule, HttpHeaders } from '@angular/common/http';
import { AuthService } from './auth.service';
import { Observable, of } from 'rxjs';
import { expect } from '@jest/globals';

describe('AuthService', () => {
  let authService: AuthService;
  let httpMock: Partial<HttpClient>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientModule],
    });
    authService = TestBed.inject(AuthService);

    httpMock = {
      post: jest.fn(),
    };

    authService = new AuthService(httpMock as HttpClient);
  });

  it('should be created', () => {
    expect(authService).toBeTruthy();
  });

  describe('login', () => {
    it('should make a POST request to the login API endpoint with the provided username and password', () => {
      const username = 'testuser';
      const password = 'password';

      authService.login(username, password);

      expect(httpMock.post).toHaveBeenCalledWith(
        'https://localhost:7094/api/auth/signin',
        { username, password },
        expect.objectContaining({ headers: expect.any(HttpHeaders) })
      );
    });

    it('should return an Observable', () => {
      const username = 'testuser';
      const password = 'password';

      (httpMock.post as jest.Mock).mockReturnValue(of({}));

      const result = authService.login(username, password);

      expect(result).toBeInstanceOf(Observable);
    });
  });

  describe('register', () => {
    it('should make a POST request to the register API endpoint with the provided username, email, and password', () => {
      const username = 'testuser';
      const email = 'test@example.com';
      const password = 'password';

      authService.register(username, email, password);

      expect(httpMock.post).toHaveBeenCalledWith(
        'https://localhost:7094/api/auth/signup',
        { username, email, password },
        expect.objectContaining({ headers: expect.any(HttpHeaders) })
      );
    });

    it('should return an Observable', () => {
      const username = 'testuser';
      const email = 'test@example.com';
      const password = 'password';

      (httpMock.post as jest.Mock).mockReturnValue(of({}));

      const result = authService.register(username, email, password);

      expect(result).toBeInstanceOf(Observable);
    });
  });

  describe('logout', () => {
    it('should make a POST request to the logout API endpoint', () => {
      authService.logout();

      expect(httpMock.post).toHaveBeenCalledWith(
        'https://localhost:7094/api/auth/signout',
        {},
        expect.objectContaining({ headers: expect.any(HttpHeaders) })
      );
    });

    it('should return an Observable', () => {

      (httpMock.post as jest.Mock).mockReturnValue(of({}));

      const result = authService.logout();

      expect(result).toBeInstanceOf(Observable);
    });
  });
});
