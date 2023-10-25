import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ScenarioPanelComponent } from './scenario-panel.component';

describe('ScenarioPanelComponent', () => {
  let component: ScenarioPanelComponent;
  let fixture: ComponentFixture<ScenarioPanelComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ScenarioPanelComponent]
    });
    fixture = TestBed.createComponent(ScenarioPanelComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
