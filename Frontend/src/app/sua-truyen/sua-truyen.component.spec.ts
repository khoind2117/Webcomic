import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SuaTruyenComponent } from './sua-truyen.component';

describe('SuaTruyenComponent', () => {
  let component: SuaTruyenComponent;
  let fixture: ComponentFixture<SuaTruyenComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SuaTruyenComponent]
    });
    fixture = TestBed.createComponent(SuaTruyenComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
