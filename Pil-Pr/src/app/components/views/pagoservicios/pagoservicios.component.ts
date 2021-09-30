import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
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

  constructor(private router: Router, private _servicio:ServicioService) { }

  ngOnInit(): void {
    this.listado();
    this.ultimosMovimientos();
    this.datosCuentaPesos();
  }

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

  ultimosMovimientos(){
    let id:any = [sessionStorage.getItem("Id_usuario")];
    this._servicio.ultimosMovimientos(id).subscribe(datosUltimoMovimiento => {
      this.ultimos = datosUltimoMovimiento;
  },error =>{
    console.log(error);
  });
  }
}
