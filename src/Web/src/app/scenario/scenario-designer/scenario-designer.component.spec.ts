import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ScenarioDesignerComponent } from './scenario-designer.component';

describe('ScenarioDesignerComponent', () => {
  let component: ScenarioDesignerComponent;
  let fixture: ComponentFixture<ScenarioDesignerComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ScenarioDesignerComponent]
    });
    fixture = TestBed.createComponent(ScenarioDesignerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
