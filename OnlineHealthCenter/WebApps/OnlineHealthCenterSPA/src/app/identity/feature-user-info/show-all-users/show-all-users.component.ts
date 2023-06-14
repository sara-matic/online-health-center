import { Component } from '@angular/core';
import { UserFacadeService } from '../../domain/application-services/user-facade.service';
import { IUserDetails } from '../../domain/model/user-details';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-show-all-users',
  templateUrl: './show-all-users.component.html',
  styleUrls: ['./show-all-users.component.css']
})
export class ShowAllUsersComponent {
  public users!: IUserDetails[];
  public userFilter: string = '';

  constructor(private userService: UserFacadeService) {
    this.loadUsers();
  }

  private loadUsers(): void {
    this.userService.getAllUsers().subscribe((users: IUserDetails[]) => {
      this.filterUsers(users);
    },
    (error: any) => {
      console.error('Error fetching users: ', error);
      window.alert('Failed to retrieve users.');
    }
    );
  }

  public onUserFilterChange(): void {
    this.loadUsers();
  }

  private filterUsers(users: IUserDetails[]): void {
    if (this.userFilter !== '') {
      this.users = users.filter(user => user.firstName.concat(user.lastName).toLowerCase().includes(this.userFilter.toLowerCase())
      || user.lastName.concat(user.firstName).toLowerCase().includes(this.userFilter.toLowerCase())
    );
    }
    else {
      this.users = users;
    }
  }
}
