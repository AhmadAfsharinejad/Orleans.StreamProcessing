import { TestBed } from '@angular/core/testing';

import { PluginConfigGetService } from './plugin-config-get.service';

describe('PluginConfigGetService', () => {
  let service: PluginConfigGetService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PluginConfigGetService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
