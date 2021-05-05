import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { Auth0Token } from '../auth-token/auth-token.model';
import { authtokenS } from '../auth-token/auth-token.service';

@Injectable({
  providedIn: 'root'
})
export class OwnAPI {
  private Auth0Token: Auth0Token;
  private httpOptions;
  private URL = 'http://localhost:17304/api/autodb';

  constructor(private http: HttpClient, private apiAuth: authtokenS) { }

  // Auth For API

  public GetToken() {
   this.apiAuth.RequestNewToken().subscribe(
      (data: Auth0Token) => {
       this.Auth0Token = data;
       this.SetHttpHeaders();
      },
     (error: HttpErrorResponse) => console.log(error)
    );
 }

  public DeleteToken() {
    this.Auth0Token = null;
  }

  private SetHttpHeaders() {
    this.httpOptions = {
      headers: new HttpHeaders({
        'Authorization': 'Bearer ' + this.Auth0Token.access_token,
        'Content-Type': 'application/json'
      })
    };
  }

  public PostAUTO(auto: any) {
    return this.http.post<any>(this.URL, auto, this.httpOptions);
  }
}
