import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InteractToPostComponent } from './interact-to-post.component';

describe('InteractToPostComponent', () => {
  let component: InteractToPostComponent;
  let fixture: ComponentFixture<InteractToPostComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ InteractToPostComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(InteractToPostComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
