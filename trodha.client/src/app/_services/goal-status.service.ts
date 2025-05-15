// trodha.client/src/app/_services/goal-status.service.ts
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CreateGoalStatus, GoalStatus, GoalStatusReport } from '../_models/goal-status.model';

@Injectable({
  providedIn: 'root'
})
export class GoalStatusService {
  private apiUrl = 'http://localhost:5253/api/goal-statuses';

  constructor(private http: HttpClient) { }

  getByGoalId(goalId: number): Observable<GoalStatus[]> {
    return this.http.get<GoalStatus[]>(`${this.apiUrl}/goal/${goalId}`);
  }

  getAllStatuses(): Observable<GoalStatus[]> {
    return this.http.get<GoalStatus[]>(this.apiUrl);
  }

  getByDateRange(startDate: Date, endDate: Date): Observable<GoalStatus[]> {
    return this.http.get<GoalStatus[]>(`${this.apiUrl}/date-range?startDate=${startDate.toISOString()}&endDate=${endDate.toISOString()}`);
  }

  create(status: CreateGoalStatus): Observable<GoalStatus> {
    return this.http.post<GoalStatus>(this.apiUrl, status);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }

  getReport(startDate: Date, endDate: Date): Observable<GoalStatusReport[]> {
    return this.http.get<GoalStatusReport[]>(`${this.apiUrl}/report?startDate=${startDate.toISOString()}&endDate=${endDate.toISOString()}`);
  }
}
