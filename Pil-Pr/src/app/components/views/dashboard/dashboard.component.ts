import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ServicioService } from 'src/app/services/servicio.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  constructor(private router: Router, private _servicio:ServicioService) { }

  ngOnInit(): void {
    this.validarPermiso();
    // this.listado();
  }
  
  validarPermiso(){
    if(!sessionStorage.getItem('token')){
      this.router.navigate(['/']);
    }
  }

  listado(){
      this._servicio.listadoTipoServicio().subscribe(datos => {
      // console.log(JSON.stringify(datos));
      return JSON.stringify(datos);
    });
  }

}
