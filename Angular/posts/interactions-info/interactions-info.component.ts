import { ShareComponent } from './../../../modalComponents/share/share/share.component';
import { MatDialog } from '@angular/material/dialog';
import { TranslateService } from '@ngx-translate/core';
import { PostInteractionTypesEnum } from './../../../../nonAngularSpecific/httpApiModels/receivingModels/postInteractiontypesEnum';
import { PostModel } from './../../../../nonAngularSpecific/httpApiModels/receivingModels/postsModel';
import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-interactions-info',
  templateUrl: './interactions-info.component.html',
  styleUrls: ['./interactions-info.component.css'],
})
export class InteractionsInfoComponent implements OnInit {
  interactionTypes: PostInteractionTypesEnum[] = [];
  interactionsCount: number = 0;

  @Input()
  commentModelAsPostModel: PostModel;

  @Input()
  isCommentModelAsPostModel: boolean = false;

  postModel: PostModel = new PostModel();

  constructor(
    public constuctorPostModel: PostModel,
    public translateService: TranslateService
  ) {}

  ngOnInit(): void {
    if (this.isCommentModelAsPostModel) {
      Object.assign(this.postModel, this.commentModelAsPostModel);
    } else {
      Object.assign(this.postModel, this.constuctorPostModel);
    }
    var postInteractionTypesAndCountList: PostInteractionsTypeAndCount[] = [];

    postInteractionTypesAndCountList.push(
      new PostInteractionsTypeAndCount({
        postInteractionTypesEnum: PostInteractionTypesEnum.Like,
        count: this.postModel.likeInteractionsCount,
      })
    );

    postInteractionTypesAndCountList.push(
      new PostInteractionsTypeAndCount({
        postInteractionTypesEnum: PostInteractionTypesEnum.Love,
        count: this.postModel.loveInteractionsCount,
      })
    );

    postInteractionTypesAndCountList.push(
      new PostInteractionsTypeAndCount({
        postInteractionTypesEnum: PostInteractionTypesEnum.HaHa,
        count: this.postModel.haHaInteractionsCount,
      })
    );

    postInteractionTypesAndCountList.push(
      new PostInteractionsTypeAndCount({
        postInteractionTypesEnum: PostInteractionTypesEnum.Wow,
        count: this.postModel.wowInteractionsCount,
      })
    );

    postInteractionTypesAndCountList.push(
      new PostInteractionsTypeAndCount({
        postInteractionTypesEnum: PostInteractionTypesEnum.Sad,
        count: this.postModel.sadInteractionsCount,
      })
    );

    postInteractionTypesAndCountList.push(
      new PostInteractionsTypeAndCount({
        postInteractionTypesEnum: PostInteractionTypesEnum.Angry,
        count: this.postModel.angryInteractionsCount,
      })
    );

    this.interactionTypes = postInteractionTypesAndCountList
      .filter((x) => {
        return x.count != 0;
      })
      .sort()
      .reverse()
      .map((x) => {
        return x.postInteractionTypesEnum;
      });

    postInteractionTypesAndCountList.forEach((x) => {
      this.interactionsCount += x.count;
    });
  }
}

export class PostInteractionsTypeAndCount {
  postInteractionTypesEnum: PostInteractionTypesEnum;
  count: number;

  constructor(init?: Partial<PostInteractionsTypeAndCount>) {
    Object.assign(this, init);
  }
}
