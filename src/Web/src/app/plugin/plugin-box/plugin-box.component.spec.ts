import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PluginBoxComponent } from './plugin-box.component';

describe('PluginBoxComponent', () => {
  let component: PluginBoxComponent;
  let fixture: ComponentFixture<PluginBoxComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PluginBoxComponent]
    });
    fixture = TestBed.createComponent(PluginBoxComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
