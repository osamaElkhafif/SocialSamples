import { TranslateService } from '@ngx-translate/core';
import { Router } from '@angular/router';
import { TestClass } from './../../../../nonAngularSpecific/oAuthRelated/testClass';
import { ChangeDetectionStrategy } from '@angular/compiler/src/compiler_facade_interface';
import { PostModel } from './../../../../nonAngularSpecific/httpApiModels/receivingModels/postsModel';
import {
  Component,
  OnChanges,
  OnInit,
  SimpleChanges,
  ChangeDetectorRef,
} from '@angular/core';
import { UserProfilePictureData } from '../../common/user-profile-picture/userProfilePictureData';

@Component({
  selector: 'app-post-header',
  templateUrl: './post-header.component.html',
  styleUrls: ['./post-header.component.css'],
})
export class PostHeaderComponent implements OnInit {
  userProfilePictureDataForThis: UserProfilePictureData =
    UserProfilePictureData.empty();

  timeZoneHours = Math.floor((new Date().getTimezoneOffset() * 2 * -1) / 60);
  timeZoneMintes = Math.floor(
    new Date().getTimezoneOffset() * 2 * -1 - this.timeZoneHours * 60
  );

  constructor(
    public testClass: TestClass,
    public postModel: PostModel,
    private router: Router,
    public translate: TranslateService
  ) {}

  ngOnInit(): void {
    this.userProfilePictureDataForThis = new UserProfilePictureData(
      50,
      this.postModel.postOwnerProfilePictureUrl,
      this.postModel.postOwnerProfileImagePosition,
      this.postModel.profileImageWidth,
      this.postModel.profileImageHeight,
      this.postModel.postOwnerProfileImageType
    );
  }

  userProfileClicked() {
    this.router.navigate(['/MyApp/Profile', this.postModel.postOwnerUserName]);
  }
}
