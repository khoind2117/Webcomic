import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ApiResponse } from 'src/app/models/ApiResponse';
import { Comic } from 'src/app/models/comic';
import { ComicService } from 'src/app/services/comic.service';

@Component({
  selector: 'app-truyen-theo-tag',
  templateUrl: './truyen-theo-tag.component.html',
  styleUrls: ['./truyen-theo-tag.component.css']
})
export class TruyenTheoTagComponent implements OnInit {
  comicsResponse!: ApiResponse<Comic>
  tagId!: number;
  pageNumber = 1; // Số trang mặc định

  constructor(
    private route: ActivatedRoute,
    private comicService: ComicService
  ) { }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      const tagId = +params['id'];
      const page = +params['page'];
  
      this.comicService.getAllComicsByTag(tagId, page).subscribe((response: ApiResponse<Comic>) => {
        this.comicsResponse = response;
        // console.log(this.comicsResponse);
      });
    });
  }
}
