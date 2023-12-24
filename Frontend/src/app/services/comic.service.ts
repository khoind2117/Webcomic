import { Injectable } from "@angular/core";
import { EnvironmentService } from "./environment.service";
import { HttpClient, HttpParams } from "@angular/common/http";
import { ApiBaseService } from "./api-base.service";
import { Comic } from "../models/comic";
import { Observable } from "rxjs";
import { ApiResponse } from "../models/ApiResponse";

@Injectable({ providedIn: 'root' })
export class ComicService extends ApiBaseService {
  constructor(
    environementService: EnvironmentService,
    http: HttpClient) {
    super(environementService, http);
  }

  getAllComicsByTag(tagId: number, page: number): Observable<ApiResponse<Comic>> {
    return this.http.get<ApiResponse<Comic>>(`${this.apiUrl}/Comic/get-all-comics-by-tag`, {
      params: {
        tagId: tagId.toString(),
        page: page.toString()
      }
    });
  }
}
