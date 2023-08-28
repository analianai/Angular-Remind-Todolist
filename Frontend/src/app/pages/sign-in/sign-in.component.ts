import { AxiosInstance } from 'axios';
import { ApiTodoListService } from './../../services/api-todo-list.service';
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { UserLogin } from 'src/app/interfaces/user';

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.css']
})
export class SignInComponent {
  user: UserLogin;
  axiosInstance: AxiosInstance;
  showAlert: boolean = false;
  error: boolean = true;
  showLoad: boolean = false;
  message: string = '';
  showPassword: boolean = false;

  constructor(private router: Router, private apiTodoListService: ApiTodoListService) {
    this.user = {
      email: '',
      password: ''
    }
    this.axiosInstance = apiTodoListService.getAxiosInstance();

    const token = localStorage.getItem('token')
    if (token) {
      this.router.navigate(['/board']);
    }
  }

    toggleShowPassword() {
    this.showPassword = !this.showPassword;
  }

  async onSubmit() {
    this.showLoad = true;
    try {
      const response = await this.axiosInstance.post('/users/auth', this.user);

      if (response.status === 200) {
        this.showLoad = false;
        localStorage.setItem('token', response.data.token);
        this.router.navigate(['/board']);
      }
    } catch(error: any) {
      this.showLoad = false;
      this.message = error.response.data;
      this.showAlert = true;

      setTimeout(() => {
        this.showAlert = false;
        this.message = '';
      }, 10000)
    }
  }
}
