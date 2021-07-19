import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Archivo } from 'src/app/models/Archivo';
import { User } from 'src/app/models/User';
import { AccountService } from 'src/app/services/account.service';
import { ImagenService } from 'src/app/services/imagen.service';

@Component({
  selector: 'app-verificacion',
  templateUrl: './verificacion.component.html',
  styleUrls: ['./verificacion.component.css']
})
export class VerificacionComponent implements OnInit {

  user: User;
  formulario : FormGroup;

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private img: ImagenService,
    private accountService: AccountService) { 
      //Referencia al usuario logeado  
      this.user = this.accountService.userValue;
      this.formulario = this.fb.group({
        img1: [Array, Validators.required],
        img2: [Array,Validators.required]
      });
  }

  ngOnInit(): void {
  }

  // Getter conveniente para acceder mas facil a los campos de form
  get f() { return this.formulario.controls; }

  
  fileEvent(imgInput: Event){
    let file = (<HTMLInputElement>imgInput.target).files![0];
    this.formulario.patchValue(
      {
        //img1: new Archivo(1,file.name,file.type)
        img1: file
      });
  }

  fileEvent2(imgInput: Event){
    let file = (<HTMLInputElement>imgInput.target).files![0];
    this.formulario.patchValue(
      {
        //img2: new Archivo(2,file.name,file.type)
        img2: file
      });
  }

  subirArchivos(){
    //Subir imagen 1
    this.img.uploadFiles(this.user.id, this.formulario.value).subscribe(data => {
      console.log("se subion las imagenes")
    })
    console.log("Se subieron las imagenes")
    //Redigir a la pagina de login
    this.router.navigate(['/login']);
  }

}
