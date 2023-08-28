import { Injectable } from '@angular/core';
import axios, { AxiosInstance } from 'axios';

@Injectable({
  providedIn: 'root'
})
export class ApiTodoListService {
  axiosInstance: AxiosInstance;

  constructor() {
    this.axiosInstance = axios.create({
      baseURL: 'https://apieqp1.azurewebsites.net/',
      timeout: 5000,
      headers: {
        'Content-Type': 'application/json'
      }
    })
  }
  
// https://apieqp1.azurewebsites.net/
     
  getAxiosInstance(): AxiosInstance {
    const token = localStorage.getItem('token');
    if (token) {
      this.axiosInstance.defaults.headers.common['Authorization'] = `Bearer ${token}`;
    }
    return this.axiosInstance;
  }

  
}
