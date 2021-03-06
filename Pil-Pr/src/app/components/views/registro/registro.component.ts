import { Component, OnInit } from '@angular/core';
import { async } from '@angular/core/testing';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Usuario } from 'src/app/models/Usuario';
import { UsuarioService } from 'src/app/services/usuario.service';
import { DomSanitizer } from '@angular/platform-browser';

@Component({
  selector: 'app-registro',
  templateUrl: './registro.component.html',
  styleUrls: ['./registro.component.css']
})
export class RegistroComponent implements OnInit {
  
  formulario : FormGroup;
  loading = false;
  submitted = false;
  img_doc : any;
  img_doc_2 : any;
  aceptarTerminos: boolean = true;

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
      dni: ['', [Validators.required, Validators.pattern('[0-9]{8}')]],
      mail: ['', [Validators.required, Validators.email]],
      username: ['', Validators.required],
      password: ['', [Validators.required, Validators.minLength(8)]]
    });
  }

  ngOnInit(): void {
  }

  
  // Getter conveniente para acceder mas facil a los campos de form
  get f() { return this.formulario.controls; }

  onSubmit() {
    this.submitted = true;

    
    
    // Para aca si el formulario es invalido
    if (this.formulario.invalid) {
      console.log("formulario incompleto")
      return;
    }
    const user = new Usuario(-1, this.f.dni.value, this.f.firstName.value, this.f.lastName.value,  this.f.mail.value,
                    this.f.username.value, this.f.password.value, this.img_doc_2, "");


    //Envio de formulario al backend 
    this._UsuarioService.agregarUsuario(user).subscribe(datos => {
        this._toastr.success('Se registro correctamente', 'USUARIO REGISTRADO');
        this.router.navigate(['/login']);
    }, error => {
        this._toastr.error(error, 'Error');
    });

    //Redigir a la pagina de login
    //this.router.navigate(['/login']);
  }
  
  fileEvent(imgInput: Event){
    let file = (<HTMLInputElement>imgInput.target).files![0];
    this.img_doc = file;
    this.extraerBase64(this.img_doc).then((imagen: any) => {
      this.img_doc_2 = imagen.base;
      console.log(imagen);
    });
    //console.log(this.extraerBase64(this.img_doc));
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

  fileEvent2(imgInput: Event){
    let file = (<HTMLInputElement>imgInput.target).files![0];
    this.img_doc_2 = file;
  }

  subirArchivos(){
    //Subir imagen 1
    /*this.img.uploadFiles(this.img_doc).subscribe(data => {
      console.log("se subio excelente la imagen 1")
    })*/

    //subir imagen 2
    /*
    this.img.uploadFiles(this.img_doc_2).subscribe(data => {
      console.log("se subio excelente la imagen 2")
    })
    */
    
    console.log("Se subieron las imagenes")
    console.log(this.img_doc);
    console.log(this.img_doc_2)

    //Redigir a la pagina de login
    this.router.navigate(['/login']);
  }
}
