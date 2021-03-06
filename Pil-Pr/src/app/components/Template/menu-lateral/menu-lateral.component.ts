import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-menu-lateral',
  templateUrl: './menu-lateral.component.html',
  styleUrls: ['./menu-lateral.component.css']
})
export class MenuLateralComponent implements OnInit {

  constructor(private router: Router) { }

  ngOnInit(): void {
  }

  cerrarSesion(){
    localStorage.removeItem("token");
    sessionStorage.removeItem ("Usuario");
    sessionStorage.removeItem("Id_usuario");
    sessionStorage.clear();
    localStorage.clear();
    this.router.navigate(['/']);
  }

}
