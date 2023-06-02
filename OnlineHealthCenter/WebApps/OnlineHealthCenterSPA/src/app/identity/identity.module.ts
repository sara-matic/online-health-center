import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { IdentityRoutingModule } from './identity-routing.module';
import { IdentityComponent } from './identity.component';
import { LoginFormComponent } from './feature-authentication/login-form/login-form.component';
import { ReactiveFormsModule } from '@angular/forms';
import { MatDividerModule } from '@angular/material/divider';
import { UserProfileComponent } from './feature-user-info/user-profile/user-profile.component';
import { LogoutComponent } from './feature-authentication/logout/logout.component';
import { RegistrationFormComponent } from './feature-authentication/registration-form/registration-form.component';

@NgModule({
  declarations: [
    IdentityComponent,
    LoginFormComponent,
    UserProfileComponent,
    LogoutComponent,
    RegistrationFormComponent
  ],
  imports: [
    CommonModule,
    IdentityRoutingModule,
    ReactiveFormsModule,
    MatDividerModule
  ]
})
export class IdentityModule { }
