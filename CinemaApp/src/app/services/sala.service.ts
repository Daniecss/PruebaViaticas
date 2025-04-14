import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface SalaCineDto {
  idSala?: number;
  nombre: string;
  estado: string;
}

@Injectable({
  providedIn: 'root'
})
export class SalaCineService {
  private apiUrl = 'http://localhost:5046/api/SalaCine';

  constructor(private http: HttpClient) {}

  crearSala(sala: SalaCineDto): Observable<SalaCineDto> {
    return this.http.post<SalaCineDto>(this.apiUrl, sala);
  }

  obtenerSalas(): Observable<SalaCineDto[]> {
    return this.http.get<SalaCineDto[]>(this.apiUrl);
  }

  buscarSalaPorId(id: number) {
    return this.http.get<any>(`${this.apiUrl}/${id}`);
  }

  guardarUltimaBusqueda(resultado: any): void {
    localStorage.setItem('ultimaBusqueda', JSON.stringify(resultado));
  }

  updateSala(sala: SalaCineDto): Observable<SalaCineDto> {
    return this.http.put<SalaCineDto>(`${this.apiUrl}/${sala.idSala}`, sala);
  }

  getEstadoSalaPorNombre(nombre: string): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/buscar/${nombre}`);
  }

  obtenerUltimaBusqueda(): any {
    const ultimaBusqueda = localStorage.getItem('ultimaBusqueda');
    return ultimaBusqueda ? JSON.parse(ultimaBusqueda) : null;
  }

  eliminarSala(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }

  guardarUltimaSala(sala: SalaCineDto): void {
    localStorage.setItem('ultimaSala', JSON.stringify(sala));
  }
  
  obtenerUltimaSala(): SalaCineDto | null {
    const data = localStorage.getItem('ultimaSala');
    return data ? JSON.parse(data) : null;
  }
}


