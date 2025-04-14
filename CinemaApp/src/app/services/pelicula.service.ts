import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class PeliculaService {
  private apiUrl = 'http://localhost:5046/api/pelicula'; 

  constructor(private http: HttpClient) {}

  crearPelicula(pelicula: any): Observable<any> {
    console.log('Datos enviados:', pelicula);  
    return this.http.post(this.apiUrl, pelicula);  
  }

  obtenerPeliculas(): Observable<any[]> {
    return this.http.get<any[]>(this.apiUrl);  
  }

  getPeliculaById(id: number): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/${id}`);  
  }

  updatePelicula(id: number, pelicula: any): Observable<any> {
    return this.http.put(`${this.apiUrl}/${id}`, pelicula);
  }

  buscarPeliculaPorId(id: number): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/${id}`);
  }

  guardarUltimaPeliculaActualizada(pelicula: any): void {
    localStorage.setItem('ultimaPeliculaActualizada', JSON.stringify(pelicula));
  }
  
  obtenerUltimaPeliculaActualizada(): any | null {
    const data = localStorage.getItem('ultimaPeliculaActualizada');
    return data ? JSON.parse(data) : null;
  }

  guardarUltimaBusquedaPelicula(pelicula: any): void {
    localStorage.setItem('ultimaBusquedaPelicula', JSON.stringify(pelicula));
  }
  
  obtenerUltimaBusquedaPelicula(): any {
    const data = localStorage.getItem('ultimaBusquedaPelicula');
    return data ? JSON.parse(data) : null;
  }

  
}

