export class PagoServicio {
    constructor(
        public Id:number,
        public Servicio:number,
        public CuentaOrigen:number,
        public CVUServicio:string,
        public Fecha:string,
        public Monto:number,
    ){}
}