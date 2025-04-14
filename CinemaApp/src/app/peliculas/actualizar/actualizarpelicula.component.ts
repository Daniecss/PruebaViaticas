import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { PeliculaService } from '../../services/pelicula.service';

@Component({
  selector: 'app-actualizarpelicula',
  templateUrl: './actualizarpelicula.component.html',
  imports: [ReactiveFormsModule, FormsModule, CommonModule],
})
export class ActualizarPeliculaComponent implements OnInit {
  buscarForm: FormGroup;
  peliculaForm: FormGroup;
  peliculas: any[] = [];
  error: string | null = null;
  mostrarFormulario: boolean = false;
  mensaje: string = '';
  peliculaSeleccionada: any = null;

  constructor(
    private fb: FormBuilder,
    private peliculaService: PeliculaService
  ) {
    this.buscarForm = this.fb.group({
      id: [null, Validators.required],
    });

    this.peliculaForm = this.fb.group({
      nombre: ['', Validators.required],
      duracion: [null, [Validators.required, Validators.min(1)]],
    });
  }

  ngOnInit(): void {
    // Obtener todas las películas disponibles
    this.peliculaService.obtenerPeliculas().subscribe({
      next: (data) => {
        this.peliculas = data;
      },
      error: () => {
        this.error = 'No se pudo obtener las películas.';
      }
    });

    // Cargar la última película actualizada desde el localStorage
    const ultima = this.peliculaService.obtenerUltimaBusquedaPelicula();
    if (ultima) {
      this.peliculaSeleccionada = ultima;
      this.mostrarFormulario = true;
      this.buscarForm.setValue({ id: ultima.idPelicula });
      this.peliculaForm.setValue({
        nombre: ultima.nombre,
        duracion: ultima.duracion
      });
    }
  }

  buscarPelicula() {
    const id = this.buscarForm.value.id;
    this.peliculaService.getPeliculaById(id).subscribe({
      next: (data) => {
        this.peliculaSeleccionada = data;
        this.peliculaForm.setValue({
          nombre: data.nombre,
          duracion: data.duracion,
        });
        this.mostrarFormulario = true;
      },
      error: () => {
        this.error = 'No se encontró la película con ese ID.';
      }
    });
  }

  actualizarPelicula() {
    const id = this.buscarForm.value.id;
    const peliculaActualizada = this.peliculaForm.value;

    this.peliculaService.updatePelicula(id, peliculaActualizada).subscribe({
      next: () => {
        this.mensaje = 'Película actualizada con éxito.';
        this.peliculaSeleccionada.nombre = peliculaActualizada.nombre;
        this.peliculaSeleccionada.duracion = peliculaActualizada.duracion;

        // Guardar en localStorage usando el servicio
        this.peliculaService.guardarUltimaBusquedaPelicula({
          idPelicula: id,
          ...peliculaActualizada
        });

        this.buscarForm.reset();
        this.peliculaForm.reset();
        this.mostrarFormulario = false;
      },
      error: () => {
        this.error = 'Ocurrió un error al actualizar la película.';
      }
    });
  }
}