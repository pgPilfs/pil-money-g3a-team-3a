export class Cuenta {

    constructor(
        public TipoCuenta:string,
        public TipoMoneda:string,
        public NombreApellido:string,
        public CVU:string,
        public Alias:string,
        public Saldo:number,
        public FechaAlta:string
    ) {}
}