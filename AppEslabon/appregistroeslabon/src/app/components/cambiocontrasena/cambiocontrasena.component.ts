import { Component, OnInit } from '@angular/core';
import { Validators, FormBuilder, FormGroup } from '@angular/forms';
import { MyValidators } from '../../validators/registroValidators';
import { EslabonService } from '../../services/eslabon.service';
import { Router } from '@angular/router';
import { UsuarioModel } from '../../models/usuario.model';

@Component({
  selector: 'app-cambiocontrasena',
  templateUrl: './cambiocontrasena.component.html',
  styleUrls: ['./cambiocontrasena.component.css']
})
export class CambiocontrasenaComponent implements OnInit {

  cambio: FormGroup;
  generos = [
    { text: 'Masculino', value: 'Masculino' },
    { text: 'Femenino', value: 'Femenino' }
  ];
  usuario: UsuarioModel;
  mensaje: string;

  constructor( private eslabonService: EslabonService,
               private fb: FormBuilder,
               private router: Router ) {
    this.crearFormulario();
  }

  ngOnInit() {
  }

  crearFormulario() {
    this.cambio = this.fb.group({
      usuario: ['', [Validators.required, Validators.minLength(7)]],
      contrasenaAnterior: ['', [Validators.required, Validators.minLength(10)]],
      contrasena: ['', [Validators.required, Validators.minLength(10)]],
      confirmar: ['', [Validators.required, Validators.minLength(10), MyValidators.checkPass]],
    });
  }

  Guardar() {
    this.eslabonService.cambiarContrasena( this.cambio.value )
      .subscribe(data => {
        if (data['codigo'] === 200) {
          this.router.navigate(['/login']);
        }
      }, error => {
        this.mensaje = 'Datos erroneos, favor de verificar';
      });
  }

}
