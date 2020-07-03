import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouteReuseStrategy } from '@angular/router';

import { IonicModule, IonicRouteStrategy } from '@ionic/angular';
import { SplashScreen } from '@ionic-native/splash-screen/ngx';
import { StatusBar } from '@ionic-native/status-bar/ngx';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { AuthGuardService } from './services/auth-guard.service';
import { AuthenticationService } from './services/Authentication.service';
import { IonicStorageModule } from '@ionic/storage';
import { ComponentsModule } from './components/component.module';
import { AngularFireModule } from '@angular/fire';
import { AngularFirestoreModule } from '@angular/fire/firestore';
import { AngularFireStorageModule } from '@angular/fire/storage';
import { environment } from 'src/environments/environment';
import { FirebaseService } from './services/firebase.service';
import { RequestService } from './services/request.service';
import { WebService } from './services/webservice.service';
import { HttpClientModule } from '@angular/common/http';
import { UtilService } from './services/util.service';
import { Ng2Rut } from 'ng2-rut';
import { CarritoService } from './services/carrito.service';
import { CurrencyPipe } from '@angular/common';

@NgModule({
  declarations: [AppComponent],
  entryComponents: [],
  imports: [
    HttpClientModule,
    BrowserModule,
    Ng2Rut,
    IonicModule.forRoot({ mode: 'ios' }),
    IonicStorageModule.forRoot({ name: '__mydb', driverOrder: ['indexeddb', 'sqlite', 'websql'] }),
    AppRoutingModule,
    AngularFireModule.initializeApp(environment.firebase),
    AngularFirestoreModule, // imports firebase/firestore, only needed for database features
    AngularFireStorageModule
  ],

  providers: [
    StatusBar,
    SplashScreen,
    AuthGuardService,
    AuthenticationService,
    FirebaseService,
    RequestService,
    WebService,
    CarritoService,
    UtilService,
    CurrencyPipe,
    { provide: RouteReuseStrategy, useClass: IonicRouteStrategy }
  ],
  bootstrap: [AppComponent],
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class AppModule { }
