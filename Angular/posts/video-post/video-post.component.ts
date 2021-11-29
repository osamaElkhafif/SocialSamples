import { OAuthService } from 'angular-oauth2-oidc';
import { PostModel } from 'src/app/nonAngularSpecific/httpApiModels/receivingModels/postsModel';
import {
  ChangeDetectionStrategy,
  Component,
  ElementRef,
  OnInit,
  ViewChild,
} from '@angular/core';
import Hls from 'hls.js';
import { PostPrivacyEnum } from 'src/app/nonAngularSpecific/httpApiModels/receivingModels/postPrivacyEnum';
import { ShowControllers } from './showControllers';

@Component({
  selector: 'app-video-post',
  templateUrl: './video-post.component.html',
  styleUrls: ['./video-post.component.css'],
  providers: [ShowControllers],
  changeDetection: ChangeDetectionStrategy.Default,
})
export class VideoPostComponent implements OnInit {
  @ViewChild('video', { static: true })
  video: ElementRef;

  hls: Hls = new Hls({
    debug: true,
    xhrSetup: (xhr, url) => {
      xhr.setRequestHeader(
        'Authorization',
        ` Bearer ${this.oAuthService.getAccessToken()}`
      );
    },
    backBufferLength: 10,
    maxMaxBufferLength: 20,
  });

  constructor(
    private postModel: PostModel,
    private oAuthService: OAuthService,
    private showControllers: ShowControllers
  ) {}

  ngAfterViewInit(): void {
    var videoSrc = this.postModel.postVideoName;
    this.hls.loadSource(videoSrc);
    this.hls.attachMedia(this.video.nativeElement as HTMLMediaElement);
    this.hls.on(Hls.Events.MANIFEST_PARSED, () => {
      var levels = this.hls.levels;
    });
    this.hls.on(Hls.Events.LEVEL_SWITCHED, () => {
      console.log('levelSwitched');
    });

    this.hls.on(Hls.Events.LEVEL_LOADED, () => {
      console.log('levelLoaded');
    });
    this.hls.on(Hls.Events.BUFFER_APPENDED, () => {
      console.log('bufferAppended');
    });
  }

  ngOnInit(): void {}

  onClicked() {
    if (this.hls.currentLevel == 1) {
      this.hls.currentLevel = 0;
      this.hls.media?.pause();
      setTimeout(() => {
        this.hls.media?.play();
      }, 6000);
    } else {
      this.hls.currentLevel = 1;
    }
  }

  videoWrapperclicked() {
    this.showControllers.show.next(true);
  }
}
