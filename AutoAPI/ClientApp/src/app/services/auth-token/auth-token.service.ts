import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})

export class authtokenS
{

  private body: any = {
    client_id: environment.auth0ApiConfig.client_id,
    client_secret: environment.auth0ApiConfig.client_secret,
    audience: environment.auth0ApiConfig.audience,
    grant_type: environment.auth0ApiConfig.grant_type
  };
  
  constructor(private http: HttpClient) {}

  private URL = 'https://rcloud.eu.auth0.com/oauth/token';

  public RequestNewToken()
  {
    return this.http.post(this.URL, this.body);

  }

}


