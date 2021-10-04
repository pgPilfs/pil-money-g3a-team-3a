import { Component, OnInit, NgModule } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { DomSanitizer } from '@angular/platform-browser';
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
  img_doc : any;
  
  constructor(
    private fb: FormBuilder,
    private router: Router,
    private _UsuarioService : UsuarioService,
    private _toastr: ToastrService,
    private sanitizer: DomSanitizer 
  ) { }

  ngOnInit(): void {
    this.DatosUsuario();
  }

  DatosUsuario(){
    let id:any = [sessionStorage.getItem("Id_usuario")];
    this._UsuarioService.detalleUsuario(id).subscribe(datos => {
      this.Usuario = datos;
      console.log(this.Usuario[0].FotoPerfil);
      this.img_doc = this.Usuario[0].FotoPerfil;
  },error =>{
    console.log(error);
  });

}

extraerBase64 = async ($event: any) => new Promise((resolve, reject) => {
  const unsafeImg = window.URL.createObjectURL($event);
  const image = this.sanitizer.bypassSecurityTrustUrl(unsafeImg);
  const reader = new FileReader();
  reader.readAsDataURL($event);
  reader.onload = () => {
    resolve({
      base: reader.result,
      image
    });
  };
  reader.onerror = error => {
    resolve({
      base: null
    });
  };  
});

}
