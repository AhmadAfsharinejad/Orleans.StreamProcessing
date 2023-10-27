import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PluginPropertiesTabComponent } from './plugin-properties-tab.component';

describe('PluginPropertiesTabComponent', () => {
  let component: PluginPropertiesTabComponent;
  let fixture: ComponentFixture<PluginPropertiesTabComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PluginPropertiesTabComponent]
    });
    fixture = TestBed.createComponent(PluginPropertiesTabComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
