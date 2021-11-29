import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VideoControllersComponent } from './video-controllers.component';

describe('VideoControllersComponent', () => {
  let component: VideoControllersComponent;
  let fixture: ComponentFixture<VideoControllersComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ VideoControllersComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(VideoControllersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
