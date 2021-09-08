export class Autenticacion{
    
    private Id?:number;
    private IdUsuario?:number;
    private Token?:string;
    private Fecha?:Date;
    private Estado?:number;

    constructor(id?:number, idUsuario?:number, token?:string, fecha?:Date, estado?:number){
        this.Id = id;
        this.IdUsuario = idUsuario;
        this.Token = token;
        this.Fecha = fecha;
        this.Estado = estado;
    }
    
}