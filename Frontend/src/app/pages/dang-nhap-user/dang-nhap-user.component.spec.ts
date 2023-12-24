/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { DangNhapUserComponent } from './dang-nhap-user.component';

describe('DangNhapUserComponent', () => {
  let component: DangNhapUserComponent;
  let fixture: ComponentFixture<DangNhapUserComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DangNhapUserComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DangNhapUserComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
