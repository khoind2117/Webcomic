import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DangTruyenComponent } from './dang-truyen.component';

describe('DangTruyenComponent', () => {
  let component: DangTruyenComponent;
  let fixture: ComponentFixture<DangTruyenComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [DangTruyenComponent]
    });
    fixture = TestBed.createComponent(DangTruyenComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
