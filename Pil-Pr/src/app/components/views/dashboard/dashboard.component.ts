import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Cuenta } from 'src/app/models/Cuenta';
import { Transacciones } from 'src/app/models/Transacciones';
import { ServicioService } from 'src/app/services/servicio.service';
import { TransaccionService } from 'src/app/services/transaccion.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  cuenta:Cuenta[] = [];
  ultimos:any[] = [];
  listadoTrans:Transacciones[] = [];
  listadoFinal:any[] = [];
  cantidadMovimientos: any;

  constructor(
    private router: Router,
    private _servicio:ServicioService,
    private _toastr: ToastrService,
    private _transServi: TransaccionService,
    ) { }

  ngOnInit(): void {
    this.datosCuentaPesos();    
    this.ultimosMovimientos();
    this.ListadoDeTransacciones();
     
  }  
 
  datosCuentaPesos(){
      let id:any = [sessionStorage.getItem("Id_usuario")];
      console.log(id);
      this._servicio.datosCuentaEnPesos(id).subscribe(datosCuenta => {
        this.cuenta = datosCuenta;           
    },error =>{
      console.log(error);
    });
    
  }

  ultimosMovimientos(){
    let id:any = [sessionStorage.getItem("Id_usuario")];
    this._servicio.ultimosMovimientos(id).subscribe(datosUltimoMovimiento => {
      this.ultimos = datosUltimoMovimiento;
      this.listadoFinal.push(this.ultimos);
  },error =>{
    console.log(error);
  });
}

  ListadoDeTransacciones(){
    let id:any = [sessionStorage.getItem("Id_usuario")];
    this._transServi.ListadoDeTransacciones(id).subscribe(datos => {
      this.listadoTrans = datos;
      this.cantidadMovimientos = this.listadoTrans.length;
    }, error => {
        this._toastr.error(error, 'Error');
    });
  }
  
}
