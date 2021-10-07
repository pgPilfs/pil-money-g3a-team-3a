import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Cuenta } from 'src/app/models/Cuenta';
import { PagoServicio } from 'src/app/models/PagoServicio';
import { Servicio } from 'src/app/models/Servicio';
import { TipoServicio } from 'src/app/models/TipoServicio';
import { ServicioService } from 'src/app/services/servicio.service';

@Component({
  selector: 'app-pagoservicios',
  templateUrl: './pagoservicios.component.html',
  styleUrls: ['./pagoservicios.component.css']
})
export class PagoserviciosComponent implements OnInit {

  listadoServicio:TipoServicio[] = [];
  ultimos:any[] = [];
  cuenta:Cuenta[] = [];
  servicio:Servicio[] = [];
  form2: FormGroup;
  submitted2 = false;
  id_cuenta:number = 0;
  id_servicio:number = 0;
  id_cuenta_origen:any;

  
  constructor(
    private router: Router,
    private _servicio:ServicioService,
    private fb2 : FormBuilder,
    private _toastr: ToastrService,
    ) { 
      this.form2 = this.fb2.group({
        // CuentaDestino2: ['', [Validators.required, Validators.minLength(22), Validators.maxLength(22)]],
        // CuentaOrigen2: ['', Validators.required],
        IngresoMonto2: ['', Validators.required]
      });
    }

  ngOnInit(): void {
    this.listado();
    this.ultimosMovimientos();
    this.datosCuentaPesos();
  }

  get f2() { return this.form2.controls; }

  datosCuentaPesos(){
    let id:any = [sessionStorage.getItem("Id_usuario")];
    this._servicio.datosCuentaEnPesos(id).subscribe(datosCuenta => {
      this.cuenta = datosCuenta;
  },error =>{
    console.log(error);
  });
  }
  
  listado(){
    this._servicio.listadoTipoServicio().subscribe(datosServicio => {
      this.listadoServicio = datosServicio;
    },error =>{
      console.log(error);
    });
  }

  obtenerId(id:any){
    this.id_servicio = id;
    this.datosServicio();
  }

  datosServicio(){
    this._servicio.datosServicio([this.id_servicio]).subscribe(datos => {
      this.servicio = datos;
  },error =>{
    console.log(error);
  });
  }

  ultimosMovimientos(){
    let id:any = [sessionStorage.getItem("Id_usuario")];
    this._servicio.ultimosMovimientos(id).subscribe(datosUltimoMovimiento => {
      this.ultimos = datosUltimoMovimiento;
  },error =>{
    console.log(error);
  });
  }

  PagoServicio(){
    this.submitted2 = true;
    if (this.form2.invalid) {
      console.log("dentro del metodo");
      return;
    }
    this.id_cuenta_origen  = sessionStorage.getItem("Id_usuario");
    let id = -1;
    let servicio = this.id_servicio;
    let cuentaOrigen = parseInt(this.id_cuenta_origen);
    let CVUServicio = this.servicio[0]?.CVUServicio;
    let fechaPago = new Date();
    let monto = this.form2.get("IngresoMonto2")?.value;

    const pagoServicio = new PagoServicio(id, servicio, cuentaOrigen, CVUServicio, fechaPago.toLocaleString(), monto);

    console.log(pagoServicio);

    this._servicio.pagoServicio(pagoServicio).subscribe(datos => {
      this._toastr.success('Se registro correctamente pago del servicio', 'PAGO DEL SERVICIO FUE REGISTRADO');
      setTimeout(() =>{
        window.location.reload();
      }, 2000);
    }, error => {
        this._toastr.error(error.message, 'Error');
    });
  }
}
