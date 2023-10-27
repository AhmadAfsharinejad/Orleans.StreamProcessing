import { TestBed } from '@angular/core/testing';

import { NodeConfigHandlerService } from './node-config-handler.service';

describe('NodeConfigHandlerService', () => {
  let service: NodeConfigHandlerService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(NodeConfigHandlerService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
