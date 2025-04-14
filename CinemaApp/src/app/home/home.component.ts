import { Component } from '@angular/core';
import { Router, RouterModule } from '@angular/router';  // Importa RouterModule
import { CommonModule } from '@angular/common';  // Necesario para usar *ngIf
import { IngresarPeliculaComponent } from '../peliculas/ingresar/ingresar.component';
import { IngresarSalaComponent } from '../sala/ingresarsala/ingresarsala.component';
import { AsignarPeliculasSalasComponent } from '../asignar/asignarpeliculasala/asignarpeliculasala.component';
import { ActualizarPeliculaComponent } from '../peliculas/actualizar/actualizarpelicula.component';
import { BuscarPeliculaComponent } from '../peliculas/buscar/buscarpelicula.component';
import { PeliculasComponent } from '../peliculas/mostar/mostarpeliculas.component';
import { EliminarPeliculaComponent } from '../peliculas/eliminar/eliminarpelicula.component';
import { BuscarSalaComponent } from '../sala/buscar/buscarsalafecha.component';
import { ActualizarSalaComponent } from '../sala/actualizarsala/actualizarsala.component';
import { MostrarSalasComponent } from '../sala/mostrarsalas/mostrarsalas.component';
import { EliminarSalaComponent } from '../sala/eliminarsala/eliminarsala.component';
import { BuscarPeliculaSalaComponent } from '../asignar/buscarfecha/buscarpeliculasala.component';
import { BuscarSalaEstadoComponent } from '../sala/buscarestado/buscarsala.component';
import { DashboardComponent } from '../dashboard/dashboard.componet';
import { AuthService } from '../auth/auth.service';


@Component({
  selector: 'app-home',
  standalone: true,
  imports: [RouterModule, CommonModule, DashboardComponent, IngresarPeliculaComponent, IngresarSalaComponent, 
  AsignarPeliculasSalasComponent, ActualizarPeliculaComponent, BuscarPeliculaComponent, PeliculasComponent,
EliminarPeliculaComponent, BuscarSalaComponent, ActualizarSalaComponent, MostrarSalasComponent, BuscarPeliculaSalaComponent,
EliminarSalaComponent, BuscarSalaEstadoComponent],
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
})
export class HomeComponent {
  selectedOption?: string;

  constructor(private authService: AuthService, private router: Router) {}

  selectOption(option: string) {
    this.selectedOption = option;
  }

  logout() {
    this.authService.logout();  // Llama al servicio de autenticación para hacer logout
  }
}