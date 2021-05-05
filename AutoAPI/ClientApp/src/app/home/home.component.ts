import { Component, Inject } from '@angular/core';
import { HttpClient,HttpHeaders } from '@angular/common/http';
import { GetCarService } from '../services/getcar/getcar.service';
import { GetCarHttpService } from '../services/getcar/getcar-http.service';
import { OwnAPI } from '../services/ownapi/ownapi.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html'
})
export class HomeComponent  {
  public car: Auto[];
  status: number;
  year: number = new Date().getFullYear();
  id: number = 0;
  merk: string;
  model: string;

  constructor(private http: HttpClient, private http0: OwnAPI, @Inject('BASE_URL') private baseUrl: string, public service: GetCarService, public http1: GetCarHttpService) {
    this.Merk()
    http.get<Auto[]>(baseUrl + 'api/autodb').subscribe(result => {
      this.car = result;
    }, error => console.error(error));
  }

  SendPost(bouwjaar: number, brandstof: string) {
    if (!isEmpty(this.merk) && !isEmpty(this.model) && this.model != "Geen Model" && bouwjaar > 1885 && bouwjaar < this.year + 1 && !isEmpty(brandstof)
      || !isEmpty(this.merk) && this.service.Opslag2 == null && bouwjaar > 1885 && bouwjaar < this.year + 1 && !isEmpty(brandstof)) {
      const data: Auto = {
        merk: this.merk,
        model: this.model,
        bouwjaar: bouwjaar,
        brandstof: brandstof
      };
     // const httpOptions = {
      //  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
      //}
     // this.http.post(this.baseUrl + 'api/autodb', JSON.stringify(data), httpOptions).subscribe();

      // MAKE AUTH REQUEST WITH JWT TOKEN
      this.http0.PostAUTO(data).subscribe();
      this.status = 2;
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

interface Auto {
  merk: string;
  model: string;
  bouwjaar: number;
  brandstof: string;
}
