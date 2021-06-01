import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { Auth0Token } from '../auth-token/auth-token.model';
import { authtokenS } from '../auth-token/auth-token.service';
import { Car } from './models/car.model';
import { Person } from './models/person.model';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class OwnAPI {
  private Auth0Token: Auth0Token;
  private httpOptions;
  public car: Car[];
  public persoon: Person;
  private create: boolean;

  constructor(private http: HttpClient, private apiAuth: authtokenS) { }

  // Auth For API

  public GetToken(pr: Person) {
   this.apiAuth.RequestNewToken().subscribe(
     (data: Auth0Token) =>
     {
       this.Auth0Token = data;
       this.httpOptions = {
         headers: new HttpHeaders({
           'Authorization': 'Bearer ' + this.Auth0Token.access_token,
           'Content-Type': 'application/json'
         })
       };
       this.CreatePerson(pr);
      },
     (error: HttpErrorResponse) => console.log(error)
    );
 }

  public DeleteToken() {
    this.Auth0Token = null;
  }

  // CRUD

  // PERSON

  public GetPerson(id, pr:Person) {
    this.http.get<Person>(environment.URL.person + '/' + id).subscribe(
      result => { this.persoon = result; console.log(result); this.create = false },
      error => {
        console.log("Person NOT FOUND so we create one...")
        this.create = true;
      }
    ); 
  }
  public CreatePerson(data: Person) {
    if (this.create) {
      return this.http.post(environment.URL.person, data, this.httpOptions).subscribe(
        result => { this.persoon = data; },
        error => console.log(error));
    }
  }

  public UpdateGeld(pr:Person) {
    return this.http.put<Person>(environment.URL.person, pr, this.httpOptions);
  }

  // CAR

  public GetCar() {
    this.http.get<Car[]>(environment.URL.auto).subscribe(result => {
      this.car = result;
    }, error => console.error(error));
  }

  public UpdateCar(cr:Car) {
    return this.http.put<Car>(environment.URL.auto, cr, this.httpOptions);
  }


  public DeleteCar(id) {
    return this.http.delete(environment.URL.auto + '/' + id, this.httpOptions);
  }

  public CreateCar(data: Car) {
    return this.http.post(environment.URL.auto, JSON.stringify(data), this.httpOptions);
  }
}
