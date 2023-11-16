import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DMPhieuLuuComponent } from './dmphieu-luu.component';

describe('DMPhieuLuuComponent', () => {
  let component: DMPhieuLuuComponent;
  let fixture: ComponentFixture<DMPhieuLuuComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [DMPhieuLuuComponent]
    });
    fixture = TestBed.createComponent(DMPhieuLuuComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
