import { Component } from '@angular/core';
import { LoginActionEnum } from './LoginActionEnum';
import { Router } from '@angular/router';

@Component({
  selector: 'app-application-header',
  templateUrl: './application-header.component.html',
  styleUrls: ['./application-header.component.css']
})

export class ApplicationHeaderComponent {

  private loginType: LoginActionEnum;
  
  constructor(private routerService: Router)
  {
    this.loginType = LoginActionEnum.LoginRegister;
  }

  public onLoginButtonClick(): void
  {
    var element = document.getElementById('login-button');

    if (element?.textContent === 'Login/Register')
    {
      this.routerService.navigate(['/identity']);
    }
    else if (window.confirm("Logout is requested.\n\n Confirm or cancel."))
    {
      this.routerService.navigate(['/identity', 'logout']);
    }
  }
}