import { User } from './../../interfaces/user';
import { ApiTodoListService } from './../../services/api-todo-list.service';
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AxiosInstance } from 'axios';
import { ModalUserComponent } from 'src/app/components/modal-user/modal-user.component';

@Component({
  selector: 'app-perfil',
  templateUrl: './perfil.component.html',
  styleUrls: ['./perfil.component.css']
})
export class PerfilComponent {
  [x: string]: any;
  useUpdate: User[] = [];
  axiosInstance: AxiosInstance;
  modalUserComponent!: ModalUserComponent;
  listItemsUsers: User = {
      userId: 0,
      name: '',
      email: '',
      password: ''
    }

  constructor(private apiTodoListService: ApiTodoListService, private router: Router) {
    this.axiosInstance = apiTodoListService.getAxiosInstance();
    this.loadDataFromApiUser();
    this.listItemsUsers = {
      userId: 0,
      name: '',
      email: '',
      password: ''
    }
  }

  onLogout() {
    localStorage.removeItem('token');
    this.router.navigate(['/sign-in']);
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
  
  editUserPerfil(user: User) {
    this.listItemsUsers = user;
  }
  
  editUser(user: User) {
    this.listItemsUsers = user;
    this.listItemsUsers.password = '';
  }

  async updateTask(user: User) {
    await this.axiosInstance.put(`users/${user.userId}`, user);
  }

  loadUser() {
    this.loadDataFromApiUser();
    console.log('loadUser');
  }
}
