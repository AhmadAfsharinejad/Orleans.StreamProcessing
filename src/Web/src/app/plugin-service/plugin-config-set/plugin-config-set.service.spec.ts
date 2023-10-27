import { TestBed } from '@angular/core/testing';

import { PluginConfigSetService } from './plugin-config-set.service';

describe('PluginConfigSetService', () => {
  let service: PluginConfigSetService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PluginConfigSetService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
