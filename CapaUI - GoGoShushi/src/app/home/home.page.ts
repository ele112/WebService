import { Component } from '@angular/core';
import { StatusBar } from '@ionic-native/status-bar/ngx';
import { ModalController, IonRouterOutlet, Platform, NavController, PopoverController } from '@ionic/angular';
import { SearchViewComponent } from './search-view/search-view.component';
import { PerfilComponent } from './perfil/perfil.component';
import { PopoverComponent } from '../components/popover/popover.component';
import { AuthenticationService } from '../services/Authentication.service';

@Component({
  selector: 'app-home',
  templateUrl: 'home.page.html',
  styleUrls: ['home.page.scss'],
})
export class HomePage {

  slideConfig = {
    slidesPerView: 1.2,
    centeredSlides: false,
    cubeEffect: {
      shadow: true,
      slideShadows: true,
      shadowOffset: 20,
      shadowScale: 0.94,
    }
  }
  slideConfigMedium = {
    slidesPerView: 2.5,
    centeredSlides: false,
    cubeEffect: {
      shadow: true,
      slideShadows: true,
      shadowOffset: 20,
      shadowScale: 0.94,
    }
  }



  constructor(private statusBar: StatusBar, 
    private router: IonRouterOutlet,
    private platform: Platform,
    private navCtrl: NavController,
    private popoverController: PopoverController,
    private auth: AuthenticationService,
    public modalController: ModalController) {
    this.statusBar.overlaysWebView(true);
    this.statusBar.backgroundColorByHexString('#2AC769');

  }

  ionViewDidEnter() {
    this.slideConfig.slidesPerView = this.platform.width() > 800 ? 2.2 : 1.2;


  }

  ionViewDieLeave() {
    this.platform.resize.unsubscribe();
  }

  async presentPopover(ev: any) {
    this.auth.logout();

    // const popover = await this.popoverController.create({
    //   component: PopoverComponent,
    //   cssClass: 'my-custom-class',
    //   event: ev,
    //   translucent: true
    // });
    // return await popover.present();
  }

  swipeEvent(ev: any){
    console.log(ev);
  }

  async presentSearchView() {
    // console.log('Clicked')
    const modal = await this.modalController.create({
      component: SearchViewComponent,
      swipeToClose: true,
      animated: true
    });
    return await modal.present();
  }

  async presentProfileModal(){
    const modal = await this.modalController.create({
      component: PerfilComponent,
      swipeToClose: true,
      animated: true,
      presentingElement: this.router.nativeEl
    });
    return await modal.present();
  }


  navigateTo(page: string){
    this.navCtrl.navigateForward(`/${page}`);
  }



}
