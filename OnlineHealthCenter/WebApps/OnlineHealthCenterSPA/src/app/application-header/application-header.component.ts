import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationFacadeService } from '../identity/domain/application-services/authentication-facade.service';
import { AppStateService } from '../common/app-state/app-state.service';
import { Observable } from 'rxjs';
import { IAppState } from '../common/app-state/app-state';
import { Role } from '../common/app-state/role';

@Component({
  selector: 'app-application-header',
  templateUrl: './application-header.component.html',
  styleUrls: ['./application-header.component.css']
})

export class ApplicationHeaderComponent {
  public loginButtonText!: string;
  public appState$: Observable<IAppState>;
  
  constructor(private routerService: Router, private authenticationService: AuthenticationFacadeService,
    private appStateService: AppStateService) {
    this.updateLoginButtonText();
    this.appState$ = this.appStateService.getAppState();
    this.authenticationService.isLoggedIn().subscribe((loggedIn: boolean) => {
      this.updateLoginButtonText();
    })
  }

  public onLoginButtonClick(): void {
    if (this.loginButtonText === 'Login/Register') {
      this.routerService.navigate(['/identity']);
    }
    else if (this.loginButtonText === 'Logout' && window.confirm("Logout is requested.\n\n Confirm or cancel.")) {
      this.routerService.navigate(['/identity', 'logout']);
    }
  }

  private updateLoginButtonText(): void {
    this.authenticationService.isLoggedIn().subscribe((loggedIn: boolean) => {
      this.loginButtonText = loggedIn ? 'Logout' : 'Login/Register';
    });
  }

  public isPatientLoggedIn(appState: IAppState): boolean {
    return appState?.hasRole(Role.Patient);
  }

  public isDoctorLoggedIn(appState: IAppState): boolean {
    return appState?.hasRole(Role.Doctor);
  }

  public isNurseLoggedIn(appState: IAppState): boolean {
    return appState?.hasRole(Role.Nurse);
  }

  public isStaffLoggedIn(appState: IAppState): boolean {
    return appState?.hasRole(Role.Doctor) || appState?.hasRole(Role.Nurse);
  }

  public isUserLoggedIn(appState: IAppState): boolean {
    return appState?.userId !== undefined;
  }
}