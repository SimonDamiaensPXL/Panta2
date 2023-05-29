import { TestBed } from '@angular/core/testing';
import { PermissionsService } from './permissions.service';
import { HttpClientModule } from '@angular/common/http';

describe('PermissionsService', () => {
  let service: PermissionsService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientModule]
    });
    service = TestBed.inject(PermissionsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
