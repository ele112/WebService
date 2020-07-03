import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { DetalleProductoPageRoutingModule } from './detalle-producto-routing.module';

import { DetalleProductoPage } from './detalle-producto.page';
import { SharedModule } from 'src/shared/shared.module';
import { PipeModule } from '../pipes/pipes.module';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    SharedModule,
    PipeModule,
    DetalleProductoPageRoutingModule
  ],
  declarations: [DetalleProductoPage]
})
export class DetalleProductoPageModule {}
