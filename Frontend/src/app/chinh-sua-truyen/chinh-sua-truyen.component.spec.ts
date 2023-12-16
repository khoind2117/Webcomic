import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChinhSuaTruyenComponent } from './chinh-sua-truyen.component';

describe('ChinhSuaTruyenComponent', () => {
  let component: ChinhSuaTruyenComponent;
  let fixture: ComponentFixture<ChinhSuaTruyenComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ChinhSuaTruyenComponent]
    });
    fixture = TestBed.createComponent(ChinhSuaTruyenComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
