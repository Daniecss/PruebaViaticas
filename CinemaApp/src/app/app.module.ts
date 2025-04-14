import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app.routes'; 
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgxPaginationModule } from 'ngx-pagination';

import { IngresarPeliculaComponent } from './peliculas/ingresar/ingresar.component';  
import { IngresarSalaComponent } from './sala/ingresarsala/ingresarsala.component';  
import { AsignarPeliculasSalasComponent } from './asignar/asignarpeliculasala/asignarpeliculasala.component';
import { ActualizarPeliculaComponent } from './peliculas/actualizar/actualizarpelicula.component';
import { PeliculasComponent } from './peliculas/mostar/mostarpeliculas.component';
import { EliminarPeliculaComponent } from './peliculas/eliminar/eliminarpelicula.component';
import { BuscarSalaComponent } from './sala/buscar/buscarsalafecha.component';
import { MostrarSalasComponent } from './sala/mostrarsalas/mostrarsalas.component';
import { EliminarSalaComponent } from './sala/eliminarsala/eliminarsala.component';
import { BuscarPeliculaSalaComponent } from './asignar/buscarfecha/buscarpeliculasala.component';
import { BuscarSalaEstadoComponent } from './sala/buscarestado/buscarsala.component';
import { DashboardComponent } from './dashboard/dashboard.componet';
import { DashboardService } from './services/dashboard.service';

@NgModule({
  declarations: [
  
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    FormsModule,
    NgxPaginationModule
  ],
  providers: [],
})
export class AppModule {}



