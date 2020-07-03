import { Component, OnInit, Input, ViewChild } from '@angular/core';
import { FirebaseService } from 'src/app/services/firebase.service';
import { LoadingController, ModalController, AlertController, ToastController, IonContent } 
  from '@ionic/angular';
import { WebService } from 'src/app/services/webservice.service';
import { isNumber, isNullOrUndefined } from 'util';
import { Ingrediente, Repartidor, Estado, Promocion, Carta } from 'src/app/models/cruds.class';
import { UtilService } from 'src/app/services/util.service';
import * as _ from 'lodash';

@Component({
  selector: 'app-cruds',
  templateUrl: './cruds.component.html',
  styleUrls: ['./cruds.component.scss'],
})
export class CrudsComponent implements OnInit {
  @ViewChild(IonContent, { static: false }) ionContent: IonContent;
  
  @Input() crud: any;
  image: any;

  ingrediente:  Ingrediente = new Ingrediente();
  promocion:    Promocion   = new Promocion();
  repartidor:  Repartidor  = new Repartidor();
  estado:       Estado      = new Estado();
  carta:       Carta      = new Carta();

  isModifyIngrediente:  boolean = false;
  isModifyPromo:        boolean = false;
  isModifyRepartidor:   boolean = false;
  isModifyEstado:       boolean = false;
  isModifyCarta:        boolean = false;

  promociones:  Promocion[]   = [];
  ingredientes: Ingrediente[] = [];
  estados:      Estado[]      = [];
  repartidores: Repartidor[]  = [];
  cartas:       Carta[] = [];

  //Imagen
  imageIsUpload: boolean = false;
  imageUrl: string;

  constructor(
    private webService: WebService,
    private loading: LoadingController,
    private alertCtrl: AlertController,
    private toastCtrl: ToastController,
    private modal: ModalController,
    private firebase: FirebaseService,
    private util: UtilService
  ) { }

  ngOnInit() {
    console.log(this.crud)

    if(this.crud == 'promo'){
      this.obtenerPromociones();
    }

    if(this.crud == 'ingrediente'){
      this.obtenerIngredientes();
    }

    if(this.crud == 'estado'){
      this.obtenerEstados();
    }

    if(this.crud == 'repartidor'){
      this.obtenerRepartidores();
    }

    if(this.crud == 'carta'){
      this.obtenerIngredientes();
      this.obtenerCartas();

    }

    if(this.crud == 'asignaPromo'){
      this.obtenerCartas();
      this.obtenerPromociones();
    }
  }

  //Carta
  async guardarCarta(form: any) {

    try{
      const toast = await this.toastCtrl.create({duration: 2000, position: 'top', color: 'primary'});
      const _form = this.carta;
      
      const _ingre: any[] = _form.ingredientes;

      _form.ingredientes = _.map(_ingre, (ingId) => {
        return this.ingredientes.filter((ingrediente) => {
          return ingrediente.id == ingId;
        })[0];
      });


      if(!isNumber(_form.precio_unidad)){
        toast.message = "El precio debe ser numerico";
        return toast.present();
      }

      if(!this.imageIsUpload){
        toast.message = "Debe subir una imagen";
        return toast.present();
      }

      const loading = await this.loading.create({ message: "Creando carta..." });
      loading.present();

      const paramFoto = {
        "Nombre": this.firebase.nombreArchivo,
        "Url"   : this.imageUrl,
      }

      const response = await this.webService.agregarFoto(paramFoto);
      const idFoto = response['data']['id'];


      const paramCarta = {
        "Nombre": _form.nombre,
        "Descripcion": _form.descripcion,
        "Precio_unidad": _form.precio_unidad,
        "id_foto": idFoto,
        "ingredientes": _form.ingredientes
      }
      const result = await this.webService.agregarCarta(paramCarta);

      toast.message = result.message;
      toast.present();
      loading.dismiss();

      if(result.code == 400){
        return;
      }

      this.modal.dismiss();


    }catch{

      const toast = await this.toastCtrl.create({
        message: "Algo paso",
        duration: 2000
      });
      toast.present();
    }


    // console.log(_form);
    // const params = {
    //   "Nombre": "",
    //   "Precio_unidad": "",
    //   "Descripcion": "",

    // }
    // this.webService.EnviarCarta()

  }


  async obtenerCartas(){
    try {
      const cartas = await this.webService.ObternerCartas();
      if(cartas.code == 400){
        const toast = await this.util.Toast(cartas.message, 2000);
        toast.present();
      }

      this.cartas = new Carta().parseObject(cartas.data);
      

    } catch (error) { }
  }

  async subirImagen(event) {
    const loading = await this.loading.create({ message: 'Subiendo imagen' });
    loading.present();

    this.firebase.subirImagen(event);
    this.firebase.taskUpload.subscribe(async(porcentaje) => {
      loading.message = "Subiendo imagen "+Math.round(porcentaje)+"%";

      if(porcentaje === 100){
        loading.message = "Obteniendo imagen";
        await this.waitingFor(3000);

        this.firebase.fileRef.subscribe((url) => {
          console.log(url);
          this.imageIsUpload = true;
          this.imageUrl = url;
          loading.dismiss();
          
        });
      }

    });
  }

  modificarCarta(carta: Carta){
    this.ionContent.scrollToTop(200);
    this.isModifyCarta = true;
    this.carta = carta;
    let ids: any[] = _.map(carta.ingredientes, (item) => { return item.id });
    var multi = document.getElementById('multiValue');
    multi['value'] = ids;

  }

  async eliminarCarta(id: number){
    const alert = await this.alertCtrl.create({
      header: 'Cartas',
      message: '¿Esta seguro de eliminar la carta?',
      buttons: [
        {
          text: 'Cancelar',
          role: 'Cancelar',
          handler: () => {}
        },
        {
          text: 'Aceptar',
          role: 'Aceptar',
          handler: async () => {

            const loading = await this.loading.create({ message: 'Eliminando carta'});
            loading.present();
        
            const response = await this.webService.EliminarCarta(id);
            if(response.code == 400){
              const toast = await this.util.Toast(response.message, 2000);
              toast.present();
              loading.dismiss();
              
              return;
            }
        
            loading.dismiss();
            (await this.util.Toast(response.message, 2000)).present();
            this.obtenerCartas();
            this.carta = new Carta();

          }
        }
      ]
    })

    alert.present();
    

  }
  
  async asignaPromocion(event: any, index){
    const loading = await this.loading.create({ message: "Asginando"});
    loading.present();
    try {
      const body = this.cartas[index];
      body.id_promocion = event.detail.value;

    

      const response = await this.webService.AsignarPromo(body);
      loading.dismiss();

      const toast = await this.util.Toast(response.message, 2000);
      toast.present();

      if(response.code == 400){
        body.id_promocion = null;
      }
      

    } catch (error) {
      loading.dismiss();

     }


  }




  // Promociones

  async obtenerPromociones(){
    try {
      const promo = await this.webService.ObtenerPromociones();
      if(promo.code == 400){
        const toast = await this.util.Toast(promo.message, 2000);
        toast.present();
      }

      this.promociones = new Promocion().parseObject(promo.data);

    } catch (error) { }
  }

  async guardarPromo(){
    
    const loading = await this.loading.create({ message: 'Creando Promocion'});
 

    loading.present();
    try{
      const response = await this.webService.GuardarPromociones(this.promocion);

      if(response.code == 400){
        const toast = await this.util.Toast(response.message, 2000);
        toast.present();
        loading.dismiss();

        return;
      } 
      loading.dismiss();

      const toast = await this.util.Toast(response.message, 2000);
      toast.present();

      this.promocion = new Promocion();
      this.obtenerPromociones();

    }catch(error){
      loading.dismiss();
      const toast = await this.util.Toast("algo sucedio", 2000);
      toast.present();
    }
  }

  async guardarModificacionPromo(){
    try {
      const loading = await this.loading.create({ message: 'Actualizando '});
      loading.present();
      const response = await this.webService.ActualizarPromocion(this.promocion);
      loading.dismiss();


      this.promocion = new Promocion();
      (await this.util.Toast(response.message, 2000)).present();
      this.obtenerPromociones();

    } catch (error) { }
  }

  modificarPromo(promo: Promocion){
    this.ionContent.scrollToTop(200);
    this.isModifyPromo = true;
    this.promocion = promo;
  }

  async eliminarPromo(id: number){
    const alert = await this.alertCtrl.create({
      header: 'Promocion',
      message: '¿Esta seguro de eliminar la promocion?',
      buttons: [
        {
          text: 'Cancelar',
          role: 'Cancelar',
          handler: () => {}
        },
        {
          text: 'Aceptar',
          role: 'Aceptar',
          handler: async () => {

            const loading = await this.loading.create({ message: 'Eliminando promocion'});
            loading.present();
        
            const response = await this.webService.EliminarPromocion(id);
            if(response.code == 400){
              const toast = await this.util.Toast(response.message, 2000);
              toast.present();
              loading.dismiss();
        
              return;
            }
        
            loading.dismiss();
            (await this.util.Toast(response.message, 2000)).present();
            this.obtenerPromociones();

          }
        }
      ]
    })

    alert.present();
    

  }
  


  // Ingredientes
  async obtenerIngredientes(){
    try {
      const ingrediente = await this.webService.ObtenerIngredientes();
      if(ingrediente.code == 400){
        const toast = await this.util.Toast(ingrediente.message, 2000);
        toast.present();
      }

      this.ingredientes = new Ingrediente().parseObject(ingrediente.data);

    } catch (error) { }
  }

  async guardarIngrediente(){
    const loading = await this.loading.create({ message: 'Creando ingrediente'});
    loading.present();

    try{
      const response = await this.webService.GuardarIngredientes(this.ingrediente);

      if(response.code == 400){
        const toast = await this.util.Toast(response.message, 2000);
        toast.present();
        loading.dismiss();

        return;
      } 
      loading.dismiss();

      const toast = await this.util.Toast(response.message, 2000);
      toast.present();

      this.ingrediente = new Ingrediente();
      this.obtenerIngredientes();

    }catch(error){
      loading.dismiss();
      const toast = await this.util.Toast("algo sucedio", 2000);
      toast.present();
    }
  }

  async guardarIngredienteModificado(){
    try {
      const loading = await this.loading.create({ message: 'Actualizando '});
      loading.present();

      const response = await this.webService.ActualizarIngrediente(this.ingrediente);
      loading.dismiss();


      this.ingrediente = new Ingrediente();
      (await this.util.Toast(response.message, 2000)).present();
      this.obtenerIngredientes();

    } catch (error) { }
  }

  modificarIngrediente(ingrediente: Ingrediente) {
    this.ionContent.scrollToTop(200);
    this.isModifyIngrediente = true;
    this.ingrediente = ingrediente;

  }


  async eliminarIngrediente(id: number){
    const alert = await this.alertCtrl.create({
      header: 'Ingrediente',
      message: '¿Esta seguro de eliminar el ingrediente?',
      buttons: [
        {
          text: 'Cancelar',
          role: 'Cancelar',
          handler: () => {}
        },
        {
          text: 'Aceptar',
          role: 'Aceptar',
          handler: async () => {

            const loading = await this.loading.create({ message: 'Eliminando ingrediente'});
            loading.present();
        
            const response = await this.webService.EliminarIngrediente(id);
            if(response.code == 400){
              const toast = await this.util.Toast(response.message, 2000);
              toast.present();
              loading.dismiss();
        
              return;
            }
        
            loading.dismiss();
            (await this.util.Toast(response.message, 2000)).present();
            this.obtenerIngredientes();

          }
        }
      ]
    })

    alert.present();
    

  }
  

  //Estados
  async obtenerEstados(){
    try {
      const estado = await this.webService.ObtenerEstados();
      if(estado.code == 400){
        const toast = await this.util.Toast(estado.message, 2000);
        toast.present();
      }

      this.estados = new Estado().parseObject(estado.data);

    } catch (error) { }
  }

  async guardarEstado(){
    const loading = await this.loading.create({ message: 'Creando estado'});
    loading.present();

    try{
      const response = await this.webService.GuardarEstados(this.estado);

      if(response.code == 400){
        const toast = await this.util.Toast(response.message, 2000);
        toast.present();
        loading.dismiss();

        return;
      } 
      loading.dismiss();

      const toast = await this.util.Toast(response.message, 2000);
      toast.present();

      this.estado = new Estado();
      this.obtenerEstados();

    }catch(error){
      loading.dismiss();
      const toast = await this.util.Toast("algo sucedio", 2000);
      toast.present();
    }
  }

  async guardarEstadoModificado(){
    try {
      const loading = await this.loading.create({ message: 'Actualizando '});
      loading.present();

      const response = await this.webService.ActualizarEstado(this.estado);
      loading.dismiss();


      this.estado = new Estado();
      (await this.util.Toast(response.message, 2000)).present();
      this.obtenerEstados();

    } catch (error) { }
  }

  modificarEstado(estado: Estado) {
    this.ionContent.scrollToTop(200);
    this.isModifyEstado = true;
    this.estado = estado;

  }

  async eliminarEstado(id: number){
    const alert = await this.alertCtrl.create({
      header: 'Estado',
      message: '¿Esta seguro de eliminar el estado?',
      buttons: [
        {
          text: 'Cancelar',
          role: 'Cancelar',
          handler: () => {}
        },
        {
          text: 'Aceptar',
          role: 'Aceptar',
          handler: async () => {

            const loading = await this.loading.create({ message: 'Eliminando estado'});
            loading.present();
        
            const response = await this.webService.EliminarEstado(id);
            if(response.code == 400){
              const toast = await this.util.Toast(response.message, 2000);
              toast.present();
              loading.dismiss();
        
              return;
            }
        
            loading.dismiss();
            (await this.util.Toast(response.message, 2000)).present();
            this.obtenerEstados();

          }
        }
      ]
    })

    alert.present();
    

  }
  

  //Repartidor
  async obtenerRepartidores(){
    try {
      const repartidor = await this.webService.ObtenerRepartidores();
      if(repartidor.code == 400){
        const toast = await this.util.Toast(repartidor.message, 2000);
        toast.present();
      }

      this.repartidores = new Repartidor().parseObject(repartidor.data);

    } catch (error) { }
  }

  async guardarRepartidor(){
    const loading = await this.loading.create({ message: 'Creando repartidor'});
    loading.present();

    try{
      const response = await this.webService.GuardarRepartidor(this.repartidor);

      if(response.code == 400){
        const toast = await this.util.Toast(response.message, 2000);
        toast.present();
        loading.dismiss();

        return;
      } 
      loading.dismiss();

      const toast = await this.util.Toast(response.message, 2000);
      toast.present();

      this.repartidor = new Repartidor();
      this.obtenerRepartidores();

    }catch(error){
      loading.dismiss();
      const toast = await this.util.Toast("Algo sucedio", 2000);
      toast.present();
    }
  }

  async guardarRepartidorModificado(){
    try {
      const loading = await this.loading.create({ message: 'Actualizando '});
      loading.present();

      const response = await this.webService.ActualizarRepartidor(this.repartidor);
      loading.dismiss();

      (await this.util.Toast(response.message, 2000)).present();

      if(response.code == 200){
        this.repartidor = new Repartidor();
        this.obtenerRepartidores();
      }


    } catch (error) { }
  }

  modificarRepartidor(repartidor: Repartidor) {
    this.ionContent.scrollToTop(200);
    this.isModifyRepartidor = true;
    this.repartidor = repartidor;

  }

  async eliminarRepartidor(id: number){
    const alert = await this.alertCtrl.create({
      header: 'Repartidor',
      message: '¿Esta seguro de eliminar el repartidor?',
      buttons: [
        {
          text: 'Cancelar',
          role: 'Cancelar',
          handler: () => {}
        },
        {
          text: 'Aceptar',
          role: 'Aceptar',
          handler: async () => {

            const loading = await this.loading.create({ message: 'Eliminando repartidor'});
            loading.present();
        
            const response = await this.webService.EliminarRepartidor(id);
            if(response.code == 400){
              const toast = await this.util.Toast(response.message, 2000);
              toast.present();
              loading.dismiss();
        
              return;
            }
        
            loading.dismiss();
            (await this.util.Toast(response.message, 2000)).present();
            this.obtenerRepartidores();

          }
        }
      ]
    })

    alert.present();
    

  }
  


  // Transaccional



  asignarPromocion(carta: any, data: any){

  }





  async agregadDescuentoPromocion(carta: any) {
    const alert = await this.alertCtrl.create({
      message: 'Asignar promocion',
      inputs: [
        {
          name: '% de descuento',
          type: 'number',
          placeholder: 'Agrega el % de descuento'
        }
      ],
      buttons: [
        {
          text: 'Cancelar',
          handler: () => { }
        },
        {
          text: 'Agregar',
          handler: (data) => {
            this.asignarPromocion(carta, data);
          }
        }
      ]
    })

    alert.present();
  }


  // utils
  waitingFor(timer: number){
    return new Promise((resolve) => {
      setTimeout(() => {
        return resolve('Ok');
      }, timer);
    })
  }

  close() {
    this.modal.dismiss();
  }
}
