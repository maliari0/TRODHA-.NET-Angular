// trodha.client/src/app/_models/recommendation.model.ts
export interface Recommendation {
  recommendationId: number;
  content: string;
  userId?: number;
  isActive: boolean;
  createdAt: Date;
}
