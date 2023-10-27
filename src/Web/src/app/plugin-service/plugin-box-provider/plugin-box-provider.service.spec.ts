import { TestBed } from '@angular/core/testing';

import { PluginBoxProviderService } from './plugin-box-provider.service';

describe('PluginBoxProviderService', () => {
  let service: PluginBoxProviderService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PluginBoxProviderService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
