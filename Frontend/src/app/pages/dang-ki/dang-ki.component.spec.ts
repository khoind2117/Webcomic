import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DangKiComponent } from './dang-ki.component';

describe('DangKiComponent', () => {
  let component: DangKiComponent;
  let fixture: ComponentFixture<DangKiComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [DangKiComponent]
    });
    fixture = TestBed.createComponent(DangKiComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
