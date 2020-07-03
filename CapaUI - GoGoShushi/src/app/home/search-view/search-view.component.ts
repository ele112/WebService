import { Component, OnInit } from '@angular/core';
import { ModalController } from '@ionic/angular';

@Component({
  selector: 'app-search-view',
  templateUrl: './search-view.component.html',
  styleUrls: ['./search-view.component.scss'],
})
export class SearchViewComponent implements OnInit {

  items: any[] = [
    { name: "Amsterdam" },
    { name: "Bogota" },
    { name: "Buenos" },
    { name: "Cairo" },
    { name: "Dhaka" },
    { name: "Edinburgh" },
    { name: "Geneva" },
    { name: "Genoa" },
    { name: "Glasgow" },
    { name: "Hanoi" },
    { name: "Hong" },
    { name: "Islamabad" },
    { name: "Istanbul" },
    { name: "Jakarta" },
    { name: "Kiel" },
    { name: "Kyoto" },
    { name: "Le" },
    { name: "Lebanon" },
    { name: "Lhasa" },
    { name: "Lima" },
    { name: "London" },
    { name: "Los" },
    { name: "Madrid" },
    { name: "Manila" },
    { name: "New" },
    { name: "Olympia" },
    { name: "Oslo" },
    { name: "Panama" },
    { name: "Peking" },
    { name: "Philadelphia" },
    { name: "San" },
    { name: "Seoul" },
    { name: "Taipeh" },
    { name: "Tel" },
    { name: "Tokio" },
    { name: "Uelzen" },
    { name: "Washington" }
  ];

  searchItems: any = [];
  constructor(private modal: ModalController) {
   
   
  }

  
  ngOnInit() { }

  handleInput(event){
    const value = event.srcElement.value;
    if(value.trim() == '' || value.trim() == null) { this.searchItems = []; return; }
    this.searchItems = this.items
    .filter((item) =>{ return (item.name).toLowerCase().indexOf(value.toLowerCase()) > -1 });
  
  }


  dismiss(){
    this.modal.dismiss();
  }
}
