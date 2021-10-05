import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Cuenta } from 'src/app/models/Cuenta';
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
  form2: FormGroup;
  submitted2 = false;
  id_cuenta:number = 0;
  id_servicio:number = 0;

  
  constructor(
    private router: Router,
    private _servicio:ServicioService,
    private fb2 : FormBuilder,
    private _toastr: ToastrService,
    ) { 
      this.form2 = this.fb2.group({
        // CuentaDestino2: ['', [Validators.required, Validators.minLength(22), Validators.maxLength(22)]],
        CuentaOrigen2: ['', Validators.required],
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
      console.log(this.cuenta);
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

  ultimosMovimientos(){
    let id:any = [sessionStorage.getItem("Id_usuario")];
    this._servicio.ultimosMovimientos(id).subscribe(datosUltimoMovimiento => {
      this.ultimos = datosUltimoMovimiento;
  },error =>{
    console.log(error);
  });
  }

  PagoServicio(){

  }
}
