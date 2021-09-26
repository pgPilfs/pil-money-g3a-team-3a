export class Transacciones{

    constructor(
        public Id: number,
        public TipoTransaccion: number,
        public CuentaOrigen: number,
        public CuentaDestino: string,
        public FechaTransaccion: Date,
        public Monto: number    
    ){}
}