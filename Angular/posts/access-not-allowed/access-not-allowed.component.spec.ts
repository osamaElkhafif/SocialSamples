import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AccessNotAllowedComponent } from './access-not-allowed.component';

describe('AccessNotAllowedComponent', () => {
  let component: AccessNotAllowedComponent;
  let fixture: ComponentFixture<AccessNotAllowedComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AccessNotAllowedComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AccessNotAllowedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
