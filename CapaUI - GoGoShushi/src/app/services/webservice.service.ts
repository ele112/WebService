import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { RequestService } from './request.service';
import { Promocion, Ingrediente, Estado, Repartidor } from '../models/cruds.class';


interface response {
    code: number,
    message: string,
    data?: any[]
}
@Injectable()
export class WebService {


    constructor(
        private request: RequestService
    ){}


    //Cartas
    public agregarCarta(params: any): Promise<response> {
        return this.request.post('api/cartas/agregar', params);
    }

    public agregarFoto(params: any): Promise<response>{
        return this.request.post('api/fotos/agregar', params);
    }

    public ObternerCartas(): Promise<response>{
        return this.request.get('api/cartas');
    }

    public EliminarCarta(id: number): Promise<response>{
        return this.request.get('api/cartas/eliminar?id='+id);
    }

    public AsignarPromo(params: any): Promise<response>{
        return this.request.post('api/cartas/asignar', params);   
    }

    // Promociones 
    public ObtenerPromociones(): Promise<response>{
        return this.request.get('api/promociones');
    }
    public GuardarPromociones(params: Promocion): Promise<response>{
        return this.request.post('api/promociones/agregar', params);
    }
    
    public ActualizarPromocion(params: Promocion): Promise<response>{
        return this.request.post('api/promociones/actualizar', params);
    }

    public EliminarPromocion(id: number): Promise<response>{
        return this.request.get('api/promociones/eliminar?id='+id);
    }

    //Ingredientes
    public ObtenerIngredientes(){
        return this.request.get('api/ingrediente');
    }

    public GuardarIngredientes(params: Ingrediente): Promise<response>{
        return this.request.post('api/ingrediente/agregar', params);
    }

    public ActualizarIngrediente(params: Ingrediente): Promise<response>{
        return this.request.post('api/ingrediente/actualizar', params);
    }

    public EliminarIngrediente(id: number): Promise<response>{
        return this.request.get('api/ingrediente/eliminar?id='+id);
    }

    //Estados
    public ObtenerEstados(){
        return this.request.get('api/estado');
    }

    public GuardarEstados(params: Estado): Promise<response>{
        return this.request.post('api/estado/agregar', params);
    }

    public ActualizarEstado(params: Estado): Promise<response>{
        return this.request.post('api/estado/actualizar', params);
    }

    public EliminarEstado(id: number): Promise<response>{
        return this.request.get('api/estado/eliminar?id='+id);
    }


    //Repartidor
    public ObtenerRepartidores(){
        return this.request.get('api/repartidor');
    }

    public GuardarRepartidor(params: Repartidor): Promise<response>{
        return this.request.post('api/repartidor/agregar', params);
    }

    public ActualizarRepartidor(params: Repartidor): Promise<response>{
        return this.request.post('api/repartidor/actualizar', params);
    }

    public EliminarRepartidor(id: number): Promise<response>{
        return this.request.get('api/repartidor/eliminar?id='+id);
    }

}