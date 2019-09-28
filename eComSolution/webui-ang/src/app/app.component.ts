import { Component } from '@angular/core';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'webui-ang';
  loggedInUser: string;

  constructor(private user: UserService) {
  }

  ngOnInit() {
    this.user.currentUser.subscribe(us => this.loggedInUser = us);
  }
}
