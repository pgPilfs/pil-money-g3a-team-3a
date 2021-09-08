import { Autenticacion } from "./Autenticacion";

export class Usuario{

    private Id:number;
    private DNI:string;
    private Nombre:string;
    private Apellido:string;
    private Email:string;
    private NombreUsuario:string;
    private Clave:string;
    private FotoPerfil:string;
    private FotoDNI:string;
    private autenticacion:Autenticacion;

    constructor(id:number, dni:string, nombre:string, 
        apellido:string, email:string, nombreUsuario:string, clave:string, fotoPerfil:string, fotoDNI:string, au:Autenticacion){
        this.Id = id;
        this.DNI = dni;
        this.Nombre = nombre;
        this.Apellido = apellido;
        this.Email = email;
        this.NombreUsuario = nombreUsuario;
        this.Clave = clave;
        this.FotoPerfil = fotoPerfil;
        this.FotoDNI = fotoDNI;
        this.autenticacion = au;
    }
}