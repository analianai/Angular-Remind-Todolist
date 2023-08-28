import { User } from './../../interfaces/user';
import { AxiosInstance } from 'axios';
import { ApiTodoListService } from './../../services/api-todo-list.service';
import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-top',
  templateUrl: './top.component.html',
  styleUrls: ['./top.component.css']
})
  
export class TopComponent {
axiosInstance: AxiosInstance;
  showAlert: boolean = false;
  message: string = '';
  error: boolean = true;
  listItemsUsers: User = {
              userId: 0,
              name: '',
              email: '',
              password: ''
    }
 

  constructor(private router: Router, private apiTodoListService: ApiTodoListService) {
    this.axiosInstance = apiTodoListService.getAxiosInstance();
    this.loadDataFromApiUser();
  }
 
  async loadDataFromApiUser() { 
    console.log('carregando User...');
    try {
      const responseUser = await this.axiosInstance.get('users');
      this.listItemsUsers = responseUser.data;
      console.log(responseUser);
      } catch (error: any) {
      console.log('Erro de Acesso', error);
    }
  }

  onLogout() {
    localStorage.removeItem('token');
    this.router.navigate(['/sign-in']);
  }
  
}
