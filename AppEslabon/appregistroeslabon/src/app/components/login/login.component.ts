import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { EslabonService } from '../../services/eslabon.service';
import { Router } from '@angular/router';
import { UsuarioModel } from '../../models/usuario.model';
import { MyValidators } from '../../validators/registroValidators';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  login: FormGroup;
  usuarios: UsuarioModel[];
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
    this.login = this.fb.group({
      usuario: ['', [Validators.required, Validators.minLength(7)]],
      contrasena: ['', [Validators.required, Validators.minLength(10)]]
    });
  }

  Entrar() {
    this.eslabonService.Login(this.login.value)
      .subscribe( data => {
        if (data['codigo'] === 200) {
          this.router.navigate(['/home']);
        }
      }, error => {
        this.login.get('usuario').setValue('');
        this.login.get('contrasena').setValue('');
        this.mensaje = 'Datos incorrectos';
      });
  }

}
