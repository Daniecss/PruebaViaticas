import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { SalaCineService } from '../../services/sala.service';

@Component({
  selector: 'app-actualizarsala',
  imports: [ReactiveFormsModule, FormsModule, CommonModule],
  templateUrl: './actualizarsala.component.html',
  standalone: true,
})
export class ActualizarSalaComponent implements OnInit {
    buscarForm: FormGroup;
    salaForm: FormGroup;
    salas: any[] = [];
    error: string | null = null;
    mostrarFormulario: boolean = false;
    mensaje: string = '';
    salaSeleccionada: any = null;
  
    constructor(
      private fb: FormBuilder,
      private salaService: SalaCineService
    ) {
      this.buscarForm = this.fb.group({
        id: [null, Validators.required],
      });
  
      this.salaForm = this.fb.group({
        nombre: ['', Validators.required],
        estado: ['', Validators.required],
      });
    }
  
    ngOnInit(): void {
        this.salaService.obtenerSalas().subscribe({
          next: (data) => {
            this.salas = data;
          },
          error: () => {
            this.error = 'No se pudo obtener las salas.';
          }
        });
      
        const ultimaSala = this.salaService.obtenerUltimaBusqueda();
        if (ultimaSala) {
          this.salaSeleccionada = ultimaSala;
          this.mostrarFormulario = true;
        }
      }
      
      actualizarSala() {
        const salaActualizada = {
          idSala: this.salaSeleccionada.idSala,
          nombre: this.salaForm.value.nombre,
          estado: this.salaForm.value.estado,
        };
      
        this.salaService.updateSala(salaActualizada).subscribe({
          next: () => {
            this.mensaje = 'Sala actualizada con éxito.';
            this.salaSeleccionada.nombre = salaActualizada.nombre;
            this.salaSeleccionada.estado = salaActualizada.estado;
      
            this.salaService.guardarUltimaBusqueda(this.salaSeleccionada);
      
            this.salaForm.reset();
            this.mostrarFormulario = false;
          },
          error: (err) => {
            console.error('Error al actualizar la sala:', err);
            this.error = 'Ocurrió un error al actualizar la sala.';
          }
        });
      }
  
    buscarSala() {
      const id = this.buscarForm.value.id;
      this.salaService.buscarSalaPorId(id).subscribe({
        next: (data) => {
          this.salaSeleccionada = data;
          this.salaForm.setValue({
            nombre: data.nombre,
            estado: data.estado,
          });
          this.mostrarFormulario = true; 
        },
        error: () => {
          this.error = 'No se encontró la sala con ese ID.';
        }
      });
    }
  
  }