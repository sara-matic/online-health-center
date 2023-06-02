import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { IAppState } from 'src/app/common/app-state/app-state';
import { AppStateService } from 'src/app/common/app-state/app-state.service';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css']
})
export class UserProfileComponent {
  public appState$: Observable<IAppState>;

  constructor(private appStateService: AppStateService) {
    this.appState$ = this.appStateService.getAppState();
  }
}