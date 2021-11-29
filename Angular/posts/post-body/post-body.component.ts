import { PostModel } from './../../../../nonAngularSpecific/httpApiModels/receivingModels/postsModel';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-post-body',
  templateUrl: './post-body.component.html',
  styleUrls: ['./post-body.component.css'],
})
export class PostBodyComponent implements OnInit {
  constructor(public postModel: PostModel) {}

  ngOnInit(): void {}
}
