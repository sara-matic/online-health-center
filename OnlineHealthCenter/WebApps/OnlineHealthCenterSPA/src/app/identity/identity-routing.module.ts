import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { IdentityComponent } from './identity.component';
import { UserProfileComponent } from './feature-user-info/user-profile/user-profile.component';
import { LogoutComponent } from './feature-authentication/logout/logout.component';
import { RegistrationFormComponent } from './feature-authentication/registration-form/registration-form.component';

const routes: Routes = [
  { path: '', component: IdentityComponent },
  { path: 'profile', component: UserProfileComponent },
  { path: 'logout', component: LogoutComponent },
  { path: 'register', component: RegistrationFormComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class IdentityRoutingModule { }
