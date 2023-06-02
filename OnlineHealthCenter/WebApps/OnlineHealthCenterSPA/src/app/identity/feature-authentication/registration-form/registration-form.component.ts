import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Role } from 'src/app/common/app-state/role';
import { RegistrationFacadeService } from '../../domain/application-services/registration-facade.service';

interface IRegistrationFormData {
  firstName: string;
  lastName: string;
  username: string;
  password: string;
  email: string;
  role: string;
}

@Component({
  selector: 'app-registration-form',
  templateUrl: './registration-form.component.html',
  styleUrls: ['./registration-form.component.css']
})
export class RegistrationFormComponent {
  public registrationForm: FormGroup;
  public roles = Role;

  constructor(private registrationService: RegistrationFacadeService) {
    this.registrationForm = new FormGroup({
      firstName: new FormControl('', [Validators.required, Validators.minLength(2)]),
      lastName: new FormControl('', [Validators.required, Validators.minLength(2)]),
      username: new FormControl('', [Validators.required, Validators.minLength(3)]),
      password: new FormControl('', [Validators.required, Validators.minLength(8)]),
      email: new FormControl('', [Validators.required, Validators.email]),
      role: new FormControl('', [Validators.required]),
    });
  }

  public onRegistrationFormSubmit(): void {
    if (this.registrationForm.invalid) {

      if (this.registrationForm.controls['firstName'].invalid) {
        window.alert('First name must be at least 2 characters long.');
      }
      else if (this.registrationForm.controls['lastName'].invalid) {
        window.alert('Last name must be at least 2 characters long.');
      }
      else if (this.registrationForm.controls['username'].invalid) {
        window.alert('Username must be at least 3 characters long.');
      }
      else if (this.registrationForm.controls['password'].invalid) {
        window.alert('Password must be at least 8 characters long.');
      }
      else if (this.registrationForm.controls['email'].invalid) {
        window.alert('Email field is invalid');
      }
      else if (this.registrationForm.controls['role'].invalid) {
        window.alert('Please select a role.');
      }

      return;
    }

    const data: IRegistrationFormData = this.registrationForm.value as IRegistrationFormData;
    this.registrationService
      .register(data.firstName, data.lastName, data.role, data.username, data.password, data.email)
      .subscribe((errorMessage: string | null) => {
        if (errorMessage !== null) {
          window.alert(errorMessage);
        }
        else {
          window.alert('Registration is successfull.');
          this.registrationForm.reset();
        }
      });
  }
}