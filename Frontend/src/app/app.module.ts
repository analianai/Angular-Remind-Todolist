import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BoardComponent } from './pages/board/board.component';
import { SignInComponent } from './pages/sign-in/sign-in.component';
import { SignUpComponent } from './pages/sign-up/sign-up.component';
import { AlertComponent } from './components/alert/alert.component';
import { LazyLoadComponent } from './components/lazy-load/lazy-load.component';
import { FormsModule } from '@angular/forms';
import { ModalTaskComponent } from './components/modal-task/modal-task.component';
import { PerfilComponent } from './pages/perfil/perfil.component';
import { SobreComponent } from './pages/sobre/sobre.component';
import { ModalUserComponent } from './components/modal-user/modal-user.component';
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { PasswordModule } from "primeng/password";
import { StyleClassModule } from 'primeng/styleclass';
import { TopComponent } from './components/top/top.component';


@NgModule({
  declarations: [
    AppComponent,
    BoardComponent,
    SignInComponent,
    SignUpComponent,
    AlertComponent,
    LazyLoadComponent,
    ModalTaskComponent,
    PerfilComponent,
    SobreComponent,
    ModalUserComponent,
    TopComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    PasswordModule,
    StyleClassModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
