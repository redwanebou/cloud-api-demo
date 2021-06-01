import { Component, Inject } from '@angular/core';
import { GetCarService } from '../services/getcar/getcar.service';
import { GetCarHttpService } from '../services/getcar/getcar-http.service';
import { OwnAPI } from '../services/ownapi/ownapi.service';
import { AuthService } from '../services/auth/auth.service';
import { Person } from '../services/ownapi/models/person.model';

@Component({
  selector: 'app-geld',
  templateUrl: './geld.component.html'
})
export class GeldComponent  {
  status: number;

  constructor(public http: OwnAPI, public service: GetCarService, public http1: GetCarHttpService, private http2: AuthService) {
  }

  UpdateGeld(geld: number) {
    if (isEmpty(geld)) {
      this.status = 1;
    } else {
      const pr: Person = {
        user_id: this.http2.Information.sub,
        email: this.http2.Information.email,
        nickname: this.http2.Information.nickname,
        geld: +this.http.persoon.geld + +geld // update geld
      }
      this.http.UpdateGeld(pr).subscribe();
      this.status = 2;
      refresh();
    }
  }
}
function isEmpty(str) {
  return (!str || str.length === 0);
}
function refresh() {
  setTimeout(function () {
    window.location.reload();
  }, 500);
}
