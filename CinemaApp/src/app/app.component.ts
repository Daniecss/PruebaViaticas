import { Component } from '@angular/core';
import { Router, RouterOutlet } from '@angular/router';  // Importar Router para la navegación

@Component({
  selector: 'app-root',
  imports: [RouterOutlet],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent {
  title(title: any) {
    throw new Error('Method not implemented.');
  }
  constructor(private router: Router) {}

  navigateToIngresarPelicula() {
    this.router.navigate(['/ingresar-pelicula']);  // Navega a la ruta para ingresar película
  }

  navigateToSala() {
    this.router.navigate(['/ingresar-sala']);
  }
}