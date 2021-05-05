import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class GetCarHttpService
{

  constructor(private http: HttpClient) { }

  GetmyMerk(): Observable<Inside[]> {
    return this.http.get<Inside[]>('https://api.gocar.be/v1/public/beautiful/brands?no_cache=1&per_page=-1');
  }


  public GetmyModel(id_marque: number): Observable<Inside2[]> {
    return this.http.get<Inside2[]>(`https://api.gocar.be/v1/public/beautiful/models?no_cache=1&id_marque=${id_marque}&groups=1&per_page=-1`);
  }

}

export interface Inside {
  id_marque: number;
  marque: string;
}

export interface Inside2 {
  id_marque: number;
  modele: string;
}
