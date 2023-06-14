import { Injectable } from '@angular/core';
import { UserService } from '../infrastructure/user.service';
import { IUserDetails } from '../model/user-details';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserFacadeService {

  constructor(private userService: UserService) { }

  public getUserDetails(username: string): Observable<IUserDetails> {
    return this.userService.getUserDetails(username);
  }

  public searchUsersByName(name: string): Observable<IUserDetails[]> {
    return this.userService.searchUsersByName(name);
  }

  public getAllUsers(): Observable<IUserDetails[]> {
    return this.userService.getAllUsers();
  }
}
