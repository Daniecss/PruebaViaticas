import { TestBed } from '@angular/core/testing';

import { SalaCineService } from './sala.service';

describe('SalaService', () => {
  let service: SalaCineService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SalaCineService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
