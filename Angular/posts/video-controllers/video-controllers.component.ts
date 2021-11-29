import { TranslateService } from '@ngx-translate/core';
import { ChangeDetectionStrategy } from '@angular/compiler/src/compiler_facade_interface';
import { PostModel } from 'src/app/nonAngularSpecific/httpApiModels/receivingModels/postsModel';
import {
  Component,
  Input,
  OnInit,
  ChangeDetectorRef,
  AfterViewInit,
  OnDestroy,
} from '@angular/core';
import Hls, { Level } from 'hls.js';
import { ShowControllers } from '../video-post/showControllers';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-video-controllers',
  templateUrl: './video-controllers.component.html',
  styleUrls: ['./video-controllers.component.css'],
})
export class VideoControllersComponent
  implements OnInit, AfterViewInit, OnDestroy
{
  @Input()
  hls: Hls;
  @Input()
  videoWrapper: HTMLDivElement;
  show: boolean = true;
  isInFullScreenMode: boolean = false;
  percentageBuffered: number;
  positionPercentage: number;
  hlsLevels: Level[];
  duration: string;
  timeElapsed: string = '0:0:0';
  isPlaying: boolean = false;
  isAutoAdaptive: boolean = true;
  private subsciptions: Subscription[] = [];

  constructor(
    private postModel: PostModel,
    private showControllers: ShowControllers,
    public translate: TranslateService
  ) {}

  ngOnInit(): void {
    var subs = this.showControllers.show.subscribe((value) => {
      this.show = value;
    });

    this.subsciptions.push(subs);
  }

  ngAfterViewInit(): void {
    this.hls.on(Hls.Events.BUFFER_APPENDED, () => {
      var len = this.hls.media?.buffered.length;
      if (len != undefined && len >= 1) {
        console.log(
          this.hls.media?.buffered.end(0).toString() +
            this.postModel.postVideoName
        );

        this.percentageBuffered =
          (this.hls.media?.buffered.end(0)! / this.hls.media?.duration!) * 100;
        console.log(this.percentageBuffered);
      }
    });

    this.hls.on(Hls.Events.MANIFEST_LOADED, () => {
      this.hlsLevels = this.hls.levels.sort((a, b) => {
        return a.height - b.height;
      });
      var x = 12;
    });

    this.hls.on(Hls.Events.MEDIA_ATTACHED, () => {
      this.hls.media!.ontimeupdate = () => {
        this.timeElapsed = this.convertFormSecondesToTimeFormat(
          this.hls.media?.currentTime!
        );
        this.positionPercentage =
          (this.hls.media?.currentTime! / this.hls.media?.duration!) * 100;
      };
      this.hls.media!.ondurationchange = () => {
        this.duration = this.convertFormSecondesToTimeFormat(
          this.hls.media?.duration!
        );
      };
    });
  }

  convertFormSecondesToTimeFormat(seconds: number): string {
    var hours = Math.floor(seconds / 3600);
    var remainingSecondsAfterHours = seconds - hours * 3600;
    var minutes = Math.floor(remainingSecondsAfterHours / 60);
    var remainingSeconds = Math.round(
      remainingSecondsAfterHours - minutes * 60
    );
    return `${hours}:${minutes}:${remainingSeconds}`;
  }

  playIconClicked(event: Event) {
    event.stopPropagation();
    if (this.hls.media?.paused) {
      this.hls.media?.play();
      this.isPlaying = true;
    } else {
      this.hls.media?.pause();
      this.isPlaying = false;
    }
  }

  seekForoward(event: Event) {
    event.stopPropagation();
    this.hls.media!.currentTime += 10;
  }

  wrapperClicked(event: Event) {
    event.stopPropagation();
    this.show = false;
  }

  seekBackward(event: Event) {
    event.stopPropagation();
    this.hls.media!.currentTime -= 10;
  }

  ngOnDestroy(): void {
    this.subsciptions.forEach((sub) => {
      sub.unsubscribe();
    });
  }

  changeVideoQuality(index: number, event: Event) {
    event.stopPropagation();
    this.hls.currentLevel = index;
    if (index == -1) {
      this.isAutoAdaptive = true;
    } else {
      this.isAutoAdaptive = false;
    }
  }

  goToFullScreen(event: Event) {
    event.stopPropagation();
    if (document.fullscreenElement) {
      document.exitFullscreen();
      this.isInFullScreenMode = false;
    } else {
      this.videoWrapper.requestFullscreen();
      this.isInFullScreenMode = true;
    }
  }
}
