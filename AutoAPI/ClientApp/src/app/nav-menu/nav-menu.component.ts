import { Component } from '@angular/core';
import { AuthService } from '../services/auth/auth.service';
import { OwnAPI } from '../services/ownapi/ownapi.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  isExpanded = false;

  constructor(public auth: AuthService, public http: OwnAPI) {
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
