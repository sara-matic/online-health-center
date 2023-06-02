import { Component, OnDestroy } from '@angular/core';
import { AuthenticationFacadeService } from '../../domain/application-services/authentication-facade.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-logout',
  templateUrl: './logout.component.html',
  styleUrls: ['./logout.component.css']
})
export class LogoutComponent implements OnDestroy {
  public logoutSuccess: boolean = false;
  private sub: Subscription;

  constructor(private authenticationService: AuthenticationFacadeService) {
    this.sub = this.authenticationService.logout().subscribe((success: boolean) => {
      if (success) {
        this.logoutSuccess = true;
        var element = document.getElementById('login-button');
        element!.textContent = "Login/Register";
      }
    });
  }

  ngOnDestroy(): void {
    this.sub.unsubscribe();
  }
}