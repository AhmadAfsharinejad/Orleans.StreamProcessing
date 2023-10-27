import { TestBed } from '@angular/core/testing';

import { PluginMetadataProviderService } from './plugin-metadata-provider.service';

describe('PluginMetadataProviderService', () => {
  let service: PluginMetadataProviderService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PluginMetadataProviderService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
