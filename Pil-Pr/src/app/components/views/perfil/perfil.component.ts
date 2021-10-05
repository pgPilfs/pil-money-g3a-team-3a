import { Component, OnInit} from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
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
  img_doc_2 : any;
  img_actual: any;
  img_final:any;
  submitted = false;
  formulario : FormGroup;
  
  constructor(
    private fb: FormBuilder,
    private router: Router,
    private _UsuarioService : UsuarioService,
    private _toastr: ToastrService,
    private sanitizer: DomSanitizer 
  ) { 
    this.formulario = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['',Validators.required],
      mail: ['', [Validators.required, Validators.email]]
    });
  }

  ngOnInit(): void {
    this.DatosUsuario();
  }

  get f() { return this.formulario.controls; }

  DatosUsuario(){
    let id:any = [sessionStorage.getItem("Id_usuario")];
    this._UsuarioService.detalleUsuario(id).subscribe(datos => {
      this.Usuario = datos;
      this.img_actual = this.Usuario[0].FotoPerfil;
      console.log(this.Usuario);
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
      base: reader.result
    });
  };
  reader.onerror = error => {
    resolve({
      base: null
    });
  };  
});

modificarUsuario(){
  this.submitted = true;

  let id:any = sessionStorage.getItem("Id_usuario");

    // Para aca si el formulario es invalido
    if (this.formulario.invalid) {
      console.log("formulario incompleto");
      return;
    }
    if(this.img_doc_2){
      this.img_final = this.img_doc_2;
    }else{
      this.img_final = this.img_actual;
    }
    // console.log(this.f.firstName.value);
    // console.log(this.f.lastName.value);
    // console.log(this.f.mail.value);
    // console.log(this.img_final);

    const user = new Usuario(parseInt(id), "", this.f.firstName.value, this.f.lastName.value,  this.f.mail.value,"", "", this.img_final, "");
    console.log(user);

    //Envio de formulario al backend 
    this._UsuarioService.actualizarUsuario(user).subscribe(datos => {
        this._toastr.success('Se actualizo correctamente', 'USUARIO ACTUALIZADO');
        this.router.navigate(['/dashboard']);
    }, error => {
        this._toastr.error(error, 'Error');
    });
}

fileEvent(imgInput: Event){
  let file = (<HTMLInputElement>imgInput.target).files![0];
  this.img_doc = file;
  this.extraerBase64(this.img_doc).then((imagen: any) => {
    this.img_doc_2 = imagen.base;
    console.log(imagen);
  });
}

}