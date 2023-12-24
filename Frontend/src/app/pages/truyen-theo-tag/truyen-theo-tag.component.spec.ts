/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { TruyenTheoTagComponent } from './truyen-theo-tag.component';

describe('TruyenTheoTagComponent', () => {
  let component: TruyenTheoTagComponent;
  let fixture: ComponentFixture<TruyenTheoTagComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TruyenTheoTagComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TruyenTheoTagComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
