import { Component, Input } from '@angular/core';
import { Recommendation } from '../../_models/recommendation.model';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-recommendation-card',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './recommendation-card.component.html',
  styleUrl: './recommendation-card.component.css'
})
export class RecommendationCardComponent {
  @Input() recommendation!: Recommendation;
}
