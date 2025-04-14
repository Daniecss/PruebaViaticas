import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DashboardService {
  private apiUrl = 'http://localhost:5046/api'; // Cambiar según la URL de tu backend

  constructor(private http: HttpClient) {}

  getTotalPeliculas(): Observable<number> {
    return this.http.get<number>(`${this.apiUrl}/Pelicula/total`);
  }

  getTotalSalasCine(): Observable<number> {
    return this.http.get<number>(`${this.apiUrl}/SalaCine/total`);
  }

  getTotalAsignaciones(): Observable<number> {
    return this.http.get<number>(`${this.apiUrl}/PeliculaSalaCine/total`);
  }

  getAsignaciones(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/PeliculaSalaCine/dashboard`);
  }
}
