import { Component, OnInit } from '@angular/core';
import { PeliculaService } from '../../services/pelicula.service';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';

@Component({
  selector: 'app-buscarpelicula',
  imports: [ReactiveFormsModule, FormsModule, CommonModule],
  templateUrl: './buscarpelicula.component.html',
})
export class BuscarPeliculaComponent implements OnInit {
    peliculas: any[] = [];
    idPeliculaSeleccionada: number | undefined;
    peliculaEncontrada: any;
    errorMessage: string | undefined;
  
    constructor(private peliculaService: PeliculaService) {}
  
    ngOnInit() {
      this.obtenerPeliculas();
  
      // Recuperar la última búsqueda desde el servicio
      this.peliculaEncontrada = this.peliculaService.obtenerUltimaBusquedaPelicula();
    }
  
    // Obtener todas las películas para mostrar en el select
    obtenerPeliculas() {
      this.peliculaService.obtenerPeliculas().subscribe({
        next: (data) => {
          this.peliculas = data;
        },
        error: () => {
          this.errorMessage = 'No se pudieron cargar las películas';
        }
      });
    }
  
    // Buscar película por ID seleccionada
    onBuscarPelicula() {
      if (this.idPeliculaSeleccionada) {
        this.peliculaService.buscarPeliculaPorId(this.idPeliculaSeleccionada).subscribe({
          next: (data) => {
            this.peliculaEncontrada = data;
            this.errorMessage = undefined;
  
            // Guardar la última búsqueda en localStorage
            this.peliculaService.guardarUltimaBusquedaPelicula(data);
          },
          error: () => {
            this.errorMessage = 'Pelicula no encontrada';
            this.peliculaEncontrada = undefined;
          }
        });
      }
    }
  }