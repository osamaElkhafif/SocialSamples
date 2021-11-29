import { PostModel } from './../../../../nonAngularSpecific/httpApiModels/receivingModels/postsModel';
import { UserProfilePictureData } from './../../common/user-profile-picture/userProfilePictureData';
import { Component, OnInit, ChangeDetectorRef, Input } from '@angular/core';
import { ItemComponent } from '../../common/appListArguments';
import { TestClass } from 'src/app/nonAngularSpecific/oAuthRelated/testClass';

export const getPostModel = () => {
  return new PostModel();
};

export const getTest = () => {
  return new TestClass();
};

@Component({
  selector: 'app-post-item',
  templateUrl: './post-item.component.html',
  styleUrls: ['./post-item.component.css'],
  providers: [
    {
      provide: TestClass,
      useFactory: getTest,
    },
    {
      provide: PostModel,
      useFactory: getPostModel,
    },
  ],
})
export class PostItemComponent implements OnInit, ItemComponent<PostModel> {
  constructor(
    public postModel: PostModel,
    private ChangeDetectorRef: ChangeDetectorRef,
    public testClass: TestClass
  ) {}

  itemData!: PostModel;
  @Input()
  sharedPostModel: PostModel;
  userProfilePictureDataForThis!: UserProfilePictureData;
  @Input()
  isShared: boolean = false;

  ngOnInit(): void {
    if (this.isShared) {
      Object.assign(this.postModel, this.sharedPostModel);
    } else {
      Object.assign(this.postModel, this.itemData);
    }
  }
}
