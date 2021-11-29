import {
  PostPicturesModalData,
  PostImage,
} from './../../../appWideServicesModules/modalsServices/postPicturesModalData';
import { PostPicturesComponent } from './../../../modalComponents/post-pictures/post-pictures.component';
import { MatDialog } from '@angular/material/dialog';
import { ImageTypeEnum } from 'src/app/nonAngularSpecific/httpApiModels/receivingModels/imageTypeEnum';
import { ImageModel } from './../../../../nonAngularSpecific/httpApiModels/receivingModels/imageModel';
import { OAuthService } from 'angular-oauth2-oidc';
import { PostModel } from './../../../../nonAngularSpecific/httpApiModels/receivingModels/postsModel';
import {
  AfterViewInit,
  Component,
  ElementRef,
  OnInit,
  ViewChild,
} from '@angular/core';

@Component({
  selector: 'app-images-post',
  templateUrl: './images-post.component.html',
  styleUrls: ['./images-post.component.css'],
})
export class ImagesPostComponent implements OnInit {
  imagesToShowUrls: string[] = [];
  imagesToShowRowSpan: number[] = [];
  imagesToShowColSpan: number[] = [];
  showImage: boolean[] = [];
  accessToken: string;
  imagesToShowModels: ImageModel[] = [];

  constructor(
    public postModel: PostModel,
    private oAuthService: OAuthService,
    private matDialog: MatDialog,
    private postPicturesModalData: PostPicturesModalData
  ) {
    this.accessToken = oAuthService.getAccessToken();
  }

  ngOnInit(): void {
    this.imagesToShowUrls = this.postModel.postImagesModels
      .slice(0, 3)
      .map((x) => {
        return x.imageUrl;
      });
    this.imagesToShowModels = this.postModel.postImagesModels;
    this.processImages();
  }

  processImages() {
    var firstImagesCount = this.imagesToShowUrls.length;
    if (firstImagesCount == 1) {
      this.imagesToShowRowSpan = [2, 0, 0];
      this.imagesToShowColSpan = [2, 0, 0];
      this.showImage = [true, false, false];
    } else if (firstImagesCount == 2) {
      if (
        this.getImageType(this.imagesToShowModels[0]) ==
          ImageTypeEnum.Portrait &&
        this.getImageType(this.imagesToShowModels[1]) == ImageTypeEnum.Portrait
      ) {
        this.imagesToShowRowSpan = [2, 2, 0];
        this.imagesToShowColSpan = [1, 1, 0];
        this.showImage = [true, true, false];
      } else if (
        this.getImageType(this.imagesToShowModels[0]) ==
          ImageTypeEnum.Landscape &&
        this.getImageType(this.imagesToShowModels[1]) == ImageTypeEnum.Landscape
      ) {
        this.imagesToShowRowSpan = [1, 1, 0];
        this.imagesToShowColSpan = [2, 2, 0];
        this.showImage = [true, true, false];
      } else if (
        this.getImageType(this.imagesToShowModels[0]) ==
          ImageTypeEnum.Portrait &&
        this.getImageType(this.imagesToShowModels[1]) == ImageTypeEnum.Landscape
      ) {
        this.imagesToShowRowSpan = [2, 1, 0];
        this.imagesToShowColSpan = [1, 1, 0];
        this.showImage = [true, true, false];
      } else if (
        this.getImageType(this.imagesToShowModels[0]) ==
          ImageTypeEnum.Landscape &&
        this.getImageType(this.imagesToShowModels[1]) == ImageTypeEnum.Portrait
      ) {
        this.imagesToShowRowSpan = [1, 2, 0];
        this.imagesToShowColSpan = [1, 1, 0];
        this.showImage = [true, true, false];
      }
    } else if (firstImagesCount == 3) {
      if (
        this.getImageType(this.imagesToShowModels[0]) == ImageTypeEnum.Landscape
      ) {
        this.imagesToShowRowSpan = [1, 1, 1];
        this.imagesToShowColSpan = [2, 1, 1];
        this.showImage = [true, true, true];
      } else {
        this.imagesToShowRowSpan = [2, 1, 1];
        this.imagesToShowColSpan = [1, 1, 1];
        this.showImage = [true, true, true];
      }
    }
  }

  getImageType(imageModel: ImageModel): ImageTypeEnum {
    return imageModel.imageHeight > imageModel.imageWidth
      ? ImageTypeEnum.Portrait
      : ImageTypeEnum.Landscape;
  }

  imagesClicked() {
    this.postPicturesModalData.postImages = [];
    this.postModel.postImagesModels.forEach((x) => {
      var postImage = new PostImage();
      postImage.url = x.imageUrl;
      postImage.privacy = this.postModel.postPrivacy;

      this.postPicturesModalData.postImages.push(postImage);
    });
    this.matDialog.open(PostPicturesComponent);
  }
}
