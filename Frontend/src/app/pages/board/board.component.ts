import { User } from './../../interfaces/user';
import { ModalTaskComponent } from './../../components/modal-task/modal-task.component';
import { AxiosInstance } from 'axios';
import { Task } from './../../interfaces/task';
import { ApiTodoListService } from './../../services/api-todo-list.service';
import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-board',
  templateUrl: './board.component.html',
  styleUrls: ['./board.component.css']
})
export class BoardComponent {
  [x: string]: any;
  listTitle: string[] = [ 'Planejado', 'Em Andamento', 'Concluído' ];
  listItems: Task[] = [];
  axiosInstance: AxiosInstance;
  showAlert: boolean = false;
  showLoad: boolean = false;
  message: string = '';
  taskUpdate: Task;
  modalTaskComponent!: ModalTaskComponent;
  user: User;
   listItemsUsers: User = {
      userId: 0,
      name: '',
      email: '',
      password: ''
    }
 
  constructor(private apiTodoListService: ApiTodoListService, private router: Router) {
    this.axiosInstance = apiTodoListService.getAxiosInstance();
    this.loadDataFromApi();
    this.loadDataFromApiUser();
    this.taskUpdate = {
      userId: 0,
      title: '',
      description: '',
      status: '',
      taskId: 0
    }
    this.user = {
      userId: 0,
      name: '',
      email: '',
      password: ''
    }
  }
  
  onLogout() {
    localStorage.removeItem('token');
    this.router.navigate(['']);
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
  
  async loadDataFromApi() {
    console.log('carregando Task...');
    try {
      const response = await this.axiosInstance.get('tasks');
      this.listItems = response.data;
      console.log(response);
    } catch (error: any) {
      console.log('Erro de Acesso', error);
    }
  }

  onDragOver(event: DragEvent) {
    event.preventDefault();
  }

  onDrop(event: any) {
    event.preventDefault();
    const dataTransfer = event.dataTransfer as DataTransfer;
    const itemId = dataTransfer.getData('text/plain');
    const itemIndex = this.listItems.findIndex((item) => item.taskId.toString() === itemId);
    if (itemIndex !== -1) {
      const listIndex = this.listTitle.findIndex((title) =>
        event.currentTarget?.querySelector('.title')?.textContent?.includes(title)
      );
      this.listItems[itemIndex].status = this.listTitle[listIndex];
      this.updateTask(this.listItems[itemIndex]);
    }
  }

  editTask(task: Task) {
    this.taskUpdate = task;
  }

  deleteTask(task: Task) {
    this.taskUpdate = task;
  }

  async updateTask(task: Task) {
    await this.axiosInstance.put(`tasks/${task.taskId}`, task);
  }

  onDragStart(event: DragEvent, itemId: number) {
    event.dataTransfer?.setData('text/plain', itemId.toString());
  }

  getListItems(status: string) {
    return this.listItems.filter((item) => item.status === status);
  }

  loadTask() {
    this.loadDataFromApi();
    console.log('loadTask');
  }
}
