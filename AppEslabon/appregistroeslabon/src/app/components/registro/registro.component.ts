import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators, FormsModule, FormBuilder } from '@angular/forms';
import { EslabonService } from '../../services/eslabon.service';
import { MyValidators } from '../../validators/registroValidators';
import { UsuarioModel } from '../../models/usuario.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-registro',
  templateUrl: './registro.component.html',
  styleUrls: ['./registro.component.css']
})
export class RegistroComponent implements OnInit {

  registro: FormGroup;
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
    this.registro = this.fb.group({
      usuario: ['', [Validators.required, Validators.minLength(7)]],
      correo: ['', [Validators.required, Validators.email, Validators.pattern('[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,3}$')]],
      contrasena: ['', [Validators.required, Validators.minLength(10)]],
      confirmar: ['', [Validators.required, Validators.minLength(10), MyValidators.checkPass]],
      sexo: ['', [Validators.required]]
    });
  }

  Guardar() {
    this.eslabonService.registrarUsuario( this.registro.value )
      .subscribe(data => {
        if (data['codigo'] === 200) {
          this.router.navigate(['/login']);
        }
      }, error => {
        this.mensaje =error.error;
      });
  }

}
