import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditCompanyServiceComponent } from './edit-company-service.component';

describe('EditServiceComponent', () => {
  let component: EditCompanyServiceComponent;
  let fixture: ComponentFixture<EditCompanyServiceComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EditCompanyServiceComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EditCompanyServiceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
