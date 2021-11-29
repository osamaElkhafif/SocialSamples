import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ImagesPostComponent } from './images-post.component';

describe('ImagesPostComponent', () => {
  let component: ImagesPostComponent;
  let fixture: ComponentFixture<ImagesPostComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ImagesPostComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ImagesPostComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
