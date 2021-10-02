

export class Usuario{

    // private Id:number;
    // private DNI:string;
    // private Nombre:string;
    // private Apellido:string;
    // private Email:string;
    // private NombreUsuario:string;
    // private Clave:string;
    // private FotoPerfil:string;
    // private FotoDNI:string;


    // constructor(id:number, dni:string, nombre:string, 
    //     apellido:string, email:string, nombreUsuario:string, clave:string, fotoPerfil:string, fotoDNI:string){
    //     this.Id = id;
    //     this.DNI = dni;
    //     this.Nombre = nombre;
    //     this.Apellido = apellido;
    //     this.Email = email;
    //     this.NombreUsuario = nombreUsuario;
    //     this.Clave = clave;
    //     this.FotoPerfil = fotoPerfil;
    //     this.FotoDNI = fotoDNI;
    // }

    constructor(
        public Id:number,
        public DNI:string,
        public Nombre:string,
        public Apellido:string,
        public Email:string,
        public NombreUsuario:string,
        public Clave:string,
        public FotoPerfil:string,
        public FotoDNI:string
    ){}
}