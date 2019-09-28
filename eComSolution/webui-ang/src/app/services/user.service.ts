import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable()
export class UserService {

  private userSession = new BehaviorSubject('');
  currentUser = this.userSession.asObservable();

  constructor() {
  }

  assignUser(userId: string) {
    this.userSession.next(userId);
  }
}
