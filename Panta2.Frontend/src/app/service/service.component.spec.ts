import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ServiceComponent } from './service.component';
import { Service } from '../core/models/service.model';

describe('ServiceComponent', () => {
  let component: ServiceComponent;
  let fixture: ComponentFixture<ServiceComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ServiceComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ServiceComponent);
    component = fixture.componentInstance;
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should correctly bind the service input', () => {
    const mockService: Service = { serviceId: 1, name: 'Test Service', icon: 'Test Icon', link: 'Test Link', isFavorite: true };
    component.service = mockService;
    fixture.detectChanges();

    const serviceNameElement: HTMLElement = fixture.nativeElement.querySelector('h2');
    expect(serviceNameElement.textContent).toBe('Test Service');
    const imgElement: HTMLImageElement = fixture.nativeElement.querySelector('img');
    expect(imgElement.getAttribute('src')).toBe('Test Icon');
    const aElement: HTMLImageElement = fixture.nativeElement.querySelector('a');
    expect(aElement.getAttribute('href')).toBe('Test Link');
  });
});
