import { Component, OnInit } from '@angular/core';
import { SalaCineService } from '../../services/sala.service';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';

@Component({
  selector: 'app-buscarsalafecha',
  imports: [ReactiveFormsModule, FormsModule, CommonModule],
  templateUrl: './buscarsalafecha.component.html',
  standalone: true,
})
export class BuscarSalaComponent implements OnInit {
  salas: any[] = [];
  idSeleccionado: number | null = null;
  resultado: any = null;
  error: string | null = null;

  constructor(private salaService: SalaCineService) {}

  ngOnInit(): void {
    this.salaService.obtenerSalas().subscribe({
      next: (salas) => this.salas = salas,
      error: () => this.error = 'Error al cargar las salas.'
    });

    const ultimaBusqueda = this.salaService.obtenerUltimaBusqueda();
    if (ultimaBusqueda) {
      this.resultado = ultimaBusqueda;
    }
  }

  buscarSala() {
    this.resultado = null;
    this.error = null;

    if (this.idSeleccionado === null) {
      this.error = 'Seleccione una sala.';
      return;
    }

    this.salaService.buscarSalaPorId(this.idSeleccionado).subscribe({
      next: (res) => {
        this.resultado = res;

        this.salaService.guardarUltimaBusqueda(res);
      },
      error: () => this.error = 'No se encontró la sala.'
    });
  }
}
