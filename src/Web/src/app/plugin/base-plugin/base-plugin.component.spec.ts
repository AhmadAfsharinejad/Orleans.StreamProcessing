import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BasePluginComponent } from './base-plugin.component';

describe('BasePluginComponent', () => {
  let component: BasePluginComponent;
  let fixture: ComponentFixture<BasePluginComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [BasePluginComponent]
    });
    fixture = TestBed.createComponent(BasePluginComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
