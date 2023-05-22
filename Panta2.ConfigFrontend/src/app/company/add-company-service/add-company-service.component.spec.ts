import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddCompanyServiceComponent } from './add-company-service.component';

describe('AddRoleComponent', () => {
  let component: AddCompanyServiceComponent;
  let fixture: ComponentFixture<AddCompanyServiceComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddCompanyServiceComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddCompanyServiceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
