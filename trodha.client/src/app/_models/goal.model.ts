// trodha.client/src/app/_models/goal.model.ts
export interface Goal {
  goalId: number;
  userId: number;
  title: string;
  description?: string;
  frequency: number;
  periodUnit: string;
  importanceLevelId: number;
  importanceLevelName: string;
  isActive: boolean;
  createdAt: Date;
  updatedAt: Date;
  completedCount: number;
  targetCount: number;
}

export interface CreateGoal {
  title: string;
  description?: string;
  frequency: number;
  periodUnit: string;
  importanceLevelId: number;
}

export interface UpdateGoal {
  title: string;
  description?: string;
  frequency: number;
  periodUnit: string;
  importanceLevelId: number;
  isActive: boolean;
}
