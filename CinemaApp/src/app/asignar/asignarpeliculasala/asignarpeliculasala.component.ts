import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms'; 
import { PeliculaSalaService } from '../../services/pelicula-sala-cine.service';
import { PeliculaService } from '../../services/pelicula.service';
import { SalaCineService } from '../../services/sala.service';
import { HttpErrorResponse } from '@angular/common/http';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-asignarpeliculasala',
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule],
  templateUrl: './asignarpeliculasala.component.html',
})
export class AsignarPeliculasSalasComponent implements OnInit {
  peliculas: any[] = [];
  salas: any[] = [];
  asignaciones: any[] = [];
  lastAsignacion: any;
  mensaje: string = '';
  errorMessage: string = '';
  asignacionForm!: FormGroup;

  constructor(
    private peliculaSalaService: PeliculaSalaService,
    private peliculaService: PeliculaService,
    private salaCineService: SalaCineService,
    private fb: FormBuilder
  ) {}

  ngOnInit(): void {
    this.asignacionForm = this.fb.group({
      idPelicula: ['', Validators.required],
      idSalaCine: ['', Validators.required],
      fechaPublicacion: ['', Validators.required],
      fechaFin: ['', Validators.required],
    });

    this.loadLastAsignacion();
    this.loadPeliculas();
    this.loadSalas();
  }

  loadLastAsignacion(): void {
    this.peliculaSalaService.getAllAsignaciones().subscribe(data => {
      this.asignaciones = data;
      if (this.asignaciones.length > 0) {
        this.lastAsignacion = this.asignaciones[this.asignaciones.length - 1];
      }
    });
  }

  loadPeliculas(): void {
    this.peliculaService.obtenerPeliculas().subscribe(
      response => this.peliculas = response,
      error => this.errorMessage = 'Error al cargar las películas'
    );
  }

  loadSalas(): void {
    this.salaCineService.obtenerSalas().subscribe(
      response => this.salas = response,
      error => this.errorMessage = 'Error al cargar las salas'
    );
  }

  onSubmit(): void {
    if (this.asignacionForm.invalid) return;

    const asignacion = {
      IdPelicula: this.asignacionForm.value.idPelicula,
      IdSalaCine: this.asignacionForm.value.idSalaCine,
      FechaPublicacion: this.asignacionForm.value.fechaPublicacion,
      FechaFin: this.asignacionForm.value.fechaFin,
    };

    this.peliculaSalaService.asignarPeliculasASalas([asignacion]).subscribe(
      (response) => {
        this.mensaje = response.mensaje;
        this.errorMessage = '';
        this.loadLastAsignacion();
      },
      (error) => {
        this.errorMessage = 'Error al asignar película: ' + error.message;
        this.mensaje = '';
      }
    );
  }
}
