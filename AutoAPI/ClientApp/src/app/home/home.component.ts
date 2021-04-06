import { Component, Inject } from '@angular/core';
import { HttpClient,HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html'
})
export class HomeComponent  {
  public car: Auto[];
  status: number;
  year: number = new Date().getFullYear();

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    http.get<Auto[]>(baseUrl + 'api/autodb').subscribe(result => {
      this.car = result;
    }, error => console.error(error));
  }

  SendPost(merk: string, model: string, bouwjaar: number, brandstof: string) {
    if (!isEmpty(merk) && !isEmpty(model) && bouwjaar > 1885 && bouwjaar < this.year + 1 && !isEmpty(brandstof)) {
      const data: Auto = {
        merk: merk,
        model: model,
        bouwjaar: bouwjaar,
        brandstof: brandstof
      };
      const httpOptions = {
        headers: new HttpHeaders({ 'Content-Type': 'application/json' })
      }
      this.http.post(this.baseUrl + 'api/autodb', JSON.stringify(data), httpOptions).subscribe();
      this.status = 2;
    } else
      this.status = 1;
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
