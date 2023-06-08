import { Component, OnDestroy } from '@angular/core';
import { AuthenticationFacadeService } from '../../domain/application-services/authentication-facade.service';
import { Subscription } from 'rxjs';
import { Router } from '@angular/router';

@Component({
  selector: 'app-logout',
  templateUrl: './logout.component.html',
  styleUrls: ['./logout.component.css']
})
export class LogoutComponent implements OnDestroy {
  public logoutSuccess: boolean = false;
  private sub: Subscription;

  constructor(private authenticationService: AuthenticationFacadeService, private routerService: Router) {
    this.sub = this.authenticationService.logout().subscribe((success: boolean) => {
      if (success) {
        this.logoutSuccess = true;
      }
    });
  }

  ngOnDestroy(): void {
    this.sub.unsubscribe();
  }
}