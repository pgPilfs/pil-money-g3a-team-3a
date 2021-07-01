import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-registro',
  templateUrl: './registro.component.html',
  styleUrls: ['./registro.component.css']
})
export class RegistroComponent implements OnInit {
  
  formulario : FormGroup;
  loading = false;
  submitted = false;

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
      password: ['', [Validators.required, Validators.minLength(8)]],
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
    this.router.navigate(['/login']);
  }
}
