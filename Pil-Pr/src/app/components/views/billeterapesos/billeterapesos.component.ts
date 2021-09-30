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
  // form2: FormGroup;
  submitted = false;
  // submitted2 = false;
  cuenta:Cuenta[] = [];
  listadoTrans:Transacciones[] = [];
  id_cuenta:number = 0;
  
  constructor(
    private router: Router, 
    private _servicio:ServicioService, 
    private fb : FormBuilder,    
    private _toastr: ToastrService,
    private _transServi: TransaccionService
    ) {
      this.form = this.fb.group({
        CuentaOrigen: ['', [Validators.required, Validators.minLength(22), Validators.maxLength(22)]],
        CuentaDestino: ['', Validators.required],
        IngresoMonto: ['', Validators.required],
        CuentaDestino2: ['', [Validators.required, Validators.minLength(22), Validators.maxLength(22)]],
        CuentaOrigen2: ['', Validators.required],
        IngresoMonto2: ['', Validators.required]
      });
    }

  ngOnInit(): void {
    this.datosCuentaPesos();
    this.ListadoDeTransacciones();
  }

  get f() { return this.form.controls; }
  // get f2() { return this.form2.controls; }
  
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
      this.id_cuenta = parseInt(id);
      console.log(this.id_cuenta);
      const transaccion = new Transacciones (
        parseInt("0"),
        parseInt("2"),
        this.id_cuenta, 
        this.form.get("CuentaOrigen")?.value,
        fecha.toLocaleDateString(),       
        this.form.get("IngresoMonto")?.value
        );
      console.log(transaccion);
      // No hace nada si el formulario es invalido
      if (this.form.invalid) {
        return;
      }
      this._transServi.IngresarDinero(transaccion).subscribe(datos => {
        this._toastr.success('Se registro correctamente', 'INGRESO DE DINERO REGISTRADO');
        setTimeout(() =>{
          window.location.reload();
        }, 2000);
    }, error => {
        this._toastr.error(error.message, 'Error');
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

  Transferencias(){
    this.submitted = true;
    console.log("estoy en el método");
  }

  // limpiar(){
  //   this.form.clearValidators();
  //   console.log("estoy en el método");
  // }
  
}
