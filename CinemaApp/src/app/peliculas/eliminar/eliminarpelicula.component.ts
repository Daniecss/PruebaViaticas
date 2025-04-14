import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { NgxPaginationModule } from 'ngx-pagination';

@Component({
  selector: 'app-eliminarpelicula',
  imports: [ReactiveFormsModule, FormsModule, CommonModule, NgxPaginationModule],
  templateUrl: './eliminarpelicula.component.html',
})
export class EliminarPeliculaComponent implements OnInit {
  peliculas: any[] = [];
  errorMessage: string = '';
  currentPage: number = 1;  
  itemsPerPage: number = 5; 
  totalItems: number = 0;   

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.obtenerPeliculas();  
  }

  obtenerPeliculas(): void {
    this.http.get<any[]>('http://localhost:5046/api/Pelicula')
      .subscribe({
        next: (data) => {
          this.peliculas = data;
          this.totalItems = data.length;  
        },
        error: (err) => {
          this.errorMessage = 'Error al cargar las películas: ' + err.message;
        }
      });
  }

  eliminarPelicula(id: number): void {
    if (confirm('¿Estás seguro de que deseas eliminar esta película?')) {
      this.http.delete(`http://localhost:5046/api/Pelicula/${id}`)
        .subscribe({
          next: () => {
            this.peliculas = this.peliculas.filter(pelicula => pelicula.idPelicula !== id);  
            this.totalItems = this.peliculas.length;  
          },
          error: (err) => {
            this.errorMessage = 'Error al eliminar la película: ' + err.message;
          }
        });
    }
  }
}
