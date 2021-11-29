import { Component, Input, OnInit } from '@angular/core';
import { PostPrivacyEnum } from 'src/app/nonAngularSpecific/httpApiModels/receivingModels/postPrivacyEnum';

@Component({
  selector: 'app-post-privacy',
  templateUrl: './post-privacy.component.html',
  styleUrls: ['./post-privacy.component.css'],
})
export class PostPrivacyComponent implements OnInit {
  @Input()
  postPrivacy: PostPrivacyEnum;

  constructor() {}

  ngOnInit(): void {
    var x = this.postPrivacy;
    var y = 44;
  }
}
