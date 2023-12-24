import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { EnvironmentService } from './environment.service';
import { ApiBaseService } from './api-base.service';

@Injectable({ providedIn: 'root' })

export class AuthService extends ApiBaseService {
    constructor(
        environementService: EnvironmentService,
        http: HttpClient) {
        super(environementService, http);
      }

  login(credentials: any): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/Auth/login`, credentials);
  }

  register(userInfo: any): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/Auth/register`, userInfo);
  }
}
