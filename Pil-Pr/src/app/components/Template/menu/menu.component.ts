import { Component, OnInit, Input } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.css']
})
export class MenuComponent implements OnInit {

  @Input() nombreUsario:any;
  UsuarioLogeado: any;

  constructor(private router: Router) { }

  ngOnInit(): void {
    this.UsuarioLogeado=sessionStorage.getItem("Usuario");
  }

  cerrarSesion(){
    localStorage.removeItem("token");
    localStorage.clear();
    sessionStorage.removeItem ("Usuario");
    sessionStorage.clear();
    this.router.navigate(['/']);
  }
}
