import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { TipoServicio } from 'src/app/models/TipoServicio';
import { ServicioService } from 'src/app/services/servicio.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  listadoServicio:TipoServicio[] = []; 

  constructor(private router: Router, private _servicio:ServicioService) { }

  ngOnInit(): void {
    this.validarPermiso();
    this.listado();
  }
  
  validarPermiso(){
    if(!sessionStorage.getItem('token')){
      this.router.navigate(['/']);
    }
  }

  listado(){
      this._servicio.listadoTipoServicio().subscribe(datos => {
       this.listadoServicio = datos;
       console.log(this.listadoServicio);
    });
  }

}
