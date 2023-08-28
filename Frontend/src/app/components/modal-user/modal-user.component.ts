import { Component, EventEmitter, Input, Output, OnInit, Inject } from '@angular/core';
import { Router } from '@angular/router';
import { ApiTodoListService } from 'src/app/services/api-todo-list.service';
import { AxiosInstance } from 'axios';
import { User } from '../../interfaces/user';

@Component({
  selector: 'app-modal-user',
  templateUrl: './modal-user.component.html',
  styleUrls: ['./modal-user.component.css']
})
    
export class ModalUserComponent {
axiosInstance: AxiosInstance;
  showAlert: boolean = false;
  message: string = '';
  error: boolean = true;
 
  @Input() user: User = {
    userId: 0,
    name: '',
    email: '',
    password: ''
  }
  @Output() closeModalEvent = new EventEmitter<void>();

  constructor(private router: Router, private apiTodoListService: ApiTodoListService) {
    this.axiosInstance = apiTodoListService.getAxiosInstance();
    this.user= {
       userId: 0,
        name: '',
        email: '',
        password: ''
    }
  }

  showPassword: boolean = false;
 
  toggleShowPassword() {
    this.showPassword = !this.showPassword;
  }
  
  showPasswordConfirm: boolean = false;

  toggleShowPasswordConfirm() {
    this.showPasswordConfirm = !this.showPasswordConfirm;
  }

  confirmNewPassword!: string;

  async onSubmitPassword() {
    try {
      if (this.user.password === this.confirmNewPassword) {
        console.log(this.user);
        const response = await this.axiosInstance.put('/users/', this.user);
        this.showAlert = true;
        this.message = 'Senha alterada com sucesso!';
        this.error = false;
        window.location.href = '/perfil';
      } else {
        this.message = 'As senhas não correspondem';
        this.showAlert = true;
        this.error = true;
      }
    } catch (error: any) {
      console.log('Erro ao atualizar o usuario', error);
      this.message = error.response.data;
      this.showAlert = true;
      this.error = true;
    }
  }  
  async onSubmit() {
    try {
       console.log(this.user);
        const response = await this.axiosInstance.put('/users/', this.user);
        this.showAlert = true;
        this.message = 'Perfil Atualizado com sucesso!';
        this.error = false;
          window.location.href = '/perfil'; 
    } catch (error: any) {
        console.log('Erro ao atualizar o usuario', error);
        this.message = error.response.data;
        this.showAlert = true;
        this.error = true;
    }

    setTimeout(() => {
      this.showAlert = false;
      this.message = '';
    }, 10000)
  }

  onClose() {
    this.closeModalEvent.emit();
  }
}
