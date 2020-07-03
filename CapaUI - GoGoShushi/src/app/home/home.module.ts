import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IonicModule } from '@ionic/angular';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';


import { HomePage } from './home.page';
import { SearchViewComponent } from './search-view/search-view.component';
import { PerfilComponent } from './perfil/perfil.component';
// import { PopoverComponent } from '../components/popover/popover.component';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    RouterModule.forChild([
      {
        path: '',
        component: HomePage
      }
    ])
  ],
  declarations: [HomePage, SearchViewComponent, PerfilComponent],
  entryComponents: [SearchViewComponent, PerfilComponent]
})
export class HomePageModule {}
