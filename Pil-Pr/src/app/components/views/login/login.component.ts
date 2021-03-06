import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Login } from 'src/app/models/Login'
import { LoginService } from 'src/app/services/login.service';

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
    private router: Router,
    private _loginService : LoginService,
    private _toastr: ToastrService
  ) {
    this.form = this.fb.group({
      username: ['', Validators.required],
      password: ['', [Validators.required, Validators.minLength(8)]]
    });
   }

  ngOnInit(): void {
    if (localStorage.getItem("token")){
      this.router.navigate(['/dashboard']);
    }
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

    // creamos el objeto login con el constructor con parametros pasandole los valores de los campos del 
    // formulario de la vista
    const inicioSesion = new Login(this.form.get('username')?.value, this.form.get('password')?.value);
  
    // nos sucribimos al observable del sevicio login.
    this._loginService.login(inicioSesion).subscribe(datos => {
      if(!datos){
        this._toastr.error('Usuario o Cantraseña Incorrecta', 'DATOS INCORRECTOS');
        this.form.reset();
      }else{
        // localStorage token
        localStorage.setItem('token', datos[2]);
        sessionStorage.setItem("Usuario", datos[0]);
        sessionStorage.setItem("Id_usuario", datos[1]);
        // //Redirecciona al dashboard
        this.router.navigate(['/dashboard']);
      }
    }, error => {
      this._toastr.error(error.message, 'Error');
    });
  }
}
