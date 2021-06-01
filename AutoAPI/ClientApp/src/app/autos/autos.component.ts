import { Component, Inject } from '@angular/core';
import { GetCarService } from '../services/getcar/getcar.service';
import { GetCarHttpService } from '../services/getcar/getcar-http.service';
import { OwnAPI } from '../services/ownapi/ownapi.service';
import { AuthService } from '../services/auth/auth.service';
import { Person } from '../services/ownapi/models/person.model';
import { Car } from '../services/ownapi/models/car.model';

@Component({
  selector: 'app-autos',
  templateUrl: './autos.component.html'
})
export class AutosComponent  {
  status: number;

  constructor(public http: OwnAPI, public service: GetCarService, public http1: GetCarHttpService, public http2: AuthService) {
    http.GetCar();
  }

  CheckUser(id: string): boolean {
    if (this.http2.Information.sub == id)
      return true;
    else
      return false;
  }

  DeleteCar(id: number) {
    this.http.DeleteCar(id).subscribe();
    this.status = 1;
    refresh();
  }

  BuyCar(car: Car) {
    if (this.http.persoon.geld < car.prijs) {
      this.status = 2;
    } else {
      const pr: Person = {
        user_id: this.http2.Information.sub,
        email: this.http2.Information.email,
        nickname: this.http2.Information.nickname,
        geld: +this.http.persoon.geld + -car.prijs // update geld
      }

      const cr: Car = {
        id: car.id,
        merk: car.merk,
        model: car.model,
        bouwjaar: car.bouwjaar,
        brandstof: car.brandstof,
        prijs: car.prijs,
        verkocht: true,
        person: this.http.persoon
      }
      this.http.UpdateCar(cr).subscribe();
      this.http.UpdateGeld(pr).subscribe();
      this.status = 3;
      refresh();
    }
  }
}
function refresh() {
  setTimeout(function () {
    window.location.reload();
  }, 500);
}
