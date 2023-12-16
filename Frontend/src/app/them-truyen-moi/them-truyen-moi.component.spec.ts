import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ThemTruyenMoiComponent } from './them-truyen-moi.component';

describe('ThemTruyenMoiComponent', () => {
  let component: ThemTruyenMoiComponent;
  let fixture: ComponentFixture<ThemTruyenMoiComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ThemTruyenMoiComponent]
    });
    fixture = TestBed.createComponent(ThemTruyenMoiComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
