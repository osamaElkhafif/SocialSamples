import { FlexLayoutModule } from '@angular/flex-layout';
import { TranslateModule } from '@ngx-translate/core';
import { TestClass } from './../../../nonAngularSpecific/oAuthRelated/testClass';
import { MyCommonModule } from './../common/my-common.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatGridListModule } from '@angular/material/grid-list';
import {
  getPostModel,
  PostItemComponent,
} from './post-item/post-item.component';
import { PostHeaderComponent } from './post-header/post-header.component';
import { PostModel } from 'src/app/nonAngularSpecific/httpApiModels/receivingModels/postsModel';
import { PostPrivacyComponent } from './post-privacy/post-privacy.component';
import { PostBodyComponent } from './post-body/post-body.component';
import { ImagesPostComponent } from './images-post/images-post.component';
import { VideoPostComponent } from './video-post/video-post.component';
import { VideoControllersComponent } from './video-controllers/video-controllers.component';
import { InteractionsInfoComponent } from './interactions-info/interactions-info.component';
import { InteractToPostComponent } from './interact-to-post/interact-to-post.component';
import { NgxLongPress2Directive, NgxLongPress2Module } from 'ngx-long-press2';
import { AccessNotAllowedComponent } from './access-not-allowed/access-not-allowed.component';

@NgModule({
  declarations: [
    PostItemComponent,
    PostHeaderComponent,
    PostPrivacyComponent,
    PostBodyComponent,
    ImagesPostComponent,
    VideoPostComponent,
    VideoControllersComponent,
    InteractionsInfoComponent,
    InteractToPostComponent,
    AccessNotAllowedComponent,
  ],
  imports: [
    CommonModule,
    MyCommonModule,
    TranslateModule,
    MatGridListModule,
    NgxLongPress2Module,
    FlexLayoutModule,
  ],
  exports: [InteractionsInfoComponent, InteractToPostComponent],
})
export class PostsModule {}
