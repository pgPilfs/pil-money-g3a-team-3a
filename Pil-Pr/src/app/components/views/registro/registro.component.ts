import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Archivo } from 'src/app/models/Archivo';

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
  aceptarTerminos: boolean = false;

  constructor(
    private fb: FormBuilder,
    private router: Router    
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

    console.log(this.formulario)

    // Para aca si el formulario es invalido
    if (this.formulario.invalid) {
      console.log("formulario incompleto")
      return;
    }

    //Envio de formulario al backend 

    //Redigir a la pagina de login
    //this.router.navigate(['/login']);
  }
  
  fileEvent(imgInput: Event){
    let file = (<HTMLInputElement>imgInput.target).files![0];
    this.img_doc = new Archivo(1,file.name,file.type);
  }

  fileEvent2(imgInput: Event){
    let file = (<HTMLInputElement>imgInput.target).files![0];
    this.img_doc_2 = new Archivo(2,file.name,file.type);
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

  aceparTerminosYCondiciones(){
    this.aceptarTerminos = true;
  }
}
