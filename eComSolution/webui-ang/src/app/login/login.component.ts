import { Component, OnInit } from '@angular/core';
import { Router } from "@angular/router";
import { HttpClient } from '@angular/common/http';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  public userEmailText: string;

  constructor(private user: UserService, private router: Router, private http: HttpClient) { }

  ngOnInit() {
  }

  public onLogin() {
    this.user.assignUser(this.userEmailText);
    this.router.navigateByUrl(`/home`).then((e) => {
    });
  }
}
