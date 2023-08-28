import { Component, EventEmitter, Input, Output, OnInit, Inject } from '@angular/core';
import { Task, TaskCreate } from 'src/app/interfaces/task';
import { Router } from '@angular/router';
import { ApiTodoListService } from 'src/app/services/api-todo-list.service';
import { AxiosInstance } from 'axios';

@Component({
  selector: 'app-modal-task',
  templateUrl: './modal-task.component.html',
  styleUrls: ['./modal-task.component.css']
})
export class ModalTaskComponent {
  axiosInstance: AxiosInstance;
  showAlert: boolean = false;
  message: string = '';
  error: boolean = true;
  listItems: Task[] = [];

  @Input() task: Task = {
    userId: 0,
    title: '',
    description: '',
    status: '',
    taskId: 0
  }
  @Output() closeModalEvent = new EventEmitter<void>();

  constructor(private router: Router, private apiTodoListService: ApiTodoListService) {
    this.axiosInstance = apiTodoListService.getAxiosInstance();
    this.loadDataFromApi();
  }

   async onSubmitNew() {
    try {
      console.log(this.task);

      if (this.task.taskId === 0) {
        const response = await this.axiosInstance.post('/tasks', this.task);
        if (response.status === 200) {
          this.showAlert = true;
          this.message = 'Tarefa cadastrada com sucesso!';
          this.error = false;
          this.task = {
            title: '',
            description: '',
            status: 'Planejado',
            taskId: 0,
            userId: 0
          }
        }
      } 
    } catch(error: any) {
      this.message = error.response.data;
      this.showAlert = true;
      this.error = true;
    }

    setTimeout(() => {
      this.showAlert = false;
      this.message = '';
    }, 10000)
  }

  async onSubmit() {
    try {
      console.log(this.task);
        const response = await this.axiosInstance.put(`/tasks/${this.task.taskId}`, this.task);
        if (response.status === 200) {
            this.showAlert = true;
            this.message = 'Tarefa Atualizada com sucesso!';
            this.error = false;
            this.closeModalEvent.emit();
            window.location.href = '/board';
        }
    } catch(error: any) {
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
  onCloseNew() {
    window.location.href = '/board';
  }
  newTask() {
    this.task = {
            title: '',
            description: '',
            status: 'Planejado',
            taskId: 0,
            userId: 0
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
  
  async deleteTaskNow(task: Task) {
    try {
          const response = await this.axiosInstance.delete(`/tasks/${task.taskId}`);
          this.loadDataFromApi();
          this.showAlert = true;
          this.message = 'Tarefa Deletada com sucesso!'
          window.location.href = '/board';
      } catch (error) {
        console.log(error);
    }
  }

}
