import { Component } from '@angular/core';
import { AuthenticationFacadeService } from '../../domain/application-services/authentication-facade.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-logout',
  templateUrl: './logout.component.html',
  styleUrls: ['./logout.component.css']
})
export class LogoutComponent {
  public logoutSuccess$: Observable<boolean>;

  constructor(private authenticationService: AuthenticationFacadeService) {
    this.logoutSuccess$ = this.authenticationService.logout();
  }
}
