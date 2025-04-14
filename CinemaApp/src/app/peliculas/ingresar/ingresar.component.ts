import { Component } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { PeliculaService } from '../../services/pelicula.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-ingresar-pelicula',  
  imports: [ReactiveFormsModule, FormsModule, CommonModule],
  templateUrl: './ingresar.component.html',
  styleUrls: ['./ingresar.component.scss']
})
export class IngresarPeliculaComponent {
  pelicula = { Nombre: '', Duracion: 0 };  // Estructura inicial del objeto pelicula
  ultimaPelicula: any = null;  // Para almacenar la última película agregada

  constructor(private peliculaService: PeliculaService) {
    // Cargar los datos si existe alguna película guardada previamente
    const storedPelicula = localStorage.getItem('ultimaPelicula');
    if (storedPelicula) {
      this.ultimaPelicula = JSON.parse(storedPelicula);
    }
  }

  // Método para agregar la película
  agregarPelicula() {
    console.log('Datos enviados:', this.pelicula);
    this.peliculaService.crearPelicula(this.pelicula).subscribe(
      response => {
        console.log('Película agregada:', response);
        this.ultimaPelicula = this.pelicula;  // Guardamos la última película agregada
        this.saveStorage();  // Llamar a la función para guardar la película en localStorage
        this.pelicula = { Nombre: '', Duracion: 0 };  // Limpiar el formulario después de agregar
      },
      error => {
        console.error('Error al agregar la película:', error);
      }
    );
  }

  // Función para guardar la película en localStorage
  saveStorage() {
    localStorage.setItem('ultimaPelicula', JSON.stringify(this.pelicula));  // Guardamos en localStorage
  }
}
