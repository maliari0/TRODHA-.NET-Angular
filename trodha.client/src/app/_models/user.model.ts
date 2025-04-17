export interface User {
  userId: number;
  email: string;
  firstName: string;
  lastName: string;
  birthDate: Date;
}

export interface RegisterRequest {
  email: string;
  password: string;
  firstName: string;
  lastName: string;
  birthDate: string;
}

export interface LoginRequest {
  email: string;
  password: string;
}

export interface AuthResponse {
  user: User;
  token: string;
}
