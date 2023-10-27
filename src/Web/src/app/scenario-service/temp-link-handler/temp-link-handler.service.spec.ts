import { TestBed } from '@angular/core/testing';

import { TempLinkHandlerService } from './temp-link-handler.service';

describe('TempLinkHandlerService', () => {
  let service: TempLinkHandlerService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TempLinkHandlerService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
