import { Injectable } from "@angular/core";
import { EnvironmentService } from "./environment.service";
import { HttpClient } from "@angular/common/http";
import { ApiBaseService } from "./api-base.service";
import { Comic } from "../models/comic";
import { Observable } from "rxjs";
import { Tag } from "../models/tag";

@Injectable({ providedIn: 'root' })
export class TagService extends ApiBaseService {
  constructor(
    environementService: EnvironmentService,
    http: HttpClient) {
    super(environementService, http);
  }

  getAllTags(): Observable<Tag[]> {
    return this.http.get<Tag[]>(this.apiUrl + "/Tag/get-all-tags");
  }
}
