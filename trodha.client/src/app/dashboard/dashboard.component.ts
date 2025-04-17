import { Component, OnInit } from '@angular/core';
import { User } from '../_models/user.model';
import { Goal } from '../_models/goal.model';
import { Recommendation } from '../_models/recommendation.model';
import { AuthService } from '../_services/auth.service';
import { GoalService } from '../_services/goal.service';
import { RecommendationService } from '../_services/recommendation.service';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { RecommendationCardComponent } from '../shared/recommendation-card/recommendation-card.component';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css'],
  standalone: true,
  imports: [CommonModule, RouterModule, RecommendationCardComponent]
})

export class DashboardComponent implements OnInit {
  currentUser: User | null = null;
  activeGoals: Goal[] = [];
  recommendation: Recommendation | null = null;
  statistics: Record<string, number> = {};
  loading = true;
  error = '';

  constructor(
    private authService: AuthService,
    private goalService: GoalService,
    private recommendationService: RecommendationService
  ) { }

  ngOnInit(): void {
    this.currentUser = this.authService.currentUserValue;
    this.loadDashboardData();
  }

  loadDashboardData(): void {
    this.loading = true;

    // Aktif hedefleri getir
    this.goalService.getActiveGoals().subscribe({
      next: goals => {
        this.activeGoals = goals;
        this.loading = false;
      },
      error: error => {
        this.error = 'Hedefler yüklenirken hata oluştu';
        this.loading = false;
      }
    });

    // Rastgele öneri getir
    this.recommendationService.getRandomUserRecommendation().subscribe({
      next: recommendation => {
        this.recommendation = recommendation;
      },
      error: error => {
        console.error('Öneri yüklenirken hata oluştu', error);
      }
    });

    // İstatistikleri getir
    this.goalService.getGoalStatistics().subscribe({
      next: stats => {
        this.statistics = stats;
      },
      error: error => {
        console.error('İstatistikler yüklenirken hata oluştu', error);
      }
    });
  }
}
