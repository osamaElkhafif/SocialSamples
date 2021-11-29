import { SharingPostModalData } from './../../../appWideServicesModules/modalsServices/sharingPostModalData';
import { AddCommentInteractionModel } from './../../../../nonAngularSpecific/httpApiModels/sendingModels/addInteractionToComment';
import { CommentItemComponent } from './../../../modalComponents/comments/comment-item/comment-item.component';
import { AddPostInteractionModel } from './../../../../nonAngularSpecific/httpApiModels/sendingModels/addPostInteractions';
import { HttpClient } from '@angular/common/http';
import { InteractionsAndCommentsActions } from './../../../../nonAngularSpecific/httpApiActions/interactionsAndCommentsActions';
import { PostInteractionTypesEnum } from './../../../../nonAngularSpecific/httpApiModels/receivingModels/postInteractiontypesEnum';
import { CommentsComponent } from './../../../modalComponents/comments/comments/comments.component';
import { MatDialog } from '@angular/material/dialog';
import { PostCommentsModalData } from './../../../appWideServicesModules/modalsServices/postCommentsModalData';
import { Component, Input, OnInit } from '@angular/core';
import { PostModel } from 'src/app/nonAngularSpecific/httpApiModels/receivingModels/postsModel';
import { ShareComponent } from 'src/app/angularSpecific/modalComponents/share/share/share.component';

@Component({
  selector: 'app-interact-to-post',
  templateUrl: './interact-to-post.component.html',
  styleUrls: ['./interact-to-post.component.css'],
})
export class InteractToPostComponent implements OnInit {
  showInteractionBox: boolean = false;
  @Input()
  isForComments: boolean = false;

  @Input()
  postModelFromCommentModel: PostModel;

  @Input()
  commentItemComponent: CommentItemComponent;

  postModel: PostModel = new PostModel();

  constructor(
    public postModelconstructor: PostModel,
    private postCommentsModalData: PostCommentsModalData,
    private matDialog: MatDialog,
    private http: HttpClient,
    private sharingPostModalData: SharingPostModalData
  ) {}

  ngOnInit(): void {
    if (this.isForComments) {
      Object.assign(this.postModel, this.postModelFromCommentModel);
    } else {
      Object.assign(this.postModel, this.postModelconstructor);
    }
  }

  longPressed() {
    this.showInteractionBox = !this.showInteractionBox;
  }

  getComments() {
    this.postCommentsModalData.postId = this.postModel.postId;
    this.postCommentsModalData.postType = this.postModel.postType;

    this.matDialog.open(CommentsComponent);
  }

  interactionTypeClicked(postInteractionType: PostInteractionTypesEnum) {
    var interactionsAndCommentsActions = new InteractionsAndCommentsActions(
      this.http
    );

    if (!this.isForComments) {
      var addPostInteractionModel = new AddPostInteractionModel();

      addPostInteractionModel.postId = this.postModel.postId;
      addPostInteractionModel.postType = this.postModel.postType;
      addPostInteractionModel.postInteractionType = postInteractionType;

      interactionsAndCommentsActions
        .addPostInteraction(addPostInteractionModel)
        .subscribe((x) => {
          if (x.ok) {
            this.postModel.userInteractionToPostType = postInteractionType;
          }
        });
    } else {
      var addCommentInteractionModel = new AddCommentInteractionModel();

      addCommentInteractionModel.postId = this.postModel.postId;
      addCommentInteractionModel.postType = this.postModel.postType;
      addCommentInteractionModel.postInteractionType = postInteractionType;
      addCommentInteractionModel.commentId =
        this.commentItemComponent.itemData.commentId;
      interactionsAndCommentsActions
        .addCommentInteractoin(addCommentInteractionModel)
        .subscribe((x) => {
          if (x.ok) {
            this.commentItemComponent.changeUserInteractionType(
              postInteractionType
            );
          }
        });
    }
  }

  changeUserInteraction(postInteractionTypesEnum: PostInteractionTypesEnum) {
    this.postModel.userInteractionToPostType = postInteractionTypesEnum;
  }

  sharePost() {
    if (this.postModel.isShared == false && this.postModel.postPrivacy == 1) {
      this.sharingPostModalData.postId = this.postModel.postId;
      this.sharingPostModalData.postType = this.postModel.postType;
    } else if (
      this.postModel.isShared == true &&
      this.postModel.actualPost.postPrivacy == 1
    ) {
      this.sharingPostModalData.postId = this.postModel.actualPost.postId;
      this.sharingPostModalData.postType = this.postModel.actualPost.postType;
    }

    this.matDialog.open(ShareComponent);
  }
}
