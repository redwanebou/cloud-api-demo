import { Component, Inject } from '@angular/core';
import { GetCarService } from '../services/getcar/getcar.service';
import { GetCarHttpService } from '../services/getcar/getcar-http.service';
import { OwnAPI } from '../services/ownapi/ownapi.service';
import { AuthService } from '../services/auth/auth.service';
import { Person } from '../services/ownapi/models/person.model';
import { Car } from '../services/ownapi/models/car.model';
import { Model } from '../services/ownapi/models/model.model';
import { Merk } from '../services/ownapi/models/merk.model';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html'
})
export class HomeComponent  {
  status: number;
  year: number = new Date().getFullYear();
  id: number = 0;
  merk: string;
  model: string;

  constructor(public http: OwnAPI, public service: GetCarService, public http1: GetCarHttpService, private http2: AuthService) {
    this.Merk();
  }

  SendPost(prijs:number, bouwjaar: number, brandstof: string) {
    if (!isEmpty(this.merk) && !isEmpty(this.model) && this.model != "Geen Model" && bouwjaar > 1885 && bouwjaar < this.year + 1 && !isEmpty(brandstof) && prijs > 0
      || !isEmpty(this.merk) && this.service.Opslag2 == null && bouwjaar > 1885 && bouwjaar < this.year + 1 && !isEmpty(brandstof) && prijs > 0) {
      // SEND POST REQUEST

      if (this.http2.Information) {
        const mrk: Merk = {
          naam: this.merk
        }

        const mdl: Model = {
          naam: this.model,
          merk: mrk
        }
        const pr: Person = {
          user_id: this.http2.Information.sub,
          email: this.http2.Information.email,
          nickname: this.http2.Information.nickname,
          geld: this.http.persoon.geld
        }

        const car: Car = {
          bouwjaar: bouwjaar,
          brandstof: brandstof,
          prijs: prijs,
          verkocht: false,
          merk: mrk,
          model: mdl,
          person: pr
        };

        this.http.CreateCar(car).subscribe();
        this.status = 2;
      }
      else {
        // gebruiker niet ingelogd
        this.status = 3;
      }
    } else
      this.status = 1;
  }
  Merk() {
    this.http1.GetmyMerk().subscribe(r => {
      this.service.Opslag = r;
    });
  }

  Model() {
    this.http1.GetmyModel(this.id).subscribe(r => {
      this.service.Opslag2 = r;
    });
  }

  CheckMerk(input: string) {
    this.merk = input;
    for (let showme of this.service.Opslag) {
      if (input == showme.marque) {
        this.id = showme.id_marque;
        this.Model()
      }
    }
  }

  existmodel(): boolean {
    if (this.service.Opslag2 == undefined) {
      this.model = "Geen Model";
      return false;
    }
    else
      return true;
  }

  CheckModel(input: string) {
    this.model = input;
  }
}

function isEmpty(str) {
  return (!str || str.length === 0);
}
