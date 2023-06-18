import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'SPA';

  constructor(private router: Router) {
    const currentPath = this.router.url;
    if (currentPath === '') {
      this.router.navigateByUrl('/start-page');
    }
  }
}
