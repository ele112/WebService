import { Component, OnInit, Input } from '@angular/core';
import { Carta } from '../models/cruds.class';
import { UtilService } from '../services/util.service';

@Component({
  selector: 'app-detalle-producto',
  templateUrl: './detalle-producto.page.html',
  styleUrls: ['./detalle-producto.page.scss'],
})
export class DetalleProductoPage implements OnInit {

  carta: Carta;
  constructor() { }

  ngOnInit() {
    const data = UtilService.data;
    this.carta = data;
    console.log(data);
  }


}
