import { Component } from '@angular/core';
import { SalaCineDto, SalaCineService } from '../../services/sala.service';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';

@Component({
  selector: 'app-buscarestadosala',
  imports: [ReactiveFormsModule, FormsModule, CommonModule],
  templateUrl: './buscarsala.component.html',
})
export class BuscarSalaEstadoComponent {
  salas: SalaCineDto[] = []; // Lista de salas para el select
  salaSeleccionadaId: number | null = null; // ID de la sala seleccionada
  sala: any = null; // Sala con los datos detallados
  estado: string = '';
  error: string = '';

  constructor(private salaCineService: SalaCineService) {}

  ngOnInit(): void {
    this.cargarSalas();
  
    const ultima = this.salaCineService.obtenerUltimaBusqueda();
    if (ultima) {
      this.sala = ultima;
      this.estado = ultima.estado;
    }
  }
  cargarSalas() {
    this.salaCineService.obtenerSalas().subscribe({
      next: (data) => {
        this.salas = data;
      },
      error: () => {
        this.error = 'Error al cargar las salas';
      }
    });
  }

  buscarSala() {
    if (this.salaSeleccionadaId !== null) {
      const sala = this.salas.find(s => s.idSala === this.salaSeleccionadaId);
      const nombreSala = sala?.nombre ?? '';
  
      this.salaCineService.buscarSalaPorId(this.salaSeleccionadaId).subscribe({
        next: (res) => {
          this.sala = res;
          this.error = '';
  
          // Guardar en localStorage al obtener la sala
          this.salaCineService.guardarUltimaBusqueda({
            idSala: this.sala.idSala,
            nombre: this.sala.nombre,
            estado: this.estado // Se actualizará cuando llegue el estado
          });
        },
        error: () => {
          this.error = 'Sala no encontrada';
          this.sala = null;
          this.estado = '';
        }
      });
  
      this.salaCineService.getEstadoSalaPorNombre(nombreSala).subscribe({
        next: (res) => {
          this.estado = res.estado;
  
          // Actualizar el estado en localStorage (incluyendo estado más reciente)
          this.salaCineService.guardarUltimaBusqueda({
            idSala: this.salaSeleccionadaId,
            nombre: nombreSala,
            estado: this.estado
          });
        },
        error: () => {
          this.estado = 'No se pudo obtener el estado de la sala';
        }
      });
    }
  }
}