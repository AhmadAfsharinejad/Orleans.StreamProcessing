import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PluginsToolboxComponent } from './plugins-toolbox.component';

describe('PluginsToolboxComponent', () => {
  let component: PluginsToolboxComponent;
  let fixture: ComponentFixture<PluginsToolboxComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PluginsToolboxComponent]
    });
    fixture = TestBed.createComponent(PluginsToolboxComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
