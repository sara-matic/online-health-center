import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthenticationFacadeService } from '../../domain/application-services/authentication-facade.service';
import { Router } from '@angular/router';

interface ILoginFormData {
  username: string;
  password: string;
}

@Component({
  selector: 'app-login-form',
  templateUrl: './login-form.component.html',
  styleUrls: ['./login-form.component.css']
})
export class LoginFormComponent {
  public loginForm: FormGroup;

  constructor(private authenticationService: AuthenticationFacadeService, private routerService: Router) {
    this.loginForm = new FormGroup({
      username: new FormControl('', [Validators.required, Validators.minLength(3)]),
      password: new FormControl('', [Validators.required, Validators.minLength(8)]),
    });
  }

  public onLoginFormSubmit(): void {
    if (this.loginForm.invalid) {

      if (this.loginForm.controls['username'].invalid) {
        window.alert('Username must be at least 3 characters long.');
      }
      else if (this.loginForm.controls['password'].invalid) {
        window.alert('Password must be at least 8 characters long.');
      }

      return;
    }

    const data: ILoginFormData = this.loginForm.value as ILoginFormData;
    this.authenticationService.login(data.username, data.password).subscribe((success: boolean) => {
      if (success) {
        window.alert('Login is successfull.');
        this.routerService.navigate(['/identity', 'profile']);
      }
      else {
        window.alert('Login is not successfull.\nMake sure you entered valid username and password.');
        this.loginForm.reset();
      }
    });
  }
}
