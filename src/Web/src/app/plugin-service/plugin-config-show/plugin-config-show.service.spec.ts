import { TestBed } from '@angular/core/testing';

import { PluginConfigShowService } from './plugin-config-show.service';

describe('PluginConfigShowService', () => {
  let service: PluginConfigShowService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PluginConfigShowService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
