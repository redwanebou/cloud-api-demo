import { Injectable } from '@angular/core';
import { Inside,Inside2 } from "./getcar-http.service";

@Injectable({
  providedIn: 'root'
})
export class GetCarService {
  Opslag: Inside[];
  Opslag2: Inside2[];
  constructor() { }
}
