import { Injectable } from '@angular/core';
import createAuth0Client, { GetUserOptions } from '@auth0/auth0-spa-js';
import Auth0Client from '@auth0/auth0-spa-js/dist/typings/Auth0Client';
import {from, of, Observable, BehaviorSubject, combineLatest, throwError} from 'rxjs';
import {tap, catchError, concatMap, shareReplay} from 'rxjs/operators';
import {Router} from '@angular/router';
import { OwnAPI } from '../ownapi/ownapi.service';
import { Person } from '../ownapi/models/person.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  CLIENTID: string = 'haF4FN45jjIZCEHSC1yj3Dt6Tk4uPBDv'
  public client: Auth0Client;
  Information: any;
  pr: Person;
  
  auth0Client$ = (from(
    createAuth0Client({
      domain: 'rcloud.eu.auth0.com',
      client_id: this.CLIENTID,
      redirect_uri: `${window.location.origin}`
    })
  ) as Observable<Auth0Client>).pipe(
    shareReplay(1),
    catchError(err => throwError(err))
  );

  isAuthenticated$ = this.auth0Client$.pipe(
    concatMap((client: Auth0Client) => from(client.isAuthenticated())),
    tap(res => {
      this.loggedIn = res;
      if (this.loggedIn == true) {
        this.userProfile$.subscribe(r => { this.Information = r; this.CreateUserinDB() });
      }
    })
  );
  handleRedirectCallback$ = this.auth0Client$.pipe(
    concatMap((client: Auth0Client) => from(client.handleRedirectCallback()))
  );

  private userProfileSubject$ = new BehaviorSubject<any>(null);
  userProfile$ = this.userProfileSubject$.asObservable();

  loggedIn: boolean = null;

  constructor(private router: Router, private ApiService: OwnAPI) {
    this.localAuthSetup();
    this.handleAuthCallback();
  }

  CreateUserinDB() {
    if (this.Information != null) {
     this.pr = {
      user_id: this.Information.sub,
      email: this.Information.email,
      nickname: this.Information.nickname,
      geld: 0 // default waarde
      }
      this.ApiService.GetToken(this.pr); // GET TOKEN
      this.ApiService.GetPerson(this.Information.sub, this.pr);
    }
  }

  getUser$(options?): Observable<any> {
    return this.auth0Client$.pipe(
      concatMap((client: Auth0Client) => from(client.getUser(options))),
      tap(user => this.userProfileSubject$.next(user))
    );
  }

  private localAuthSetup() {

    const checkAuth$ = this.isAuthenticated$.pipe(
      concatMap((loggedIn: boolean) => {
        if (loggedIn) {
          return this.getUser$();
        }
        return of(loggedIn);
      })
    );
    checkAuth$.subscribe();
  }

  login(redirectPath: string = '/') {
    this.auth0Client$.subscribe((client: Auth0Client) => {
      client.loginWithRedirect({
        redirect_uri: `${window.location.origin}`,
        appState: { target: redirectPath }
      });
    });
  }

  private handleAuthCallback() {
    const params = window.location.search;
    if (params.includes('code=') && params.includes('state=')) {
      let targetRoute: string;
      // LETS FIND THE AUTH CODE
     // let CUTCODE = params.match('code').input.substring(6)
      //let CLIENTCODE = CUTCODE.substring(0, 16)
      // WE NEED THIS CODE FOR /USERINFO
      //this.ApiService.USERTOKEN = CLIENTCODE;

      const authComplete$ = this.handleRedirectCallback$.pipe(
        tap(cbRes => {
          targetRoute = cbRes.appState && cbRes.appState.target ? cbRes.appState.target : '/';
        }),
        concatMap(() => {
          return combineLatest([
            this.getUser$(),
            this.isAuthenticated$,
          ]);
        })
      );

      authComplete$.subscribe(([user, loggedIn]) => {
        this.router.navigate([targetRoute]);
      });
    }
  }

  logout() {
    this.ApiService.DeleteToken(); // Delete Token
    this.auth0Client$.subscribe((client: Auth0Client) => {
      client.logout({
        client_id: this.CLIENTID,
        returnTo: `${window.location.origin}`
      });
    });

  }
}
