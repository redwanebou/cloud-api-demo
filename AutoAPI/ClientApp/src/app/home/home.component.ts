import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html'
})
export class HomeComponent {
  public car: Auto[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<Auto[]>(baseUrl + 'api/autodb').subscribe(result => {
      this.car = result;
    }, error => console.error(error));
  }

  SendPost(merk: string, model: string, bouwjaar: number, brandstof: string): boolean {
    /* if (merk != "" && model != "" && bouwjaar > 1885 && brandstof != "") */
       /* SEND POST REQUEST */
    return false;
  }
}


interface Auto {
  merk: string;
  model: string;
  bouwjaar: number;
  brandstof: string;
}
