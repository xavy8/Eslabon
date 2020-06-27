import { Component, OnInit } from '@angular/core';
import { EslabonService } from '../../services/eslabon.service';
import { UsuarioModel } from '../../models/usuario.model';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MyValidators } from '../../validators/registroValidators';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  usuarios: UsuarioModel[];
  usuarioActualizar: UsuarioModel;
  actualizar: FormGroup;
  headers = [
    {text: 'Id', value: 'id'},
    {text: 'Correo', value: 'correo'},
    {text: 'Usuario', value: 'usuario'},
    {text: 'Estatus', value: 'estatus'},
    {text: 'Sexo', value: 'sexo'},
    {text: 'Fecha de Alta', value: 'fechaCreacion'}
  ];
  generos = [
    { text: 'Masculino', value: 'Masculino' },
    { text: 'Femenino', value: 'Femenino' }
  ];
  showModal = false;

  constructor( private eslabonService: EslabonService,
               private fb: FormBuilder ) {
    this.cargarInformacion();
  }

  ngOnInit() {
  }

  crearFormulario() {
    this.actualizar = this.fb.group({
      Id: [this.usuarioActualizar.id, [Validators.required]],
      usuario: [this.usuarioActualizar.usuario, [Validators.required, Validators.minLength(7)]],
      correo: [this.usuarioActualizar.correo, [Validators.required, Validators.email, Validators.pattern('[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,3}$')]],
      sexo: [this.usuarioActualizar.sexo, [Validators.required]]
    });
  }

  cargarInformacion() {
    this.eslabonService.getUsuarios()
      .subscribe( data => {
        this.usuarios = data;
      });
  }

  AbrirModal( usuario: UsuarioModel ) {
    this.showModal = true;
    this.usuarioActualizar = new  UsuarioModel();
    this.usuarioActualizar = usuario;
    this.crearFormulario();
  }

  Actualizar() {
    this.eslabonService.actualizarUsuario( this.actualizar.value )
      .subscribe(data => {
        this.cargarInformacion();
        document.getElementById('close').click();
      }, error => {
      });
  }

  DesActivar( id: number ) {
    this.eslabonService.activarDesactivar( id )
      .subscribe( data => {
        this.cargarInformacion();
      });
  }
}
