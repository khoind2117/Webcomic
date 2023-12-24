import { HttpClient } from "@angular/common/http";
import { EnvironmentService } from "./environment.service";

export class ApiBaseService {
  protected apiUrl: string;

  constructor(
    environmentSerivce: EnvironmentService,
    protected http: HttpClient,
  ) {
    this.apiUrl = environmentSerivce.getApiUrl();
  }
}
