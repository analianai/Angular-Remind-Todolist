import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard {

  constructor(private router: Router) {  }

  canActivate(): boolean {
    const isAuthetication = localStorage.getItem('token');

    if (isAuthetication) {
      return true;
    } else {
      this.router.navigate(['']);
      return false;
    }
  }

}

