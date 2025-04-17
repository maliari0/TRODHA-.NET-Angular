// trodha.client/src/app/_services/goal.service.ts
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CreateGoal, Goal, UpdateGoal } from '../_models/goal.model';

@Injectable({
  providedIn: 'root'
})
export class GoalService {
  private apiUrl = '/api/goals';

  constructor(private http: HttpClient) { }

  getGoals(): Observable<Goal[]> {
    return this.http.get<Goal[]>(this.apiUrl);
  }

  getActiveGoals(): Observable<Goal[]> {
    return this.http.get<Goal[]>(`${this.apiUrl}/active`);
  }

  getGoal(id: number): Observable<Goal> {
    return this.http.get<Goal>(`${this.apiUrl}/${id}`);
  }

  createGoal(goal: CreateGoal): Observable<Goal> {
    return this.http.post<Goal>(this.apiUrl, goal);
  }

  updateGoal(id: number, goal: UpdateGoal): Observable<Goal> {
    return this.http.put<Goal>(`${this.apiUrl}/${id}`, goal);
  }

  deleteGoal(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }

  toggleGoalStatus(id: number, isActive: boolean): Observable<any> {
    return this.http.patch(`${this.apiUrl}/${id}/toggle`, isActive);
  }

  getGoalStatistics(): Observable<Record<string, number>> {
    return this.http.get<Record<string, number>>(`${this.apiUrl}/statistics`);
  }
}
