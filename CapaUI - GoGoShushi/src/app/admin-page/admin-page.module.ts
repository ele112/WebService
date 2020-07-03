import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { AdminPagePageRoutingModule } from './admin-page-routing.module';

import { AdminPagePage } from './admin-page.page';
import { ComponentsModule } from '../components/component.module';
import { CrudsComponent } from '../components/cruds/cruds.component';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    ComponentsModule,
    AdminPagePageRoutingModule
  ],
  declarations: [AdminPagePage, CrudsComponent],
  entryComponents: [CrudsComponent],
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class AdminPagePageModule {}
