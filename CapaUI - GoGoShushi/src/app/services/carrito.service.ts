import { Injectable } from '@angular/core';
import { Carro } from '../models/carrito.class';



@Injectable()
export class CarritoService {
    

    public static carrito: Carro = new Carro();
    constructor(){

    }



}