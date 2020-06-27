import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { UsuarioModel } from '../models/usuario.model';
import { LoginModel } from '../models/login.model';
import { CambioModel } from '../models/cambio.model';

@Injectable({
  providedIn: 'root'
})
export class EslabonService {

  constructor( private http: HttpClient ) { }

  getQuery( query: string = '' ) {
    const url = `https://localhost:44350/api/Usuarios/${ query }`;
    return url;
  }

  Login( usuario: LoginModel ) {
    return this.http.post( this.getQuery('Login'), usuario );
  }

  getUsuarios() {
    return this.http.get<UsuarioModel[]>( this.getQuery('ListaUsuarios') );
  }

  getUsuario( usuario: string ) {
    return this.http.get<UsuarioModel>( this.getQuery( `VerUsuario/${ usuario }` ) );
  }

  registrarUsuario( usuario: UsuarioModel ) {
    return this.http.post( this.getQuery( `RegistrarUsuario`), usuario );
  }

  actualizarUsuario( usuario: UsuarioModel ) {
    return this.http.put( this.getQuery( `ActualizarUsuario`), usuario );
  }

  cambiarContrasena( usuario: CambioModel ) {
    return this.http.put( this.getQuery( `CambiarContrasena`), usuario );
  }

  activarDesactivar( id: number) {
    return this.http.put( this.getQuery( `ActivarDesactivar/${ id }`), {} );
  }
}
