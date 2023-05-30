import { ComponentFixture, TestBed } from '@angular/core/testing';
import { SettingsComponent } from './settings.component';
import { HttpClientModule } from '@angular/common/http';
import { StorageService } from '../core/services/storage/storage.service';
import { UserService } from '../core/services/user/user.service';
import { of, throwError } from 'rxjs';
import { expect } from '@jest/globals';

describe('SettingsComponent', () => {
  let component: SettingsComponent;
  let fixture: ComponentFixture<SettingsComponent>;
  let storageService: StorageService;
  let userService: UserService;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [SettingsComponent],
      imports: [HttpClientModule],
      providers: [
        UserService,
        StorageService
      ]
    })
      .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SettingsComponent);
    component = fixture.componentInstance;
    storageService = TestBed.inject(StorageService);
    userService = TestBed.inject(UserService);
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should initialize component properties correctly', () => {
    expect(component.services).toEqual([]);
    expect(component.isLoading).toBe(true);
    expect(component.isError).toBe(false);
    expect(component.errorMessage).toBeUndefined();
    expect(component.name).toBeUndefined();
    expect(component.inputName).toBeUndefined();
  });

  describe('ngOnInit', () => {
    const mockUser = { id: 'testUserId', firstName: 'John' };
    const mockServices = [
      { serviceId: 1, name: 'Service 1', icon: 'icon 1', link: 'link 1', isFavorite: true },
      { serviceId: 2, name: 'Service 2', icon: 'icon 2', link: 'link 2', isFavorite: false }
    ];

    beforeEach(() => {
      jest.spyOn(storageService, 'getUser').mockResolvedValue(mockUser);
      jest.spyOn(userService, 'getIsFavoriteServices').mockReturnValue(of(mockServices));
    });

    it('should set name and inputName with the user\'s first name', async () => {
      await component.ngOnInit();
      expect(component.name).toBe(mockUser.firstName);
      expect(component.inputName).toBe(mockUser.firstName);
    });

    it('should set services and isLoading correctly', async () => {
      await component.ngOnInit();
      expect(component.services).toEqual(mockServices);
      expect(component.isLoading).toBe(false);
    });

    it('should handle error and log to console', async () => {
      const mockError = new Error('Test error');
      jest.spyOn(console, 'error').mockImplementation(() => { });
      jest.spyOn(userService, 'getIsFavoriteServices').mockReturnValue(throwError(mockError));

      await component.ngOnInit();

      expect(console.error).toHaveBeenCalledWith(mockError);
    });
  });

  describe('editFavorite', () => {
    const mockServiceId = 1;
    const mockIsFavorite = true;
    const mockUser = { id: 1 };

    beforeEach(() => {
      jest.spyOn(storageService, 'getUser').mockResolvedValue(mockUser);
      jest.spyOn(userService, 'getIsFavoriteServices').mockReturnValue(of());
    });

    it('should edit the favorite', async () => {
      const mockUser = { id: 123 };
      jest.spyOn(userService, 'editFavorite').mockReturnValue(of('success'));
      //jest.spyOn(console, 'log').mockImplementation(() => { });


      await component.editFavorite(1, true);

      expect(component.isLoading).toBe(false);
      expect(storageService.getUser).toHaveBeenCalled();

    });

    it('should handle error and update errorMessage and isError', async () => {
      const mockError = new Error('Test error');
      jest.spyOn(console, 'error').mockImplementation(() => { });
      jest.spyOn(userService, 'editFavorite').mockReturnValue(throwError(mockError));

      await component.editFavorite(mockServiceId, mockIsFavorite);

      expect(console.error).toHaveBeenCalledWith(mockError);
      expect(component.errorMessage).toBe('Sorry, you cannot add more than 5 favorites. Please remove an existing favorite to add a new one.');
      expect(component.isError).toBe(true);
    });
  });

  describe('changeName', () => {
    const mockUser = { id: 'testUserId' };
    const mockInputName = 'New Name';

    beforeEach(() => {
      jest.spyOn(storageService, 'getUser').mockResolvedValue(mockUser);
    });

    it('should trigger page reload on success', async () => {
      component.inputName = mockInputName;
      jest.spyOn(userService, 'changeName').mockReturnValue(of('success'));

      await component.changeName();

      expect(storageService.getUser).toHaveBeenCalled();
      expect(component.isLoading).toBe(false);
    });

    it('should handle error and update errorMessage and isError', async () => {
      component.inputName = mockInputName;
      const mockError = new Error('Test error');
      jest.spyOn(userService, 'changeName').mockReturnValue(throwError(mockError));

      await component.changeName();

      expect(component.errorMessage).toBe("We're sorry, there was a problem updating your username. Please make sure you have entered a valid username and try again.");
      expect(component.isError).toBe(true);
    });

    it('should handle missing inputName', async () => {
      component.inputName = '';
      await component.changeName();
      expect(component.errorMessage).toBe('Please make sure you have entered a valid username and try again.');
      expect(component.isError).toBe(true);
    });

    it('should handle missing inputName', async () => {
      component.inputName = '';
      await component.changeName();
      expect(component.errorMessage).toBe('Please make sure you have entered a valid username and try again.');
      expect(component.isError).toBe(true);
    });
  });

  describe('closeFavoriteError', () => {
    it('should set isError to false and trigger page reload', () => {
      component.closeFavoriteError();
      expect(component.isError).toBe(false);
    });
  });
});
