import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { first } from 'rxjs/operators';
import { AccountService } from 'src/app/services/account.service';

@Component({
  selector: 'app-registro',
  templateUrl: './registro.component.html',
  styleUrls: ['./registro.component.css']
})
export class RegistroComponent implements OnInit {
  
  formulario : FormGroup;
  loading = false;
  submitted = false;
  aceptarTerminos: any;

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private accountService: AccountService, 
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

    var element = <HTMLInputElement> document.getElementById("checked");
    this.aceptarTerminos = element.checked;

    console.log(this.formulario)
    console.log(this.aceptarTerminos)

    // Para aca si el formulario es invalido
    if (this.formulario.invalid) {
      console.log("formulario incompleto")
      return;
    } 

    if (this.aceptarTerminos == false){
      console.log("No se aceptaron terminos")
      return;
    }

    if(this.aceptarTerminos){
      //Envio de formulario al backend
      this.accountService.register(this.formulario.value)
        .pipe(first())
        .subscribe({
            next: () => {
                this.router.navigate(['/verificacion'], { relativeTo: this.route });
            },
            error: error => {
                this.loading = false;
            }
        });

      //this.router.navigate(['/verificacion']);
    }
    
    //Redigir a la pagina de login
    //this.router.navigate(['/login']);
  }

}
