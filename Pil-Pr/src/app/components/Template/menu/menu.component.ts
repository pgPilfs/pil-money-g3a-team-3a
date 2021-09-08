import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.css']
})
export class MenuComponent implements OnInit {

  nombreUsario:any;

  constructor(private router: Router) { }

  ngOnInit(): void {
    this.nombreUsario = sessionStorage.getItem("user");
  }

  cerrarSesion(){
    sessionStorage.removeItem("user");
    sessionStorage.removeItem("token");
    sessionStorage.clear();
    this.router.navigate(['/']);
  }

}
