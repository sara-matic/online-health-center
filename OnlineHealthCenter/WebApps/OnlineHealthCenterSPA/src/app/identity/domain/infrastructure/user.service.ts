import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IUserDetails } from '../model/user-details';
import { Observable, switchMap, take } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private readonly url: string = 'http://localhost:4000/api/v1/User';

  constructor(private httpClient: HttpClient) { }

  public getUserDetails(username: string): Observable<IUserDetails> {
    return this.httpClient.get<IUserDetails>(`${this.url}/${username}`);
  }

  public searchUsersByName(name: string): Observable<IUserDetails[]> {
    let params = new HttpParams()
      .set('name', name);
    return this.httpClient.get<IUserDetails[]>(`${this.url}/SearchUsersByName`, { params });
  }

  public getAllUsers(): Observable<IUserDetails[]> {
    return this.httpClient.get<IUserDetails[]>(`${this.url}/GetAllUsers`);
  }
}
