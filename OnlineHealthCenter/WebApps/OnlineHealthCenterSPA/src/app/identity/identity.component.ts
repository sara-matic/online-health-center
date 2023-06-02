import { Component } from '@angular/core';

@Component({
  selector: 'app-identity',
  templateUrl: './identity.component.html',
  styleUrls: ['./identity.component.css']
})
export class IdentityComponent {
  loginFormVisibility: boolean = false;
  registerFormVisibility: boolean = false;

  showLoginForm(): void {
    this.loginFormVisibility = !this.loginFormVisibility;
    this.registerFormVisibility = false;
  }

  showRegisterForm(): void {
    this.registerFormVisibility = !this.registerFormVisibility;
    this.loginFormVisibility = false;
  }
}
