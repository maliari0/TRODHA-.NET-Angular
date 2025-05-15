// trodha.client/src/app/_services/recommendation.service.ts
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Recommendation } from '../_models/recommendation.model';

@Injectable({
  providedIn: 'root'
})
export class RecommendationService {
  private apiUrl = 'http://localhost:5253/api/recommendations';

  constructor(private http: HttpClient) { }

  getRandomRecommendation(): Observable<Recommendation> {
    return this.http.get<Recommendation>(`${this.apiUrl}/random`);
  }

  getRandomUserRecommendation(): Observable<Recommendation> {
    return this.http.get<Recommendation>(`${this.apiUrl}/random/user`);
  }

  getUserRecommendations(): Observable<Recommendation[]> {
    return this.http.get<Recommendation[]>(`${this.apiUrl}/user`);
  }
}
// trodha.client/src/app/_helpers/
