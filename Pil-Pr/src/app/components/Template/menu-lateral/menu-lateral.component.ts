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
    sessionStorage.removeItem("user");
    sessionStorage.removeItem("token");
    sessionStorage.clear();
    this.router.navigate(['/']);
  }

}
