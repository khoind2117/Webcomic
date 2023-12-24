import { Component, OnInit } from '@angular/core';
import { Tag } from 'src/app/models/tag';
import { TagService } from 'src/app/services/tag.service';

@Component({
  selector: 'app-tag-list',
  templateUrl: './tag-list.component.html',
  styleUrls: ['./tag-list.component.css']
})
export class TagListComponent implements OnInit {
  tags: Tag[] = [];

  constructor(private tagService: TagService) {}

  ngOnInit(): void {
    this.tagService.getAllTags().subscribe(tags => {
      this.tags = tags;
      // console.log(tags);
    });
  }

}
