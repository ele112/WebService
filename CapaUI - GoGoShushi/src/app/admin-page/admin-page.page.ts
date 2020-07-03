import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '../services/Authentication.service';
import { PopoverController, ModalController } from '@ionic/angular';
import { PopoverComponent } from '../components/popover/popover.component';
import { CrudsComponent } from '../components/cruds/cruds.component';

@Component({
  selector: 'app-admin-page',
  templateUrl: './admin-page.page.html',
  styleUrls: ['./admin-page.page.scss'],
})
export class AdminPagePage implements OnInit {

  public ELEMENTS = [
    { name: 'Agregar Carta',            value: 'carta',       src: 'assets/images/sushi.png'},
    { name: 'Asignar Promocion',        value: 'asignaPromo', src: 'assets/images/promotion.png'},
    { name: 'Matenedor Promociones',    value: 'promo',       src: 'assets/images/package.png' },
    { name: 'Matenedor  Ingredientes',  value: 'ingrediente', src: 'assets/images/harvest.png' },
    { name: 'Mantenedor Repartidores',  value: 'repartidor',  src: 'assets/images/food-delivery.png' },
    { name: 'Mantendor Estados',        value: 'estado' ,     src: 'assets/images/restaurant.png' },
  ];
  constructor(
    private popoverController: PopoverController,
    private modal: ModalController,
    private authService: AuthenticationService,) { }

  ngOnInit() {
  }


  async cerrarSesion(ev: any){
    this.authService.logout();
  }

  async showModal(show: string){
    const modal = await this.modal.create({
      component: CrudsComponent,
      animated: true,
      componentProps: { crud: show }
    });

    modal.present();
  }

}
