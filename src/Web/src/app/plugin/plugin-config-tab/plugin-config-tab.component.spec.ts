import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PluginConfigTabComponent } from './plugin-config-tab.component';

describe('PluginConfigTabComponent', () => {
  let component: PluginConfigTabComponent;
  let fixture: ComponentFixture<PluginConfigTabComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PluginConfigTabComponent]
    });
    fixture = TestBed.createComponent(PluginConfigTabComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
