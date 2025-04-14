import { Component, OnInit } from '@angular/core';
import { SalaCineService, SalaCineDto } from '../../services/sala.service';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { NgxPaginationModule } from 'ngx-pagination';

@Component({
  selector: 'app-eliminarsala',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, FormsModule, NgxPaginationModule],
  templateUrl: './eliminarsala.component.html',
})
export class EliminarSalaComponent implements OnInit {
  salas: SalaCineDto[] = [];
  errorMessage: string = '';
  currentPage: number = 1;
  itemsPerPage: number = 5;
  totalItems: number = 0;

  constructor(private salaService: SalaCineService) {}

  ngOnInit(): void {
    this.obtenerSalas();
  }

  obtenerSalas(): void {
    this.salaService.obtenerSalas().subscribe({
      next: (data) => {
        this.salas = data;
        this.totalItems = data.length;
      },
      error: (error) => {
        this.errorMessage = 'Error al cargar las salas: ' + error.message;
      }
    });
  }

  eliminarSala(id: number | undefined): void {
    if (id === undefined) {
      this.errorMessage = 'ID de sala no válido.';
      return;
    }
  
    if (confirm('¿Estás seguro de que deseas eliminar esta sala?')) {
      this.salaService.eliminarSala(id).subscribe({
        next: () => {
          this.salas = this.salas.filter(sala => sala.idSala !== id);
          this.totalItems = this.salas.length;
        },
        error: (err) => {
          this.errorMessage = 'Error al eliminar la sala: ' + err.message;
        }
      });
    }
  }
}
