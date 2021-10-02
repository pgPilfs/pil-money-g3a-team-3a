import { Component, OnInit, NgModule } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Usuario } from 'src/app/models/Usuario';
import { UsuarioService } from 'src/app/services/usuario.service';

@Component({
  selector: 'app-perfil',
  templateUrl: './perfil.component.html',
  styleUrls: ['./perfil.component.css']
})

export class PerfilComponent implements OnInit {

  Usuario:Usuario[] = [];
  
  constructor(
    private fb: FormBuilder,
    private router: Router,
    private _UsuarioService : UsuarioService,
    private _toastr: ToastrService
  ) { }

  ngOnInit(): void {
    this.DatosUsuario();
  }

  DatosUsuario(){
    let id:any = [sessionStorage.getItem("Id_usuario")];
    this._UsuarioService.detalleUsuario(id).subscribe(datos => {
    this.Usuario = datos
    console.log(this.Usuario); 
  },error =>{
    console.log(error);
  });
}

}
