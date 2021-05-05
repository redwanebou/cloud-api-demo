import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { AuthService } from '../auth/auth.service';

@Injectable({
  providedIn: 'root'
})

export class authtokenS
{
  public CLIENTCODE: string;
  private body: any = {
    client_id: environment.auth0ApiConfig.client_id,
    client_secret: environment.auth0ApiConfig.client_secret,
    grant_type: environment.auth0ApiConfig.grant_type,
    redirect_uri: environment.auth0ApiConfig.redirect_uri,
    code: "" // get code
  };
  
  constructor(private http: HttpClient) {}

  private URL = 'https://rcloud.eu.auth0.com/oauth/token';

  public RequestNewToken()
  {
    this.body.code = this.CLIENTCODE
    return this.http.post(this.URL, this.body);

  }

}


