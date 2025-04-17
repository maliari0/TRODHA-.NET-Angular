// trodha.client/src/app/_models/goal-status.model.ts
export interface GoalStatus {
  statusId: number;
  goalId: number;
  goalTitle: string;
  userId: number;
  recordDate: Date;
  recordTime: string;
  duration?: number;
  isCompleted: boolean;
  notes?: string;
  createdAt: Date;
}

export interface CreateGoalStatus {
  goalId: number;
  recordDate: Date;
  recordTime: string;
  duration?: number;
  isCompleted: boolean;
  notes?: string;
}

export interface GoalStatusReport {
  goalTitle: string;
  targetCount: number;
  completedCount: number;
  completionPercentage: number;
}
