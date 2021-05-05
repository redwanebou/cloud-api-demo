import { TestBed } from '@angular/core/testing';

import { authtokenS } from './auth-token.service';

describe('authtokenS', () => {
  let service: authtokenS;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(authtokenS);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
