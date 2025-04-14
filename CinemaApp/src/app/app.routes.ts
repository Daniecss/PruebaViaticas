import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { IngresarPeliculaComponent } from './peliculas/ingresar/ingresar.component';
import { IngresarSalaComponent } from './sala/ingresarsala/ingresarsala.component';
import { AsignarPeliculasSalasComponent } from './asignar/asignarpeliculasala/asignarpeliculasala.component';
import { ActualizarPeliculaComponent } from './peliculas/actualizar/actualizarpelicula.component';
import { BuscarPeliculaComponent } from './peliculas/buscar/buscarpelicula.component';
import { EliminarPeliculaComponent } from './peliculas/eliminar/eliminarpelicula.component';
import { BuscarSalaComponent } from './sala/buscar/buscarsalafecha.component';
import { ActualizarSalaComponent } from './sala/actualizarsala/actualizarsala.component';
import { MostrarSalasComponent } from './sala/mostrarsalas/mostrarsalas.component';
import { EliminarSalaComponent } from './sala/eliminarsala/eliminarsala.component';
import { BuscarPeliculaSalaComponent } from './asignar/buscarfecha/buscarpeliculasala.component';
import { BuscarSalaEstadoComponent } from './sala/buscarestado/buscarsala.component';
import { DashboardComponent } from './dashboard/dashboard.componet';
import { LoginComponent } from './auth/login/login.component';
import { AuthGuard } from './auth/auth.module';

export const routes: Routes = [
  { path: 'home', component: HomeComponent , canActivate: [AuthGuard] },
  { path: 'login', component: LoginComponent },
  { path: 'dashborad', component: DashboardComponent, canActivate: [AuthGuard]},
  { path: 'ingresar/pelicula', component: IngresarPeliculaComponent, canActivate: [AuthGuard] },
  { path: 'actualizar/pelicula', component: ActualizarPeliculaComponent, canActivate: [AuthGuard] },
  { path: 'buscar/pelicula', component: BuscarPeliculaComponent, canActivate: [AuthGuard] },
  { path: 'todas/pelicula', component: BuscarPeliculaComponent, canActivate: [AuthGuard] },
  { path: 'eliminar/pelicula', component: EliminarPeliculaComponent, canActivate: [AuthGuard] },
  { path: 'ingresar/sala', component: IngresarSalaComponent, canActivate: [AuthGuard] },
  { path: 'buscar/sala', component: BuscarSalaComponent, canActivate: [AuthGuard] },
  { path: 'actualizar/sala', component: ActualizarSalaComponent, canActivate: [AuthGuard] },
  { path: 'funciones/asignar', component: AsignarPeliculasSalasComponent, canActivate: [AuthGuard]  },
  { path: 'mostrarsalas/sala', component: MostrarSalasComponent, canActivate: [AuthGuard] },
  { path: 'eliminarsala/sala', component: EliminarSalaComponent , canActivate: [AuthGuard]},
  { path: 'buscarpeliculasala/asignar', component: BuscarPeliculaSalaComponent , canActivate: [AuthGuard]},
  { path: 'buscarestado/sala', component: BuscarSalaEstadoComponent, canActivate: [AuthGuard] },
  { path: '', redirectTo: '/login', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
