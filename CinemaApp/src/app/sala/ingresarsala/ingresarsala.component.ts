import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { SalaCineService } from '../../services/sala.service';

@Component({
  selector: 'app-ingresar-sala',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './ingresarsala.component.html',
  styleUrls: ['./ingresarsala.component.scss'],
  providers: [SalaCineService]
})
export class IngresarSalaComponent {
  nombre: string = '';
  estado: string = 'Disponible';
  mensaje: string = '';
  ultimaSala: any = null;

  constructor(private salaCineService: SalaCineService) {
    this.ultimaSala = this.salaCineService.obtenerUltimaSala();
  }

  onSubmit() {
    const nuevaSala = {
      nombre: this.nombre,
      estado: this.estado
    };

    this.salaCineService.crearSala(nuevaSala).subscribe({
      next: (salaCreada) => {
        this.mensaje = 'Sala guardada exitosamente';
        this.ultimaSala = salaCreada;
        this.salaCineService.guardarUltimaSala(salaCreada);
        this.nombre = '';
        this.estado = 'Disponible';
      },
      error: (err) => {
        this.mensaje = 'Error al guardar la sala';
        console.error(err);
      }
    });
  }
}
