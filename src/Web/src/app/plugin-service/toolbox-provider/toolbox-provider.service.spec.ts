import { TestBed } from '@angular/core/testing';

import { ToolboxProviderService } from './toolbox-provider.service';

describe('ToolboxProviderService', () => {
  let service: ToolboxProviderService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ToolboxProviderService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
