import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InfoTruyenComponent } from './info-truyen.component';

describe('InfoTruyenComponent', () => {
  let component: InfoTruyenComponent;
  let fixture: ComponentFixture<InfoTruyenComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [InfoTruyenComponent]
    });
    fixture = TestBed.createComponent(InfoTruyenComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
