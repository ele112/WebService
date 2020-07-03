
export class Ingrediente {
    id: number;
    nombre: string;
    descripcion: string;
    constructor(){
        this.nombre = null;
        this.descripcion = null;
    }

    parse(json: any){
        try{
            this.id = json.id;
            this.nombre = json.nombre;
            this.descripcion = json.descripcion;
            return this;

        }catch(error) { }

    }

    parseObject(object: any[]){
        try {
            let ingrediente: Ingrediente[] = []

            object.map((json) => {
                ingrediente.push(new Ingrediente().parse(json));
            });

            return ingrediente;
        } catch (error) { }
    }

}

export class Repartidor{
    id: number;
    nombre: string;
    apellidos: string;
    celular: string;
    rut: string;
    dv: string;
    email: string;

    constructor(){
        this.id = null;
        this.nombre = null;
        this.apellidos = null;
        this.celular = null;
        this.rut = null;
        this.dv = null;
        this.email = null;
    }


    parse(json: any){
        try {
            this.id = json.id;
            this.nombre = json.nombre;
            this.apellidos = json.apellidos;
            this.celular = json.celular;
            this.rut = json.rut;
            this.dv = json.dv;
            this.email = json.email;

            return this;

        } catch (error) { }

    }


    parseObject(object: any[]){
        try {
            let repartidor: Repartidor[] = []

            object.map((json) => {
                repartidor.push(new Repartidor().parse(json));
            });

            return repartidor;
        } catch (error) { }
    }
}

export class Estado{
    id: number;
    nombre: string;

    constructor(){
        this.id = null;
        this.nombre = null;
    }

    parse(json: any){
        try {
            this.id = json.id;
            this.nombre = json.nombre;

            return this;
        } catch (error) { }
    }

    parseObject(object: any[]){
        try {
            
            let estados: Estado[] = [];
            object.map((estado) => {
                estados.push(new Estado().parse(estado));
            })

            return estados;
        } catch (error) { }
    }
}


export class Promocion{
    id: number;
    nombre: string;
    descripcion: string;
    porc_descuento: string;
    max_descuento: number;


    constructor(){
        this.id = null;
        this.nombre = null;
        this.descripcion = null;
        this.porc_descuento = null;
        this.max_descuento = null;
    }

    parse(json: any){
        try {
            this.id = json.id;
            this.nombre = json.nombre;
            this.descripcion = json.descripcion;
            this.porc_descuento = json.porc_descuento;
            this.max_descuento = json.max_descuento;
            
            return this;

        } catch (error) {}
    }

    parseObject(object: any[]){
        try {
            
            let promo: Promocion[] = [];
            object.map((_promo) => {
                promo.push(new Promocion().parse(_promo));
            })

            return promo;
        } catch (error) { }
    }

}

export class Carta{
    id: number;
    nombre: string;
    descripcion: string;
    precio_unidad: string;
    id_promocion: number;
    id_foto: number;
    urlFoto: string;
    promocion: Promocion;
    ingredientes: Ingrediente[];
    cantidad?: number;

    constructor(){
        this.id = null;
        this.nombre = null;
        this.descripcion = null;
        this.precio_unidad = null;
        this.id_promocion = null;
        this.id_foto = null;
        this.urlFoto = null;
        this.promocion = null;
        this.ingredientes = null;
        this.cantidad = 0;
    }

    public parse(json: any){
        try {
            this.id = json.id;
            this.nombre = json.nombre;
            this.descripcion = json.descripcion;
            this.precio_unidad = json.precio_unidad;
            this.id_promocion = json.id_promocion;
            this.id_foto = json.id_foto;
            this.urlFoto = json.urlFoto;
            this.promocion = json.promocion[0] || null;
            this.ingredientes = json.ingredientes;
            this.cantidad = 0;

            return this;

        } catch (error) { }
    }

    
    parseObject(object: any[]){
        try {
            
            let cartas: Carta[] = [];
            object.map((carta) => {
                cartas.push(new Carta().parse(carta));
            })

            return cartas;
        } catch (error) { }
    }
}