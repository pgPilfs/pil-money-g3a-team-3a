import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  form: FormGroup;
  loading = false;
  submitted = false;

  constructor(
    private fb : FormBuilder,
    private router: Router
  ) {
    this.form = this.fb.group({
      username: ['', Validators.required],
      password: ['', Validators.required]
    });
   }

  ngOnInit(): void {
  }

  // Getter conveniente para acceder mas facil a los campos de form
  get f() { return this.form.controls; }

  onSubmit() {

    this.submitted = true;

    // No hace nada si el formulario es invalido
    if (this.form.invalid) {
      return;
    }

    this.loading = true;

    //Enviamos informacion al backend 

    //Redigis al dashboard
    console.log(this.form)
    this.router.navigate(['/home']);
  }
}
