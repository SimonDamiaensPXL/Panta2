import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { LoginComponent } from './login.component';
import { Router } from '@angular/router';
import { AuthService } from '../core/services/auth/auth.service';
import { StorageService } from '../core/services/storage/storage.service';
import { of, throwError } from 'rxjs';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

describe('LoginComponent', () => {
  let component: LoginComponent;
  let fixture: ComponentFixture<LoginComponent>;
  let authService: AuthService;
  let storageService: StorageService;
  let router: any;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LoginComponent ],
      imports: [
        HttpClientModule,
        FormsModule
      ],
      providers: [
        AuthService,
        StorageService,
        { provide: Router, useValue: { navigate: jest.fn()}},
      ],
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LoginComponent);
    component = fixture.componentInstance;
    authService = TestBed.inject(AuthService);
    storageService = TestBed.inject(StorageService);
    router = TestBed.inject(Router);
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should initialize component properties correctly', () => {
    expect(component.form).toEqual({ username: null, password: null });
    expect(component.isLoggingIn).toBe(false);
    expect(component.isLoginFailed).toBe(false);
    expect(component.errorMessage).toBe('');
  });

  describe('ngOnInit', () => {
    it('should navigate to home if user is already logged in', () => {
      jest.spyOn(storageService, 'isLoggedIn').mockReturnValue(true);
      component.ngOnInit();
      expect(router.navigate).toHaveBeenCalledWith(['/home']);
    });

    it('should not navigate if user is not logged in', () => {
      jest.spyOn(storageService, 'isLoggedIn').mockReturnValue(false);
      component.ngOnInit();
      expect(router.navigate).not.toHaveBeenCalled();
    });
  });

  describe('onSubmit', () => {
    beforeEach(() => {
      component.form = { username: 'testUser', password: 'testPassword' };
    });

    it('should set isLoggingIn to true and call authService.login with correct credentials', () => {
      jest.spyOn(authService, 'login').mockReturnValue(of({ token: 'testToken', id: 'testId' }));
      component.onSubmit();
      expect(component.isLoggingIn).toBe(true);
      expect(authService.login).toHaveBeenCalledWith('testUser', 'testPassword');
    });

    it('should handle successful login', () => {
      const mockResponse = { token: 'testToken', id: 'testId' };
      jest.spyOn(authService, 'login').mockReturnValue(of(mockResponse));
      const saveTokenSpy = jest.spyOn(storageService, 'saveToken');
      const saveIdSpy = jest.spyOn(storageService, 'saveId');
      component.onSubmit();
      expect(saveTokenSpy).toHaveBeenCalledWith('testToken');
      expect(saveIdSpy).toHaveBeenCalledWith('testId');
      expect(component.isLoginFailed).toBe(false);
      expect(router.navigate).toHaveBeenCalledWith(['/home']);
    });

    it('should handle login error with 401 status', () => {
      const errorResponse = { status: 401 };
      jest.spyOn(authService, 'login').mockReturnValue(throwError(errorResponse));
      component.onSubmit();
      expect(component.isLoggingIn).toBe(false);
      expect(component.errorMessage).toBe('Username or Password is not correct.');
      expect(component.isLoginFailed).toBe(true);
    });

    it('should handle login error with non-401 status', () => {
      const errorResponse = { status: 500, message: 'Internal Server Error' };
      jest.spyOn(authService, 'login').mockReturnValue(throwError(errorResponse));
      component.onSubmit();
      expect(component.isLoggingIn).toBe(false);
      expect(component.errorMessage).toBe('Something went wrong! Please try again.');
      expect(component.isLoginFailed).toBe(true);
    });
  });
});
