import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PluginPropertiesComponent } from './plugin-properties.component';

describe('PluginPropertiesComponent', () => {
  let component: PluginPropertiesComponent;
  let fixture: ComponentFixture<PluginPropertiesComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PluginPropertiesComponent]
    });
    fixture = TestBed.createComponent(PluginPropertiesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
