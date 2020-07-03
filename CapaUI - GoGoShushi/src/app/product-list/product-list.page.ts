import { Component, OnInit } from '@angular/core';
import { NavController } from '@ionic/angular';
import { WebService } from '../services/webservice.service';
import { UtilService } from '../services/util.service';
import { Carta } from '../models/cruds.class';
import { Route } from '@angular/compiler/src/core';
import { Router } from '@angular/router';
import { CarritoService } from '../services/carrito.service';
import { AuthenticationService } from '../services/Authentication.service';


@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.page.html',
  styleUrls: ['./product-list.page.scss'],
})
export class ProductListPage implements OnInit {

  lorem: string;

  cartas: Carta[] = [];
  _carrito: any = CarritoService.carrito;

  constructor(
    private webService: WebService,
    private util: UtilService,
    private auth: AuthenticationService,
    private carrito: CarritoService,
    private navCtrl: NavController) { }

  ngOnInit() {

    this.lorem = 'Lorem ipsum, dolor sit amet consectetur adipisicing elit. Magni, cumque quaerat? Voluptas harum distinctio '
    this.ObtenerCartas();
  }

  async ObtenerCartas(){
    try {
      const cartas = await this.webService.ObternerCartas();
      if(cartas.code == 400){
        const toast = await this.util.Toast(cartas.message, 2000);
        toast.present();
      }

      this.cartas = new Carta().parseObject(cartas.data);
      

    } catch (error) { }
  }

  modifyQuantity(action: string){
    console.log('pressed '+action);
  }

  navigateTo(page: string){
    this.navCtrl.navigateForward(`${page}`, { animated: true, animationDirection: 'forward' });
  }


  modifySelect(type: string, carta: Carta){
    if(type == 'add'){
      carta.cantidad = carta.cantidad+1;
      this.totalCarta(carta, true);
    }
    if(type == 'rm'){
      if(carta.cantidad > 0){
        carta.cantidad = carta.cantidad-1;
        this.totalCarta(carta, false);
      }
    }



  }

  totalCarta(carta: Carta, isAdd: boolean){
    if(isAdd){
      CarritoService.carrito.total = CarritoService.carrito.total +  parseInt(carta.precio_unidad);
      this._carrito =  CarritoService.carrito;
      // CarritoService.carrito.productos.push()
    }
    if(!isAdd){
      CarritoService.carrito.total = CarritoService.carrito.total -  parseInt(carta.precio_unidad);
      this._carrito =  CarritoService.carrito;
    }
  }



  navigate(carta: Carta){
    UtilService.data = carta;
    this.navCtrl.navigateForward('/detalle-producto');
  }

  logout(){
    this.auth.logout();
  }


}
