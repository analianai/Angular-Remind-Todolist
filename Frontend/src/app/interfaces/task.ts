import { User } from "./user";

export interface Task {
  taskId: number;
  title: string;
  description: string;
  status: string;
  userId: number;
  user?: User;
}

export interface TaskCreate {
  title: string;
  description: string;
  status: string;
}

export interface TaskUpdate {
  title: string;
  description: string;
  status: string;
  userId: number;
}

