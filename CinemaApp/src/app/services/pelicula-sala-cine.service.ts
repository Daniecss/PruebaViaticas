import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class PeliculaSalaService {
  private apiUrl = `${environment.apiUrl}/PeliculaSalaCine`;

  constructor(private http: HttpClient) {}

  // Obtener todas las asignaciones de películas a salas
  getAllAsignaciones(): Observable<any> {
    return this.http.get(`${this.apiUrl}/lista-simple`);
  }

  // Asignar múltiples películas a salas
  asignarPeliculasASalas(asignaciones: any[]): Observable<any> {
    return this.http.post(`${this.apiUrl}/asignar-multiple`, asignaciones);
  }

}
