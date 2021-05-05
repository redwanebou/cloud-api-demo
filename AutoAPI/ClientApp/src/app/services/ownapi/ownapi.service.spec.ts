import { TestBed } from '@angular/core/testing';

import { OwnAPI } from './ownapi.service';

describe('OwnAPI', () => {
  let service: OwnAPI;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(OwnAPI);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
