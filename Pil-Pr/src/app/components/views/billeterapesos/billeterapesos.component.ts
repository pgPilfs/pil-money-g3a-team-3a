import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Cuenta } from 'src/app/models/Cuenta';
import { Transacciones } from 'src/app/models/Transacciones';
import { ServicioService } from 'src/app/services/servicio.service';

@Component({
  selector: 'app-billeterapesos',
  templateUrl: './billeterapesos.component.html',
  styleUrls: ['./billeterapesos.component.css']
})
export class BilleterapesosComponent implements OnInit {

  form: FormGroup;
  submitted = false;
  cuenta:Cuenta[] = [];
  
  constructor(
    private router: Router, 
    private _servicio:ServicioService, 
    private fb : FormBuilder,    
    private _toastr: ToastrService
    ) {
      this.form = this.fb.group({
        CuentaOrigen: ['', Validators.required],
        IngresoMonto: ['', Validators.required]
      });
     }

  ngOnInit(): void {
    this.datosCuentaPesos();
  }

  get f() { return this.form.controls; }
  
  datosCuentaPesos(){
    let id = [2];
    this._servicio.datosCuentaEnPesos(id).subscribe(datosCuenta => {
      this.cuenta = datosCuenta;
      console.log(this.cuenta);
  },error =>{
    console.log(error);
  });
  }
  
  ingresarDinero() {
    console.log(this.f);
  }

  onSubmit() {

    this.submitted = true;
    let fecha = new Date();
    let id_cuenta = "0000525696548552215485";
    const transaccion = new Transacciones (
      0,
      2, 
      this.form.get("CuentaOrigen")?.value,
      id_cuenta, 
      fecha,       
      this.form.get("IngresoMonto")?.value
      );
      console.log(transaccion);

    // No hace nada si el formulario es invalido
    if (this.form.invalid) {
      return;
    }


  }
  
}
