/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { DangKiUserComponent } from './dang-ki-user.component';

describe('DangKiUserComponent', () => {
  let component: DangKiUserComponent;
  let fixture: ComponentFixture<DangKiUserComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DangKiUserComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DangKiUserComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
