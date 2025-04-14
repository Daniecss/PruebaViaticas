import { Component, OnInit } from '@angular/core';
import { SalaCineService, SalaCineDto } from '../../services/sala.service';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { NgxPaginationModule } from 'ngx-pagination';

@Component({
  selector: 'app-mostrarsalas',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, FormsModule, NgxPaginationModule],
  templateUrl: './mostrarsalas.component.html',
})
export class MostrarSalasComponent implements OnInit {
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

  agregarSala(nuevaSala: SalaCineDto): void {
    this.salas.push(nuevaSala);
    this.totalItems = this.salas.length;
  }
}
