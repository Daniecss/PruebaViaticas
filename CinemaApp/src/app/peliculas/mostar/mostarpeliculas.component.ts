import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { NgxPaginationModule } from 'ngx-pagination';

@Component({
  selector: 'app-mostarpeliculas',
  imports: [ReactiveFormsModule, FormsModule, CommonModule, NgxPaginationModule],
  templateUrl: './mostarpeliculas.component.html',
})
export class PeliculasComponent implements OnInit {
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
          this.peliculas = data;  // Almacena las películas en la variable
          this.totalItems = data.length;  // Actualiza el total de elementos
        },
        error: (err) => {
          this.errorMessage = 'Error al cargar las películas: ' + err.message;  // Muestra error en caso de falla
        }
      });
  }

  agregarPelicula(nuevaPelicula: any): void {
    this.peliculas.push(nuevaPelicula);  
    this.totalItems = this.peliculas.length;  
  }
}
