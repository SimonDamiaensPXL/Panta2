import { DashboardComponent } from "./dashboard.component";
import { StorageService } from "../core/services/storage/storage.service";
import { UserService } from "../core/services/user/user.service";
import { ComponentFixture, TestBed } from "@angular/core/testing";
import { HttpClientModule } from '@angular/common/http';
import { of, throwError } from 'rxjs';
import { expect } from '@jest/globals';

describe('DashboardComponent', () => {
    let component: DashboardComponent;
    let fixture: ComponentFixture<DashboardComponent>;
    let userService: UserService;
    let storageService: StorageService;

    beforeEach(async () => {
        await TestBed.configureTestingModule({
          declarations: [ DashboardComponent ],
          imports: [ HttpClientModule ],
          providers: [
            UserService,
            StorageService
          ],
        })
        .compileComponents();
    });

    beforeEach(() => {
        fixture = TestBed.createComponent(DashboardComponent);
        component = fixture.componentInstance;
        userService = TestBed.inject(UserService);
        storageService = TestBed.inject(StorageService);
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });

    it('should initialize component properties correctly', () => {
        expect(component.services).toEqual([]);
        expect(component.favoriteServices).toEqual([]);
        expect(component.filteredServices).toEqual([]);
        expect(component.userName).toBeUndefined();
        expect(component.companyLogo).toBeUndefined();
        expect(component.isLoading).toBe(true);
    });

    describe('ngOnInit', () => {
        const mockUser = { id: 'testUserId' };
        const mockServices = [
            { serviceId: 1, name: 'Service 1', icon: 'icon 1', link: 'link 1', isFavorite: true },
            { serviceId: 2, name: 'Service 2', icon: 'icon 2', link: 'link 2', isFavorite: false }
        ];                      
        const mockFavoriteServices = [
            {serviceId: 1, name: 'Service 1', icon: 'icon 1', link: 'link 1', isFavorite: true }
        ];
    
        beforeEach(() => {
          jest.spyOn(storageService, 'getUser').mockResolvedValue(mockUser);
          jest.spyOn(userService, 'getServices').mockReturnValue(of(mockServices));
          jest.spyOn(userService, 'getFavoriteServices').mockReturnValue(of(mockFavoriteServices));
        });
    
        it('should fetch user and services data and update component properties', async () => {
          await component.ngOnInit();
          expect(storageService.getUser).toHaveBeenCalled();
          expect(userService.getServices).toHaveBeenCalledWith(mockUser.id);
          expect(userService.getFavoriteServices).toHaveBeenCalledWith(mockUser.id);
          expect(component.services).toEqual(mockServices);
          expect(component.favoriteServices).toEqual(mockFavoriteServices);
          expect(component.filteredServices).toEqual(mockServices);
          expect(component.isLoading).toBe(false);
        });

        it('should handle errors and delete user storage', async () => {
            const mockError = new Error('Test error');
            jest.spyOn(console, 'error').mockImplementation(() => {}); // Mock console.error
            jest.spyOn(storageService, 'deleteUserStorage').mockImplementation(() => {}); // Mock storageService.deleteUserStorage
            jest.spyOn(userService, 'getServices').mockReturnValue(throwError(mockError));
      
            await component.ngOnInit();
      
            expect(console.error).toHaveBeenCalledWith(mockError);
            expect(storageService.deleteUserStorage).toHaveBeenCalled();
          });
    });

    describe('onFiltered', () => {
        it('should update filteredServices with the provided array', () => {
          const mockFilteredItems = [
            { serviceId: 1, name: 'Service 1', icon: 'icon 1', link: 'link 1', isFavorite: true },
            { serviceId: 2, name: 'Service 2', icon: 'icon 2', link: 'link 2', isFavorite: false }
          ];
          component.onFiltered(mockFilteredItems);
          expect(component.filteredServices).toEqual(mockFilteredItems);
        });
      });
});