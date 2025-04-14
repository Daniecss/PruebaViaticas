import { Component, OnInit } from '@angular/core';
import { DashboardService } from '../services/dashboard.service';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { NgxPaginationModule } from 'ngx-pagination';
import { IngresarPeliculaComponent } from '../peliculas/ingresar/ingresar.component';
import { IngresarSalaComponent } from '../sala/ingresarsala/ingresarsala.component';
import { AsignarPeliculasSalasComponent } from '../asignar/asignarpeliculasala/asignarpeliculasala.component';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-dashboard',
  imports: [ReactiveFormsModule, RouterModule, FormsModule, CommonModule, NgxPaginationModule],
  templateUrl: './dashboard.component.html',
})
export class DashboardComponent implements OnInit {
    totalPeliculas: number = 0;
    totalSalas: number = 0;
    totalAsignaciones: number = 0;
    asignaciones: any[] = [];
    loading: boolean = false;
    error: string | null = null;
    currentPage: number = 1;  // Página actual
    itemsPerPage: number = 5;  // Elementos por página
  
    constructor(private dashboardService: DashboardService) {}
    selectedOption?: string; // Almacena la opción seleccionada

    // Método para seleccionar la opción del menú
    selectOption(option: string) {
      this.selectedOption = option;
    }
    ngOnInit(): void {
      this.loadDashboardData();
    }
  
    loadDashboardData(): void {
      this.loading = true;
      this.dashboardService.getTotalPeliculas().subscribe(
        (total) => {
          this.totalPeliculas = total;
        },
        (error) => {
          this.error = 'Error al obtener el total de películas';
        }
      );
  
      this.dashboardService.getTotalSalasCine().subscribe(
        (total) => {
          this.totalSalas = total;
        },
        (error) => {
          this.error = 'Error al obtener el total de salas';
        }
      );
  
      this.dashboardService.getTotalAsignaciones().subscribe(
        (total) => {
          this.totalAsignaciones = total;
        },
        (error) => {
          this.error = 'Error al obtener el total de asignaciones';
        }
      );
  
      this.dashboardService.getAsignaciones().subscribe(
        (response) => {
          console.log('Asignaciones recibidas:', response);  // Agrega este log
          this.asignaciones = response;
          this.loading = false;
        },
        (error) => {
          console.error('Error al obtener asignaciones:', error);  // Agrega este log también
          this.error = 'Error al obtener las asignaciones';
          this.loading = false;
        }
      );
    }
  }
