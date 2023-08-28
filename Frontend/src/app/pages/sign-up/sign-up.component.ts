import { Component } from '@angular/core';
import { UserCreate } from 'src/app/interfaces/user';
import { Router } from '@angular/router';
import { ApiTodoListService } from 'src/app/services/api-todo-list.service';
import { AxiosInstance } from 'axios';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.css']
})
export class SignUpComponent {
  user: UserCreate;
  axiosInstance: AxiosInstance;
  showAlert: boolean = false;
  error: boolean = true;
  message: string = '';
  showLoad!: boolean;

  constructor(private router: Router, private apiTodoListService: ApiTodoListService) {
    this.user = {
      name: '',
      email: '',
      password: ''
    }
    this.axiosInstance = apiTodoListService.getAxiosInstance();
    
    const token = localStorage.getItem('token')
    if (token) {
      this.router.navigate(['/board']);
      
    }
  }

   showPassword: boolean = false;

  toggleShowPassword() {
    this.showPassword = !this.showPassword;
  }

  async onSubmit() {
    this.showLoad = true;
    try {
      const response = await this.axiosInstance.post('/users', this.user);

      if (response.status === 200) {
        this.router.navigate(['/sign-in']);
      }
    
    } catch(error: any) {
      this.message = error.response.data;
      this.showAlert = true;

      setTimeout(() => {
        this.showAlert = false;
        this.message = '';
      }, 10000)

    }
  }
}