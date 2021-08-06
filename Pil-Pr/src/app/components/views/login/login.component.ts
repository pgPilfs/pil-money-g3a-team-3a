import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { first } from 'rxjs/operators';
import { AccountService } from 'src/app/services/account.service';

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
    private accountService: AccountService,
    private route: ActivatedRoute,
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
    this.accountService.login(this.f.username.value, this.f.password.value)
            .pipe(first())
            .subscribe({
                next: () => {
                    // obtener la URL de retorno de los parámetros de consulta o por defecto a la página de inicio
                    const returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/dashboard';
                    this.router.navigateByUrl(returnUrl);
                }
            });

    //Redigis al dashboard
    console.log(this.form)
    //this.router.navigate(['/home']);
  }
}
