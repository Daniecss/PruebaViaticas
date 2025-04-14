import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { PeliculaService } from './pelicula.service';  // Asegúrate de importar el servicio de película

@Injectable({
  providedIn: 'root',
})
export class AsignarPeliculaSalaService {
  private apiUrl = 'http://localhost:5046/api/PeliculaSalaCine'; // Endpoint de asignación

  constructor(
    private http: HttpClient,
    private peliculaService: PeliculaService // Inyecta el servicio de película
  ) {}

  // Método para obtener las películas utilizando PeliculaService
  getPeliculas(): Observable<any[]> {
    return this.peliculaService.obtenerPeliculas(); // Utiliza el método del servicio de película
  }

  // Método para obtener las salas
  getSalas(): Observable<any[]> {
    return this.http.get<any[]>('http://localhost:5046/api/SalaCine');
  }

  // Método para asignar las películas a las salas
  asignarPeliculasASalas(asignaciones: any[]): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/asignar-multiple`, asignaciones);
  }
}

