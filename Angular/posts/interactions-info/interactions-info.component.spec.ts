import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InteractionsInfoComponent } from './interactions-info.component';

describe('InteractionsInfoComponent', () => {
  let component: InteractionsInfoComponent;
  let fixture: ComponentFixture<InteractionsInfoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ InteractionsInfoComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(InteractionsInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
