import { Component } from '@angular/core';
import { PeliculaSalaService } from '../../services/pelicula-sala-cine.service';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../../environments/environment';

@Component({
  selector: 'app-buscarpeliculasala',
  imports: [CommonModule, FormsModule, ReactiveFormsModule],
  templateUrl: './buscarpeliculasala.component.html',
})
export class BuscarPeliculaSalaComponent {
    fechaInicio: string = '';
    resultados: any[] = [];
  
    constructor(private http: HttpClient) {}
  
    buscarPorFecha(): void {
      if (!this.fechaInicio) {
        alert('Por favor selecciona una fecha.');
        return;
      }
  
      const fechaFormateada = this.fechaInicio;
      const url = `${environment.apiUrl}/PeliculaSalaCine/buscar-por-fecha?fechaInicio=${fechaFormateada}`;
  
      this.http.get<any[]>(url).subscribe({
        next: (data) => {
          this.resultados = data;
        },
        error: (err) => {
          console.error(err);
          alert('No se encontraron asignaciones para esa fecha o hubo un error.');
        }
      });
    }
  }