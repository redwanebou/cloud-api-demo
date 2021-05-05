// This file can be replaced during build by using the `fileReplacements` array.
// `ng build ---prod` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

export const environment = {
  production: false,
  auth0ApiConfig: {
    client_id: 'haF4FN45jjIZCEHSC1yj3Dt6Tk4uPBDv',
    client_secret: 'ZvG9-GvFafB2XnP-ypj-Sbnxx9HzvmTWgGn4JSaFcZI2Dn9UdiugC3P32EbyLyZ9',
    grant_type: 'authorization_code',
    redirect_uri: 'http://localhost:17304/'
  }
};
/*
 * In development mode, to ignore zone related error stack frames such as
 * `zone.run`, `zoneDelegate.invokeTask` for easier debugging, you can
 * import the following file, but please comment it out in production mode
 * because it will have performance impact when throw error
 */
// import 'zone.js/dist/zone-error';  // Included with Angular CLI.
