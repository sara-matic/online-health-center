import { Injectable } from '@angular/core';
import { BehaviorSubject, catchError, map, Observable, of, switchMap, take } from 'rxjs';
import { AuthenticationService } from '../infrastructure/authentication.service';
import { ILoginRequest } from '../model/login-request';
import { ILoginResponse } from '../model/login-response';
import { AppStateService } from 'src/app/common/app-state/app-state.service';
import { JwtService } from 'src/app/common/jwt/jwt.service';
import { JwtPayloadKeys } from 'src/app/common/jwt/jwt-payload-keys';
import { UserFacadeService } from './user-facade.service';
import { IUserDetails } from '../model/user-details';
import { IAppState } from 'src/app/common/app-state/app-state';
import { ILogoutRequest } from '../model/logout-request';
import { IRefreshTokenRequest } from '../model/refresh-token-request';
import { IRefreshTokenResponse } from '../model/refresh-token-response';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationFacadeService {
  private readonly loggedInKey = 'loggedIn';
  private loginStatusSubject: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);

  constructor(
    private authenticationService: AuthenticationService, 
    private appStateService: AppStateService, 
    private jwtService: JwtService,
    private userService: UserFacadeService
  ) { 
    this.updateLoginStatus();
  }

  public login(username: string, password: string): Observable<boolean> {
    const request: ILoginRequest = { username, password };

    return this.authenticationService.login(request).pipe(
      switchMap((loginResponse: ILoginResponse) => {
        this.appStateService.setAccessToken(loginResponse.accessToken);
        this.appStateService.setRefreshToken(loginResponse.refreshToken);
        
        const payload = this.jwtService.parsePayload(loginResponse.accessToken);
        this.appStateService.setUsername(payload[JwtPayloadKeys.Username]);
        this.appStateService.setEmail(payload[JwtPayloadKeys.Email]);
        this.appStateService.setUserId(payload[JwtPayloadKeys.UserId]);
        this.appStateService.setRoles(payload[JwtPayloadKeys.Role]);

        return this.userService.getUserDetails(payload[JwtPayloadKeys.Username]);
      }),
      map((userDetails: IUserDetails) => {
        this.appStateService.setFirstName(userDetails.firstName);
        this.appStateService.setLastName(userDetails.lastName);

        localStorage.setItem(this.loggedInKey, 'true');
        this.updateLoginStatus();

        return true;
      }),
      catchError((err) => {
        console.log(err);
        this.appStateService.clearAppState();
        return of(false);
      })
    );
  }

  public logout(): Observable<boolean> {
    return this.appStateService.getAppState().pipe(
      take(1),
      map((appState: IAppState) => {
        const request: ILogoutRequest = { userName: appState.username as string, refreshToken: appState.refreshToken as string };
        return request;
      }),
      switchMap((request: ILogoutRequest) => this.authenticationService.logout(request)),
      map(() => {
        this.appStateService.clearAppState();

        localStorage.removeItem(this.loggedInKey);
        this.updateLoginStatus();

        return true;
      }),
      catchError((err) => {
        console.error(err);

        localStorage.removeItem(this.loggedInKey);
        this.updateLoginStatus();

        return of(false);
      })
    );
  }

  public refreshToken(): Observable<string | null> {
    return this.appStateService.getAppState().pipe(
      take(1),
      map((appState: IAppState) => {
        const request: IRefreshTokenRequest = { userName: appState.username as string, refreshToken: appState.refreshToken as string };
        return request;
      }),
      switchMap((request: IRefreshTokenRequest) => this.authenticationService.refreshToken(request)),
      map((response: IRefreshTokenResponse) => {
        this.appStateService.setAccessToken(response.accessToken);
        this.appStateService.setRefreshToken(response.refreshToken);

        localStorage.setItem(this.loggedInKey, 'true');
        this.updateLoginStatus();

        return response.accessToken;
      }),
      catchError((err) => {
        console.log(err);
        this.appStateService.clearAppState();

        localStorage.removeItem(this.loggedInKey);
        this.updateLoginStatus();

        return of(null);
      })
    );
  }

  public isLoggedIn(): Observable<boolean> {
    return this.loginStatusSubject.asObservable();
  }

  private updateLoginStatus(): void {
    const loggedIn = localStorage.getItem(this.loggedInKey) === 'true';
    this.loginStatusSubject.next(loggedIn);
  }
}
