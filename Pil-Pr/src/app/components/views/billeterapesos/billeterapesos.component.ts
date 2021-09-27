import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Cuenta } from 'src/app/models/Cuenta';
import { Transacciones } from 'src/app/models/Transacciones';
import { ServicioService } from 'src/app/services/servicio.service';
import { TransaccionService } from 'src/app/services/transaccion.service';

@Component({
  selector: 'app-billeterapesos',
  templateUrl: './billeterapesos.component.html',
  styleUrls: ['./billeterapesos.component.css']
})
export class BilleterapesosComponent implements OnInit {

  form: FormGroup;
  submitted = false;
  cuenta:Cuenta[] = [];
  listadoTrans:Transacciones[] = [];
  
  constructor(
    private router: Router, 
    private _servicio:ServicioService, 
    private fb : FormBuilder,    
    private _toastr: ToastrService,
    private _transServi: TransaccionService
    ) {
      this.form = this.fb.group({
        CuentaOrigen: ['', Validators.required],
        CuentaDestino: ['', Validators.required],
        IngresoMonto: ['', Validators.required]
      });
     }

  ngOnInit(): void {
    this.datosCuentaPesos();
    this.ListadoDeTransacciones();
  }

  get f() { return this.form.controls; }
  
  datosCuentaPesos(){
    let id:any = [sessionStorage.getItem("Id_usuario")];
    this._servicio.datosCuentaEnPesos(id).subscribe(datosCuenta => {
      this.cuenta = datosCuenta;
  },error =>{
    console.log(error);
  });
  }
  
  onSubmit() {

      this.submitted = true;
      let fecha = new Date();
      let id:any = [sessionStorage.getItem("Id_usuario")];
      const transaccion = new Transacciones (
        parseInt("0"),
        parseInt(id), 
        parseInt(this.form.get("CuentaDestino")?.value),
        this.form.get("CuentaOrigen")?.value,
        fecha.toLocaleDateString(),       
        this.form.get("IngresoMonto")?.value
        );
  
      // No hace nada si el formulario es invalido
      if (this.form.invalid) {
        return;
      }
  
      this._transServi.IngresarDinero(transaccion).subscribe(datos => {
        this._toastr.success('Se registro correctamente', 'INGRESO DE DINERO REGISTRADO');
        this.form.reset();
        window.location.reload();
    }, error => {
        this._toastr.error(error, 'Error');
    });
  }

  ListadoDeTransacciones(){
    let id:any = [sessionStorage.getItem("Id_usuario")];
    this._transServi.ListadoDeTransacciones(id).subscribe(datos => {
      this.listadoTrans = datos;
    }, error => {
        this._toastr.error(error, 'Error');
    });
  }
  
}
