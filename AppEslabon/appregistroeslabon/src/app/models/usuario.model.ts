export class UsuarioModel {
    id?: number;
    correo: string;
    usuario: string;
    contrasena: string;
    estatus: boolean;
    sexo: string;
    fechaCreacion?: Date;
}
